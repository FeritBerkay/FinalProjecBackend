using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    //Sifreleme olan sistemlerde, herseyi byte[] formatında vermemiz gerekir. Bu nedenle bu var. Secutirykeyi btye yaptı.
    public class SecurityKeyHelper
    {
        //SecurityKey appsettindeki tip.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            //SecurtiyKeyin bytenı aldık.
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
