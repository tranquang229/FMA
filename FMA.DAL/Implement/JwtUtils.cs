
using System.Collections;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using FMA.DAL.Context;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using static Dapper.SqlMapper;

namespace FMA.DAL.Implement;

public class JwtUtils : IJwtUtils
{
    private readonly JwtSetting _jwtSetting;
    private readonly DapperContext _context;

    public JwtUtils(IOptions<JwtSetting> jwtSetting, DapperContext context)
    {
        _context = context;
        _jwtSetting = jwtSetting.Value;
    }

    public async Task<string> GenerateJwtToken(Account account)
    {
        var signingCredentials = GetSigningCredential();
        var claims = await GetClaims(account);
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
    private async Task<List<Claim>> GetClaims(Account account)
    {
        using var connection = _context.CreateConnection();
        var p = new
        {
            AccountId = account.Id
        };

        //var userRoles = await connection.QueryAsync<>().GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, account.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, account.Email ??""),
            new Claim(Constants.UId, account.Id.ToString()),
    };

        var roles = await connection.QueryAsync<Role>("GetRolesFromAccount", p, commandType: CommandType.StoredProcedure);
        foreach (var userRole in roles)
        {
            authClaims.Add(new Claim(Constants.ROLES, userRole.Name));
        }
      
        var permissions = await connection.QueryAsync<Permission>("GetFullPermissionFromAccountId", p, commandType: CommandType.StoredProcedure);
        foreach (var userPermission in permissions)
        {
            authClaims.Add(new Claim(Constants.PERMISSIONS, userPermission.Name));
        }

        return authClaims.ToList();
    }

    private SigningCredentials GetSigningCredential()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
}