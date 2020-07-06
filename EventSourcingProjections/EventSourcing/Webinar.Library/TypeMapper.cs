using System;
using System.Collections.Generic;

namespace Webinar.Library
{
    public static class TypeMapper
    {
        static readonly Dictionary<string, Type> NameTypeMap = new Dictionary<string, Type>();
        static readonly Dictionary<Type, string> TypeNameMap = new Dictionary<Type, string>();

        public static void Map<T>(string name)
        {
            NameTypeMap.Add(name, typeof(T));
            TypeNameMap.Add(typeof(T), name);
        }

        public static string GetName(object obj) => TypeNameMap[obj.GetType()];

        public static Type GetType(string name) => NameTypeMap[name];
    }
}
