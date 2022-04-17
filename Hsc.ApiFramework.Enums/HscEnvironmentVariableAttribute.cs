namespace Hsc.ApiFramework.Enums
{
    /// <summary>
    /// Attribute to assign environment variable keys from enum values. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class HscEnvironmentVariableAttribute : Attribute
    {
        /// <summary>
        /// Environment variable name
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Whether the environment variable is required
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Assigns Hsc Environment variable parameters
        /// </summary>
        /// <param name="key"></param>
        /// <param name="required"></param>
        public HscEnvironmentVariableAttribute(string key, bool required = true)
        {
            Key = key;
            Required = required;
        }
    }
}