using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsc.ApiFramework.Enums
{
    /// <summary>
    /// All Hsc Setting types
    /// </summary>
    public enum HscSetting
    {
        #region JWT
        /// <summary>
        /// JWT Token Audience
        /// </summary>
        [HscEnvironmentVariable("HSC_AUTH_JWT_AUDIENCE", required: true)]
        HSC_AUTH_JWT_AUDIENCE,

        /// <summary>
        /// JWT Token ISSUER
        /// </summary>
        [HscEnvironmentVariable("HSC_AUTH_JWT_ISSUER", required: true)]
        HSC_AUTH_JWT_ISSUER,

        /// <summary>
        /// JWT Token secret
        /// </summary>
        [HscEnvironmentVariable("HSC_AUTH_JWT_SECRET", required: true)]
        HSC_AUTH_JWT_SECRET,

        /// <summary>
        /// JWT Token duration in hours
        /// </summary>
        [HscEnvironmentVariable("HSC_TOKEN_DURATION", required: false)]
        HSC_TOKEN_DURATION,
        #endregion

        #region Database
        /// <summary>
        /// Database connection string
        /// </summary>
        [HscEnvironmentVariable("HSC_DATABASE_CONNECTION", required: true)]
        HSC_DATABASE_CONNECTION,

        /// <summary>
        /// migration assembly name
        /// </summary>
        [HscEnvironmentVariable("HSC_MIGRATIONS_ASSEMBLY", required: false)]
        HSC_MIGRATIONS_ASSEMBLY,
        #endregion

        /// <summary>
        /// Initial superadmin user name 
        /// </summary>
        [HscEnvironmentVariable("HSC_INITIAL_USERNAME", required: true)]
        HSC_INITIAL_USERNAME,

        /// <summary>
        /// Initial superadmin password
        /// </summary>
        [HscEnvironmentVariable("HSC_INITIAL_PASSWORD", required: true)]
        HSC_INITIAL_PASSWORD,

        /// <summary>
        /// Initial superadmin email
        /// </summary>
        [HscEnvironmentVariable("HSC_INITIAL_EMAIL", required: false)]
        HSC_INITIAL_EMAIL,


        /// <summary>
        /// Initial superadmin email
        /// </summary>
        [HscEnvironmentVariable("HSC_INITIAL_PHONENUMBER", required: false)]
        HSC_INITIAL_PHONENUMBER
    }
}
