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
            Register(SingleMessagePackPropertyDrawer.Default);
            Register(DoubleMessagePackPropertyDrawer.Default);
            Register(StringMessagePackPropertyDrawer.Default);
            Register(EnumMessagePackPropertyDrawer.Default);

            //nullable
            Register(NullableByteMessagePackPropertyDrawer.Default);
            Register(NullableSByteMessagePackPropertyDrawer.Default);
            Register(NullableBooleanMessagePackPropertyDrawer.Default);
            Register(NullableInt16MessagePackPropertyDrawer.Default);
            Register(NullableInt32MessagePackPropertyDrawer.Default);
            Register(NullableInt64MessagePackPropertyDrawer.Default);
            Register(NullableUInt16MessagePackPropertyDrawer.Default);
            Register(NullableUInt32MessagePackPropertyDrawer.Default);
            Register(NullableUInt64MessagePackPropertyDrawer.Default);
            Register(NullableSingleMessagePackPropertyDrawer.Default);
            Register(NullableDoubleMessagePackPropertyDrawer.Default);
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

    public class SingleMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly SingleMessagePackPropertyDrawer Default = new SingleMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.Single); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (Single)getter();
            var valueStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valueStr);
            if (valueStr != newStr)
            {
                if (Single.TryParse(newStr, out value))
                {
                    setter(value);
                    return true;
                }
            }
            return false;
        }
    }

    public class DoubleMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly DoubleMessagePackPropertyDrawer Default = new DoubleMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(System.Double); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (Double)getter();
            var valueStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valueStr);
            if (valueStr != newStr)
            {
                if (Double.TryParse(newStr, out value))
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

    public class NullableByteMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableByteMessagePackPropertyDrawer Default = new NullableByteMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(byte?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (byte?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if(string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                byte tmp;
                if (Byte.TryParse(newStr, out tmp))
                {
                    setter((byte?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableSByteMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableSByteMessagePackPropertyDrawer Default = new NullableSByteMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(sbyte?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (sbyte?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                sbyte tmp;
                if (SByte.TryParse(newStr, out tmp))
                {
                    setter((sbyte?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableBooleanMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableBooleanMessagePackPropertyDrawer Default = new NullableBooleanMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(bool?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (bool?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                bool tmp;
                if (Boolean.TryParse(newStr, out tmp))
                {
                    setter((bool?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableInt16MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableInt16MessagePackPropertyDrawer Default = new NullableInt16MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(short?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (short?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                short tmp;
                if (Int16.TryParse(newStr, out tmp))
                {
                    setter((short?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableInt32MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableInt32MessagePackPropertyDrawer Default = new NullableInt32MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(int?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (int?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                int tmp;
                if (Int32.TryParse(newStr, out tmp))
                {
                    setter((int?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableInt64MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableInt64MessagePackPropertyDrawer Default = new NullableInt64MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(long?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (long?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                long tmp;
                if (Int64.TryParse(newStr, out tmp))
                {
                    setter((long?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableUInt16MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableUInt16MessagePackPropertyDrawer Default = new NullableUInt16MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(ushort?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (ushort?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                ushort tmp;
                if (UInt16.TryParse(newStr, out tmp))
                {
                    setter((ushort?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableUInt32MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableUInt32MessagePackPropertyDrawer Default = new NullableUInt32MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(uint?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (uint?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                uint tmp;
                if (UInt32.TryParse(newStr, out tmp))
                {
                    setter((uint?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableUInt64MessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableUInt64MessagePackPropertyDrawer Default = new NullableUInt64MessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(ulong?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (ulong?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                ulong tmp;
                if (UInt64.TryParse(newStr, out tmp))
                {
                    setter((ulong?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableSingleMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableSingleMessagePackPropertyDrawer Default = new NullableSingleMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(float?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (float?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                float tmp;
                if (Single.TryParse(newStr, out tmp))
                {
                    setter((float?)tmp);
                    return true;
                }
            }
            return false;
        }
    }

    public class NullableDoubleMessagePackPropertyDrawer : IMessagePackPropertyDrawer
    {
        public static readonly NullableDoubleMessagePackPropertyDrawer Default = new NullableDoubleMessagePackPropertyDrawer();

        public Type TargetType
        {
            get { return typeof(double?); }
        }

        public bool DrawField(string label, Func<object> getter, Action<object> setter, Type type)
        {
            var value = (double?)getter();
            var valuteStr = value.ToString();
            var newStr = EditorGUILayout.TextField(label, valuteStr);
            if (valuteStr != newStr)
            {
                if (string.IsNullOrEmpty(newStr))
                {
                    setter(null);
                    return true;
                }
                double tmp;
                if (Double.TryParse(newStr, out tmp))
                {
                    setter((double?)tmp);
                    return true;
                }
            }
            return false;
        }
    }
}