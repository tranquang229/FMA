using System.Data;
using System.Reflection;
using Dapper;
using FMA.DAL.Context;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Common.Exceptions;
using FMA.Entities.Common.Settings;
using FMA.Entities.Dto;
using FMA.Entities.Enum;
using Microsoft.Extensions.Options;
using BCryptNet = BCrypt.Net.BCrypt;

namespace FMA.DAL.Implement;

public class AccountDataAccess : IAccountDataAccess
{
    private readonly IJwtUtils _jwtUtils;
    private readonly JwtSetting _jwtSetting;
    private readonly DapperContext _context;

    public AccountDataAccess(IJwtUtils jwtUtils, IOptions<JwtSetting> jwtSetting, DapperContext context)
    {
        _jwtUtils = jwtUtils;
        _jwtSetting = jwtSetting.Value;
        _context = context;
    }

    public async Task<Account> Register(RegisterRequest request)
    {
        using var connection = _context.CreateConnection();
        var user = new Account
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            PasswordHash = BCryptNet.HashPassword(request.Password),
            Email = request.Email,
            Phone = request.Phone
        };

        var account = await GetAccountByUsername(connection, request.Username);
        if (account != null)
        {
            throw new AppException("Account with this username is already existed");
        }
        var newAccountId = await connection.InsertAsync<long, Account>(user);
        user.Id = newAccountId;

        await InsertAccountRole(connection, user);

        return user;
    }

    private static async Task InsertAccountRole(IDbConnection connection, Account account)
    {
        var role = await connection.QueryFirstAsync<Role>("SELECT * FROM Roles WHERE Roles.Name = @RoleName",
            new { RoleName = EnumRole.User.ToString() });
        var p = new DynamicParameters();
        p.Add("@AccountId", account.Id);
        p.Add("@RoleId", role.Id);
        string sql = $@"INSERT INTO dbo.AccountRoles (AccountId, RoleId) VALUES (@AccountId, @RoleId)";

        await connection.ExecuteAsync(sql, p);
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        using var connection = _context.CreateConnection();

        var account = await GetAccountByUsername(connection, model.Username);
     
        // validate
        if (account == null || !BCryptNet.Verify(model.Password, account.PasswordHash))
            throw new AppException("Username or password is incorrect");

        // authentication successful so generate jwt token
        var jwtToken = await _jwtUtils.GenerateJwtToken(account);

        return new AuthenticateResponse(account, jwtToken);
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var users = await connection.GetListAsync<Account>();
        return users;
    }

    public async Task<Account> GetById(long accountId)
    {
        using var connection = _context.CreateConnection();
        var account = await connection.GetAsync<Account>(accountId);
        var roles = await GetRolesFromAccount(account.Id);
        account.Roles = roles.Select(x=>x.Name).ToList();
        account.Permissions = (await GetFullPermissionFromAccountId(accountId)).Select(x => x.Name).ToList();
     
        return account;
    }

    public async Task<List<Role>> GetRolesFromAccount(long accountId)
    {
        using var connection = _context.CreateConnection();
        var p = new
        {
            AccountId = accountId
        };
        var roles = await connection.QueryAsync<Role>("GetRolesFromAccount",  p, commandType:CommandType.StoredProcedure);
        
        return roles.ToList();
    }
    
    public async Task<List<Permission>> GetFullPermissionFromAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
        var p = new
        {
            AccountId = accountId
        };
        var permissions = await connection.QueryAsync<Permission>("GetFullPermissionFromAccountId", p, commandType:CommandType.StoredProcedure);

        return permissions.ToList();
    }


    private async Task<Account> GetAccountByUsername(IDbConnection connection, string userName)
    {
        var accounts = await connection.GetListAsync<Account>(new { Username = userName });
        var account = accounts.FirstOrDefault();
        if (account != null)
        {
            var roles = await GetRolesFromAccount(account.Id);
            account.Roles = roles.Select(x=>x.Name).ToList();

            var permissions = await GetFullPermissionFromAccountId(account.Id);
            account.Permissions = permissions.Select(x => x.Name).ToList();
        }
        
        return account;
    }
}