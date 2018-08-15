using System;
using UnityEngine;

namespace MessagePackEditor
{
    public abstract class MessagePackScriptableObject : ScriptableObject
    {
        public abstract object SerializeObject { get; set; }
        public abstract Type SerializeType { get; }

        [SerializeField]
        private byte[] _bytes = null;
        public byte[] Bytes
        {
            get { return _bytes; }
            protected set { _bytes = value; }
        }
    }

    public abstract class MessagePackScriptableObject<T> : MessagePackScriptableObject, ISerializationCallbackReceiver
    {
        public T Value { get; set; }

        public override object SerializeObject
        {
            get { return Value; }
            set { Value = (T)value; }
        }

        public override Type SerializeType
        {
            get { return typeof(T); }
        }

        public void OnAfterDeserialize()
        {
            Value = MessagePack.MessagePackSerializer.Deserialize<T>(Bytes);
        }

        public void OnBeforeSerialize()
        {
            Bytes = MessagePack.MessagePackSerializer.Serialize<T>(Value);
        }
    }
}