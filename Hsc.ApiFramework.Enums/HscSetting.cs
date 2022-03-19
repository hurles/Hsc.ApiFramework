using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsc.ApiFramework.Enums
{
    public enum HscSetting
    {
        /// <summary>
        /// JWT Token Audience
        /// </summary>
        HSC_AUTH_JWT_AUDIENCE,

        /// <summary>
        /// JWT Token ISSUER
        /// </summary>
        HSC_AUTH_JWT_ISSUER,

        /// <summary>
        /// JWT Token secret
        /// </summary>
        HSC_AUTH_JWT_SECRET,

        /// <summary>
        /// JWT Token duration in hours
        /// </summary>
        HSC_TOKEN_DURATION,

        /// <summary>
        /// Database connection string
        /// </summary>
        HSC_DATABASE_CONNECTION


    }
}
