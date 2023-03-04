namespace auth.Services;

using auth.Authorization;
using auth.Data;
using auth.Helpers;
using auth.Interfaces;
using auth.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class UserService : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private readonly ApplicationDBContext _context;
    private readonly IConfiguration _configuration;
  
    private readonly IJwtUtils _jwtUtils;

    

    public UserService( ApplicationDBContext context, IConfiguration configuration, IJwtUtils jwtUtils)
    {
        _context = context;
        _configuration = configuration;
        _jwtUtils = jwtUtils;
        
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = _jwtUtils.GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.users;
    }

    public User GetById(int id)
    {
        return _context.users.FirstOrDefault(x => x.UserId == id);
    }




    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var claims = new Claim[]
                            {          
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            };

        var token_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
        var creds = new SigningCredentials(token_key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.Now.AddYears(7);
        var token = new JwtSecurityToken(_configuration["Jwt:Issuser"],
          _configuration["Jwt:Issuser"],
          claims,
          expires: expires,
          signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public void Register(RegisterRequest model)
    {
        if (_context.users.Any(x => x.UserName == model.UserName))
            throw new Exception("UserName '" + model.UserName + "' is already taken");
        // map model to new user object
        var user = new User() {
            UserName = model.UserName,
            Password = model.Password
        };


        //// hash password
        //user.PasswordHash = BCrypt.HashPassword(model.Password);

        _context.users.Add(user);
        _context.SaveChanges();
    }


}