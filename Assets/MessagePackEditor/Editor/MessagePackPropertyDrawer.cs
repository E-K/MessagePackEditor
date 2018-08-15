using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace MessagePackEditor
{
    public interface IMessagePackPropertyDrawer
    {
        Type TargetType { get; }
        bool DrawField(string label, Func<object> getter, Action<object> setter, Type type);
    }

    public class DefaultMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        private static Dictionary<Type, PropertyInfo[]> _properties = new Dictionary<Type, PropertyInfo[]>();
        private static PropertyInfo[] GetProperties(Type type)
        {
            PropertyInfo[] results = null;
            //キャッシュにあればそれを返却
            if (_properties.TryGetValue(type, out results))
            {
                return results;
            }

            //キャッシュにない →作ってキャッシュにつめる
            results = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.GetCustomAttribute(typeof(MessagePack.KeyAttribute)) != null)
                .ToArray();

            _properties.Add(type, results);

            return results;
        }

        private readonly bool _useFoldout = true;
        private bool _foldoutOpen = false;

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

            //class or struct
            // [MessagePackObject]属性をチェック
            var attrs = type.GetCustomAttributes(typeof(MessagePack.MessagePackObjectAttribute), true);
            if (attrs == null || attrs.Length == 0)
                return false;

            bool edit = false;

            //デフォルトで作成
            var obj = getter();
            if (obj == null)
            {
                obj = Activator.CreateInstance(type);
                setter(obj);
                edit = true;
            }

            //折り畳み処理
            if (_useFoldout)
            {
                _foldoutOpen = EditorGUILayout.Foldout(_foldoutOpen, label);
                if (!_foldoutOpen)
                    return false;
            }

            var props = GetProperties(type);
            foreach (var prop in props)
            {
                DefaultMessagePackPropertyDrawer drawer = null;
                if (!_childDrawers.TryGetValue(prop.Name, out drawer))
                {
                    drawer = new DefaultMessagePackPropertyDrawer(); //子はFoldout使う
                    _childDrawers.Add(prop.Name, drawer);
                }

                edit |= drawer.DrawField(prop.Name, () => prop.GetValue(obj), (newValue) => prop.SetValue(obj, newValue), prop.PropertyType);
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