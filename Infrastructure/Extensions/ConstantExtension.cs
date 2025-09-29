using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ConstantExtension
    {
        private static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        public static IEnumerable<T> GetConstantsValues<T>(this Type type) where T : class
        {
            var fieldInfos = type.GetConstants();

            return fieldInfos.Select(fi => fi.GetRawConstantValue() as T);
        }
    }
}
