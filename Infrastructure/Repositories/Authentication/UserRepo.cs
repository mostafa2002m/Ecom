using Application.Dtos.Request.Account;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Authentication;
using Domain.Entities.Authentication;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using static Application.Dtos.Responses.ServiceRersponse;

namespace Infrastructure.Repositories.Authentication
{
    //public class UserRepo(AppDbContext dbContext, IConfiguration configuration) : IUserRepo  
       
    //{
      
    //    public Task AddAsync(ApplicationUser entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task DeleteAsync(int entityId)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<ApplicationUser>> GetAllAsync(string[] includes = null, Expression<Func<ApplicationUser, bool>> expression = null, IOrderedEnumerable<ApplicationUser> orderBy = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<ApplicationUser> GetAsync(Expression<Func<ApplicationUser, bool>> expression, string[] includes = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<ApplicationUser> GetByEmailAsync(string email)
    //    {
    //       var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    //        return user;
    //    }

    //    public Task<ApplicationUser> GetByPhoneNumberAsync(string phoneNumber)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<ApplicationUser> GetByUsernameAsync(string username)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> IsEmailUniqueAsync(string email)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> IsUsernameUniqueAsync(string username)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<RegisterResponse> RegisterAsync(RegisterModelDto model)
    //    {
    //        var getUser = await FindUserByEmail(model.Email!);
    //        if (getUser != null)
    //            return new RegisterResponse(false, ["User Already Exists"]);

    //        dbContext.Users.Add(new ApplicationUser()
    //        {
    //            Email = model.Email,
    //            Name = model.Name,
    //            Password = BCrypt.Net.BCrypt.HashPassword(model.Password)

    //        });
    //        dbContext.SaveChanges();
    //        return new RegisterResponse(true, ["User Registered"]);


    //    }

    //    public async Task<LoginResponse> LoginAsync(LoginModelDto model)
    //    {

    //        if (model is null)
    //            return new LoginResponse(false, ["model is empty"]);

    //        var getUser = await FindUserByEmail(model.Email);
    //        if (getUser == null)
    //            return new LoginResponse(false, ["user not found"]);

    //        bool checkPassword = BCrypt.Net.BCrypt.Verify(model.Password, getUser.Password);
    //        if (checkPassword)

    //            return new LoginResponse(true, ["Login Successful"], GenerateToken(getUser));
    //        else return new LoginResponse(false, ["Invalid Credential"]);

    //    }

    //    private async Task<ApplicationUser> FindUserByEmail(string email) =>
    //        await dbContext.Users.FirstOrDefaultAsync(_ => _.Email == email);

    //    private string GenerateToken(ApplicationUser user)
    //    {
    //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:securityKey"]!));
    //        var credintial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //        var userClaims = new[]
    //        {
    //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //            new Claim(ClaimTypes.Name, user.Name),
    //            new Claim(ClaimTypes.Email, user.Email)
    //        };

    //        var token = new JwtSecurityToken(
    //           issuer: configuration["Jwt:Issuer"],
    //           audience: configuration["Jwt:Audience"],
    //           signingCredentials: credintial,
    //           claims: userClaims,
    //           expires: DateTime.Now.AddDays(5));

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }

    //    public Task UpdateAsync(ApplicationUser entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
