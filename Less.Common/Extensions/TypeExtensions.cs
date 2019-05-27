//bibaoke.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Less
{
    /// <summary>
    /// Type 的扩展方法
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 获取类型的属性和字段
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IEnumerable<MemberInfo> GetPropertiesAndFields(this Type t)
        {
            List<MemberInfo> list = new List<MemberInfo>();

            if (t.BaseType.IsNotNull())
            {
                list.AddRange(t.BaseType.GetPropertiesAndFields());
            }

            MemberInfo[] members = t.GetMembers(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.DeclaredOnly);

            list.AddRange(members.Where(i => i.MemberType == MemberTypes.Property || i.MemberType == MemberTypes.Field));

            return list;
        }
    }
}
