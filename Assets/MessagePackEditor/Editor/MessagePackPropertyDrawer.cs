using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using MessagePack;

namespace MessagePackEditor
{
    public interface IMessagePackPropertyDrawer
    {
        Type TargetType { get; }
        bool DrawField(string label, Func<object> getter, Action<object> setter, Type type);
    }

    public class DefaultMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        private readonly bool _useFoldout = true;
        private bool _foldoutOpen = false;

        private bool _unionChecked = false;
        private UnionAttribute[] _unions = null;
        private int _unionSelectedIndex = -1;
        private GUIContent[] _unionPopup = null;
        private static readonly GUIContent UnionLabel = new GUIContent("Union");

        private ArrayMessagePackPropertyDrawer _arrayDrawer = null;
        private Dictionary<string, DefaultMessagePackPropertyDrawer> _childDrawers = new Dictionary<string, DefaultMessagePackPropertyDrawer>();

        public DefaultMessagePackPropertyDrawer(bool useFoldout = true)
        {
            _useFoldout = useFoldout;
        }

        public Type TargetType
        {
            get { return typeof(object); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            //Primitive?
            var primitiveDrawer = MessagePackPrimitivePropertyDrawer.GetDrawer(type);
            if (primitiveDrawer != null)
                return primitiveDrawer.DrawField(label, getter, setter, type);

            //Array?
            if (type.IsArray)
            {
                if (_arrayDrawer == null)
                    _arrayDrawer = new ArrayMessagePackPropertyDrawer(_useFoldout);

                return _arrayDrawer.DrawField(label, getter, setter, type);
            }

            //[MessagePackObject]でもないし[Union]でもないなら処理不可
            if (!MessagePackTypeCache.Instance.IsCached(type))
                return false;
            
            bool edit = false;

            //折り畳み処理
            if (_useFoldout)
            {
                _foldoutOpen = EditorGUILayout.Foldout(_foldoutOpen, label);
                if (!_foldoutOpen)
                    return false;
            }

            var concreteType = type;

            //すでにinstanceがあるか、取得しておく
            var obj = getter();

            //Union対象かを一度だけチェックする
            if (!_unionChecked)
            {
                _unions = MessagePackTypeCache.Instance.GetUnions(type);
                if (_unions != null && _unions.Length > 0)
                {
                    _unionPopup = _unions.Select(x => new GUIContent(x.SubType.FullName)).ToArray();
                }
                _unionChecked = true;
            }
            
            if(_unionPopup != null)
            {
                //初回だけ、objに合わせたindexに調整する
                if(_unionSelectedIndex == -1)
                {
                    if (obj != null)
                    {
                        _unionSelectedIndex = Array.FindIndex(_unions, union => union.SubType == obj.GetType());
                    }

                    if (_unionSelectedIndex == -1)
                        _unionSelectedIndex = 0;
                }

                var newIndex = EditorGUILayout.Popup(UnionLabel, _unionSelectedIndex, _unionPopup);
                if(newIndex != _unionSelectedIndex)
                {
                    _unionSelectedIndex = newIndex;
                    //Unionの選択が変わったら再生成するためにobjをnullにしておく
                    obj = null;
                    edit = true;
                }
                concreteType = _unions[_unionSelectedIndex].SubType;
            }

            //デフォルトで作成
            if (obj == null)
            {
                obj = Activator.CreateInstance(concreteType);
                setter(obj);
                edit = true;
            }

            var props = MessagePackTypeCache.Instance.GetProperties(concreteType);
            foreach (var prop in props)
            {
                DefaultMessagePackPropertyDrawer drawer = null;
                if (!_childDrawers.TryGetValue(prop.Name, out drawer))
                {
                    drawer = new DefaultMessagePackPropertyDrawer(); //子はFoldout使う
                    _childDrawers.Add(prop.Name, drawer);
                }

                edit |= drawer.DrawField(prop.Name, () => prop.GetValue(obj, null), (newValue) => prop.SetValue(obj, newValue, null), prop.PropertyType);
            }

            return edit;
        }
    }

    public class ArrayMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        private readonly bool _useFoldout = true;
        private bool _foldoutOpen = false;
        private int _size = 0;

        private List<DefaultMessagePackPropertyDrawer> _drawers = new List<DefaultMessagePackPropertyDrawer>();

        public ArrayMessagePackPropertyDrawer(bool useFoldout = true)
        {
            _useFoldout = useFoldout;
        }

        public Type TargetType
        {
            get { return typeof(Array); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            //変なのが来たぞ！
            if (!type.IsArray)
                return false;

            //折り畳み処理
            if (_useFoldout)
            {
                _foldoutOpen = EditorGUILayout.Foldout(_foldoutOpen, label);
                if (!_foldoutOpen)
                    return false;
            }

            bool edit = false;

            var array = (Array)getter();
            var elementType = type.GetElementType();
            //nullの場合は長さ0配列にしておく
            if (array == null)
            {
                array = Array.CreateInstance(elementType, 0);
                setter(array);
                edit = true;
            }

            //Size表示
            _size = array.Length;
            var sizeStr = _size.ToString();
            var newStr = EditorGUILayout.TextField("Size", sizeStr);
            if (sizeStr != newStr)
            {
                if (int.TryParse(newStr, out _size))
                {
                    if (array.Length != _size)
                    {
                        var newArray = Array.CreateInstance(elementType, _size);
                        Array.Copy(array, newArray, Math.Min(array.Length, newArray.Length));
                        array = newArray;
                        setter(array);
                        edit = true;
                    }
                }
            }

            if (_drawers.Count != array.Length)
            {
                _drawers.Clear();
                for (int i = 0; i < array.Length; i++)
                {
                    _drawers.Add(new DefaultMessagePackPropertyDrawer());
                }
            }

            EditorGUI.indentLevel++;
            for (int i = 0; i < array.Length; i++)
            {
                edit |= _drawers[i].DrawField("Element " + i, () => array.GetValue(i), (newValue) => array.SetValue(newValue, i), elementType);
            }
            EditorGUI.indentLevel--;

            return edit;
        }
    }
}