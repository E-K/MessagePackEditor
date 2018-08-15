using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MessagePackEditor
{
    public static class MessagePackPrimitivePropertyDrawer
    {
        private static readonly Dictionary<Type, IMessagePackPropertyDrawer> _drawers = new Dictionary<Type, IMessagePackPropertyDrawer>();
        static MessagePackPrimitivePropertyDrawer()
        {
            Register(ByteMessagePackPropertyDrawer.Default);
            Register(SByteMessagePackPropertyDrawer.Default);
            Register(BooleanMessagePackPropertyDrawer.Default);
            Register(Int16MessagePackPropertyDrawer.Default);
            Register(Int32MessagePackPropertyDrawer.Default);
            Register(Int64MessagePackPropertyDrawer.Default);
            Register(UInt16MessagePackPropertyDrawer.Default);
            Register(UInt32MessagePackPropertyDrawer.Default);
            Register(UInt64MessagePackPropertyDrawer.Default);
            Register(StringMessagePackPropertyDrawer.Default);
            Register(EnumMessagePackPropertyDrawer.Default);
        }

        private static void Register(IMessagePackPropertyDrawer drawer)
        {
            _drawers.Add(drawer.TargetType, drawer);
        }

        public static IMessagePackPropertyDrawer GetDrawer(Type type)
        {
            var key = type;
            if (type.IsEnum) key = typeof(System.Enum);

            IMessagePackPropertyDrawer result = null;
            _drawers.TryGetValue(key, out result);
            return result;
        }
    }

    public class ByteMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly ByteMessagePackPropertyDrawer Default = new ByteMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.Byte); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (Byte)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (Byte.TryParse(newStr, out value))
                {
                    setter(value);
                    return true;
                }
            }
            return false;
        }
    }

    public class SByteMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly SByteMessagePackPropertyDrawer Default = new SByteMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.SByte); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (SByte)getter();
            var valueStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valueStr);
            if (valueStr != newStr)
            {
                if (SByte.TryParse(newStr, out value))
                {
                    setter(value);
                    return true;
                }
            }
            return false;
        }
    }

    public class BooleanMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly BooleanMessagePackPropertyDrawer Default = new BooleanMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.Boolean); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (Boolean)getter();
            var newValue = EditorGUILayout.Toggle(label, value);
            if (value != newValue)
            {
                setter(newValue);
                return true;
            }
            return false;
        }
    }

    public class Int16MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly Int16MessagePackPropertyDrawer Default = new Int16MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.Int16); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (Int16)getter();
            var valueStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valueStr);
            if (valueStr != newStr)
            {
                if (Int16.TryParse(newStr, out value))
                {
                    setter(value);
                    return true;
                }
            }
            return false;
        }
    }

    public class Int32MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly Int32MessagePackPropertyDrawer Default = new Int32MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.Int32); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (Int32)getter();
            var valueStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valueStr);
            if (valueStr != newStr)
            {
                if (Int32.TryParse(newStr, out value))
                {
                    setter(value);
                    return true;
                }
            }
            return false;
        }
    }

    public class Int64MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly Int64MessagePackPropertyDrawer Default = new Int64MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.Int64); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (Int64)getter();
            var valueStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valueStr);
            if (valueStr != newStr)
            {
                if (Int64.TryParse(newStr, out value))
                {
                    setter(value);
                    return true;
                }
            }
            return false;
        }
    }

    public class UInt16MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly UInt16MessagePackPropertyDrawer Default = new UInt16MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.UInt16); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (UInt16)getter();
            var valueStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valueStr);
            if (valueStr != newStr)
            {
                if (UInt16.TryParse(newStr, out value))
                {
                    setter(value);
                    return true;
                }
            }
            return false;
        }
    }

    public class UInt32MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly UInt32MessagePackPropertyDrawer Default = new UInt32MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.UInt32); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (UInt32)getter();
            var valueStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valueStr);
            if (valueStr != newStr)
            {
                if (UInt32.TryParse(newStr, out value))
                {
                    setter(value);
                    return true;
                }
            }
            return false;
        }
    }

    public class UInt64MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly UInt64MessagePackPropertyDrawer Default = new UInt64MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.UInt64); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (UInt64)getter();
            var valueStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valueStr);
            if (valueStr != newStr)
            {
                if (UInt64.TryParse(newStr, out value))
                {
                    setter(value);
                    return true;
                }
            }
            return false;
        }
    }

    public class StringMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly StringMessagePackPropertyDrawer Default = new StringMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.String); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (String)getter();
            var newStr = EditorGUILayout.TextField(label, value);
            if (value != newStr)
            {
                setter(newStr);
                return true;
            }
            return false;
        }
    }

    public class EnumMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly EnumMessagePackPropertyDrawer Default = new EnumMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.Enum); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (Enum)getter();
            var selected = EditorGUILayout.EnumPopup(label, value);
            if (value != selected)
            {
                setter(selected);
                return true;
            }
            return false;
        }
    }
}