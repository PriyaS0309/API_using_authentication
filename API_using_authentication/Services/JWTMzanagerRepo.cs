using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace API_using_authentication.Services
{
    public class JWTMzanagerRepo : IJWTManagerRepo
    {
        private DepartmentDbContext departmentDbContext;
        private IConfiguration configuration;
        public JWTMzanagerRepo(DepartmentDbContext departmentDbContext, IConfiguration configuration)
        {
            this.departmentDbContext = departmentDbContext;
            this.configuration = configuration;
        }
        

        public Tokens Authenticate(Users user)
        {
            if (departmentDbContext.User.Any(model => model.Username != user.Username && model.Password != user.Password))
            {
                return null;
            }
            else
            {
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, user.Username) }

                        ),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)
                };

                //creating token
                var token = tokenhandler.CreateToken(tokenDescriptor);

                return new Tokens { Token = tokenhandler.WriteToken(token) };
            }

        }
    }
}
