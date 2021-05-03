using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //Bi token var bu teken ile baglantı kurulucak. Bu tokenın abidik gubidik bir string gi var o tek. DateTime da gecerli oldugu sure.
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
