using Core.Entities;
using Core.Extentios;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

public partial class JwtHelper : ITokenHelper
{
    //APP json dosyasını okumamıza yarar. Configuration
    public IConfiguration Configuration { get; }
    //Tokenın degerleri Aldık.
    private TokenOptions _tokenOptions;
    //Token ne zaman gecersiz olucak.
    private DateTime _accessTokenExpiration;
    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        //Configurationlarımdaki alanları bul. Sectionları yani TokenOptions bolumu ile baslayarak. Ardından TokenOptionslarındaki degelere atadık.
        //Yani json to class.
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

    }
    public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
    {
        //Token ne zaman iptal olucak. Onu aldık. jston to class yaptıgımız yerden.
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        //Securitykeyi kullanarak securitykey olsuturduk.
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        //Hangi anahtar ve algoritma. Bizim yazdıgımızı kullandık.
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        //Verilen bilgilere gore tokeni olusturdu.
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
        //Olusturdugum jwt yi atadım.
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //Olusturulan tokenını yazdırdık.
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };

    }
    //Verdigim bilgilere gore. Yeni bir jason web token olusturdum. 
    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
        SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
    {
        var jwt = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, operationClaims),
            signingCredentials: signingCredentials
        );
        return jwt;
    }
    //Bana bilgileri alarak claim listesi olustur.
    private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddName($"{user.FirstName} {user.LastName}");
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

        return claims;
    }
}
