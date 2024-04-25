using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RDFSurveyForm.Data;
using RDFSurveyForm.JWT.AUTHENTICATION;
using RDFSurveyForm.JwtServices;
using RDFSurveyForm.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RDFSurveyForm.JWT.SERVICES
{
    public class UserService : IUserService
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;

        public UserService(
                            StoreContext context,
                            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public  AuthenticateResponse Authenticate(AuthenticateRequest request)
        {

            var user =  _context.Customer.Include(x=> x.Role).SingleOrDefault(x => x.UserName == request.UserName
                                                        && x.IsActive != false);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }

            var token = generateJwtToken(user);
            return  new AuthenticateResponse(user, token, _context);
        }

        private  string generateJwtToken(User user)
        {
            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {

                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("id", user.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials
               (new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
