namespace Hsc.ApiFramework.Enums
{
    /// <summary>
    /// Default Hsc User roles
    /// </summary>
    public enum HscUserRoles
    {
        /// <summary>
        /// Default superadmin role
        /// </summary>
        [HscEnumKey("54A63F2C-1D1F-4841-992B-B6A75C15F1EA")]
        SuperAdmin,

        /// <summary>
        /// Default Admin role
        /// </summary>
        [HscEnumKey("A554ACF2-D82E-4935-92E0-3A18C73B9CE5")]
        Admin,

        /// <summary>
        /// Default Moderator role
        /// </summary>
        [HscEnumKey("E3C55FD7-567F-4E63-961F-8F3E1951F8E6")]
        Moderator,

        /// <summary>
        /// Default user role
        /// </summary>
        [HscEnumKey("C98A4499-B60D-4FDE-926D-E4482FAFD69B")]
        User
    }
}