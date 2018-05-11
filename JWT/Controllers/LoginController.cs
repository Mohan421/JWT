using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{

    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetToken(string userName, string password)
        {
            LoginModel loginModel = new LoginModel();
            loginModel.Username = userName;
            loginModel.Password = password;
            IActionResult response = Unauthorized();
            var user = Authenticate(loginModel);
            if (user != null)
            {
                var tokenString = GenerateToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
        private string GenerateToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddHours(1),
              signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginModel login)
        {
            UserModel user = null;
            if (login.Username == "Admin" && login.Password == "123456")
            {
                user = new UserModel { Name = "Admin", Email = "Admin@admin.com" };
            }
            return user;
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private class UserModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}