using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            DrawDefaultInspector();
            using (new EditorGUILayout.HorizontalScope())
            {
                var exists = File.Exists(t.Filepath);
                using (new EditorGUI.DisabledScope(!exists))
                {
                    if (GUILayout.Button("Load"))
                    {
                        if (File.Exists(t.Filepath))
                        {
                            t.Load();
                        }
                        else
                        {
                            UnityEngine.Debug.LogErrorFormat("File:{0} is not exsits", t.Filepath);
                        }
                    }
                }

                if (GUILayout.Button("Load from ..."))
                {
                    var filepath = EditorUtility.OpenFilePanel("Load MessagePack File", GetOpenDirectory(t), null);
                    if(!string.IsNullOrEmpty(filepath))
                    {
                        if (!File.Exists(filepath))
                        {
                            UnityEngine.Debug.LogErrorFormat("File:{0} is not exsits", filepath);
                        }
                        else
                        {
                            //正しいファイルパスがきた
                            t.Filepath = filepath;
                            t.Load();
                        }
                    }
                }
                if (GUILayout.Button("Save"))
                {
                    t.Save();
                }
                if (GUILayout.Button("Save As ..."))
                {
                    var filepath = EditorUtility.SaveFilePanel("Save MessagePack File", GetOpenDirectory(t), "messagegpack", "byte");
                    if (!string.IsNullOrEmpty(filepath))
                    {
                        t.Filepath = filepath;
                        t.Save();
                    }
                }
            }
        }

        private string GetOpenDirectory(MessagePackScriptableObject t)
        {
            var filepath = t.Filepath;
            if (File.Exists(filepath))
                return Path.GetDirectoryName(filepath);

            return Application.dataPath;
        }
    }
}