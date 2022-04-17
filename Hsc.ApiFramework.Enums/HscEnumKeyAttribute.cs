namespace Hsc.ApiFramework.Enums
{
    /// <summary>
    /// Attribute to assign custom keys to enum values. This is used for assigning Role Id's upon database creation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class HscEnumKeyAttribute : Attribute
    {
        /// <summary>
        /// Value to use as Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Attribute to assign custom keys to enum values. This is used for assigning Role Id's upon database creation.
        /// </summary>
        public HscEnumKeyAttribute(string key)
        {
            Key = key;
        }
    }
}