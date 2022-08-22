using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FMA.DAL.Implement;

public class JwtUtils : IJwtUtils
{
    private readonly JwtSetting _jwtSetting;

    public JwtUtils(IOptions<JwtSetting> jwtSetting)
    {
        _jwtSetting = jwtSetting.Value;
    }

    public string GenerateJwtToken(Account account)
    {
        var signingCredentials = GetSigningCredential();
        var claims = GetClaims(account);
        var token = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public long? ValidateJwtToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSetting.Key);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);


            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = long.Parse(jwtToken.Claims.First(x => x.Type == Constants.UId).Value);

            // return user id from JWT token if validation successful
            return userId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }
    private SecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSetting.DurationInMinutes));
        var token = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            claims: claims,
            expires: expiration,
            signingCredentials: signingCredentials);

        return token;
    }
    private List<Claim> GetClaims(Account account)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, account.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, account.Email ??""),
            new Claim(Constants.ROLE, account.Role.ToString()),
            new Claim(Constants.UId, account.Id.ToString())
        };

        return claims.ToList();
    }

    private SigningCredentials GetSigningCredential()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
       
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
}