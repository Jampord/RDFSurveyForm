using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.Model;

namespace RDFSurveyForm.JWT.AUTHENTICATION
{
    public class AuthenticateResponse
    {
        private readonly StoreContext _context;



        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
        public int? Role { get; set; }
        public string UserRoleName { get; set; }
        public ICollection<string> Permission {  get; set; }
        public bool UpdatePass { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token, StoreContext context)
        {
              _context = context;

            Id = user.Id;
            FullName = user.FullName;
            UserName = user.UserName;
            //Password = user.Password;
            Role = user.RoleId;
            UpdatePass = user.UpdatePass;
            Permission = user.Role.Permission;
            Token = token;

            var role = _context.Customer.Include(x => x.Role).FirstOrDefaultAsync(x => x.RoleId == Role);
            if (role != null)
            {
                UserRoleName = user.Role.RoleName;
            }
        }


    }
}
