using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using MessagePack;

namespace MessagePackEditor
{
    [InitializeOnLoad]
    class MessagePackTypeCache
    {
        #region static
        public static readonly MessagePackTypeCache Instance = new MessagePackTypeCache();
        static MessagePackTypeCache()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(asm => !asm.FullName.StartsWith("mscorlib,"))
                .Where(asm => !asm.FullName.StartsWith("System,"))
                .Where(asm => !asm.FullName.StartsWith("System."))
                .Where(asm => !asm.FullName.StartsWith("nunit.framework"))
                .Where(asm => !asm.FullName.StartsWith("ICSharpCode.NRefactory,"))
                .Where(asm => !asm.FullName.StartsWith("ExCSS.Unity,"))
                .Where(asm => !asm.FullName.StartsWith("Unity"))
                .Where(asm => !asm.FullName.StartsWith("SyntaxTree"))
                .Where(asm => !asm.FullName.StartsWith("Mono.Security,"))
                .Where(asm => !asm.IsDynamic)
                .SelectMany(asm => asm.GetExportedTypes())
                .ToArray();

            foreach (var t in types)
            {
                //cache union
                var unions = t.GetCustomAttributes<UnionAttribute>()
                    .OrderBy(attr => attr.Key)
                    .ToArray();

                if (unions != null && unions.Length > 0)
                {
                    Instance.UnionCache.Add(t, unions);
                }

                //next cache [Key] property
                //type should be concrete
                if (t.IsInterface || t.IsAbstract)
                    continue;

                if (t.GetCustomAttribute<MessagePackObjectAttribute>() == null)
                    continue;

                var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(prop => prop.CanRead && prop.CanWrite) //requires get; set;
                    .Where(prop => prop.GetCustomAttribute<KeyAttribute>() != null)
                    .ToArray();

                Instance.PropertyCache.Add(t, props);
            }
        }
        #endregion

        public Dictionary<Type, UnionAttribute[]> UnionCache { get; } = new Dictionary<Type, UnionAttribute[]>();
        public Dictionary<Type, PropertyInfo[]> PropertyCache { get; } = new Dictionary<Type, PropertyInfo[]>();

        public UnionAttribute[] GetUnions(Type type)
        {
            UnionAttribute[] unions = null;
            UnionCache.TryGetValue(type, out unions);
            return unions;
        }

        private MessagePackTypeCache()
        {
        }

        public bool IsCached(Type t)
        {
            return UnionCache.ContainsKey(t) || PropertyCache.ContainsKey(t);
        }

        public PropertyInfo[] GetProperties(Type type, int unionKey = 0)
        {
            PropertyInfo[] result = null;
            this.PropertyCache.TryGetValue(type, out result);
            return result;
        }
    }
}
