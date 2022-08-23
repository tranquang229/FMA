using System.Data;
using System.Reflection;
using Dapper;
using FMA.DAL.Context;
using FMA.DAL.Interface;
using FMA.Entities;
using FMA.Entities.Common.Exceptions;
using FMA.Entities.Common.Settings;
using FMA.Entities.Dto;
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
            Role = Role.User
        };

        var account = await GetAccountByUsername(connection, request.Username);
        if (account != null)
        {
            throw new AppException("Account with this username is already existed");
        }
        var newId = await connection.InsertAsync<long, Account>(user);
        user.Id = newId;
        
        return user;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        using var connection = _context.CreateConnection();

        var account = await GetAccountByUsername(connection, model.Username);

        // validate
        if (account == null || !BCryptNet.Verify(model.Password, account.PasswordHash))
            throw new AppException("Username or password is incorrect");

        // authentication successful so generate jwt token
        var jwtToken = _jwtUtils.GenerateJwtToken(account);

        return new AuthenticateResponse(account, jwtToken);
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var users = await connection.GetListAsync<Account>();
        return users;
    }

    public async Task<Account> GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var user = await connection.GetAsync<Account>(id);
        return user;
    }

    private async Task<Account> GetAccountByUsername(IDbConnection connection, string userName)
    {
        var accounts = await connection.GetListAsync<Account>(new { Username = userName });
        var account = accounts.FirstOrDefault();
        return account;
    }
}