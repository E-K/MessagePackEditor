using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MessagePackEditor
{
    [CustomEditor(typeof(MessagePackScriptableObject), editorForChildClasses: true)]
    public class MessagePackScriptableObjectInspector : UnityEditor.Editor
    {
        private DefaultMessagePackPropertyDrawer _drawer = null;

        private void OnEnable()
        {
            _drawer = new DefaultMessagePackPropertyDrawer(useFoldout: false);
        }

        public override void OnInspectorGUI()
        {
            var t = (MessagePackScriptableObject)target;
            bool edit = _drawer.DrawField("", () => t.SerializeObject, (newValue) => t.SerializeObject = newValue, t.SerializeType);
            if (edit)
                EditorUtility.SetDirty(target);
        }
    }
}