using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsc.ApiFramework.Enums.Extensions
{
    /// <summary>
    /// Extension methods to get hsc enum descriptions
    /// </summary>
    public static class EnumExtensions
    {

        /// <summary>
        /// Gets value of Description attribute (System.ComponentModel). Falls back to the enum value name converted to string if not available.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetRoleName(this HscUserRoles value)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0
              ? ((DescriptionAttribute)attributes[0]).Description
              : value.ToString();
        }

        /// <summary>
        /// Gets value of HscEnumKey attribute. As this is used to generate initial keys for database generation, will throw an exception when the attribute is not assigned to the current enum value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetRoleKey(this HscUserRoles value)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(HscEnumKeyAttribute), false);
            return attributes.Length > 0
              ? ((HscEnumKeyAttribute)attributes[0]).Key
              : throw new Exception($"Role key attribute not added for role {value}");
        }

        /// <summary>
        /// Gets value of HscEnvironmentVariable Attribute. As this is used for the names of environment variables, will throw an exception when the attribute is not assigned to the current enum value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetEnvironmentKey(this HscSetting value)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(HscEnvironmentVariableAttribute), false);
            return attributes.Length > 0
              ? ((HscEnvironmentVariableAttribute)attributes[0]).Key
              : throw new Exception($"HscEnvironmentVariable attribute not added for {value}");
        }

        /// <summary>
        /// Gets Required value of HscEnvironmentVariable Attribute. As this is used for the names of environment variables, will throw an exception when the attribute is not assigned to the current enum value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool GetIsRequired(this HscSetting value)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(HscEnvironmentVariableAttribute), false);
            return attributes.Length > 0
              ? ((HscEnvironmentVariableAttribute)attributes[0]).Required
              : throw new Exception($"HscEnvironmentVariableAttribute attribute not added for {value}");
        }

    }
}
