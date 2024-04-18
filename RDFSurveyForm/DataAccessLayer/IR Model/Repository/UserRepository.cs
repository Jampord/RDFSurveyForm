using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.DataAccessLayer.Interface;
using RDFSurveyForm.Dto.ModelDto.UserDto;
using RDFSurveyForm.Model;
using RDFSurveyForm.Services;

namespace RDFSurveyForm.DataAccessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _context;
        

        public UserRepository(StoreContext context)
        {
            _context = context;
            
        }

        public async Task<bool> UserAlreadyExist(string fullname)
        {
            var userAlreadyExist = await _context.Customer.AnyAsync(u => u.FullName == fullname);
            if (userAlreadyExist)
            {
                return false;
            }
            return true;
        }
        

        public async Task<bool> UserNameAlreadyExist(string username)
        {
            var userNameAlreadyExist = await _context.Customer.AnyAsync(u => u.UserName == username);
            if (userNameAlreadyExist)
            {
                return false;
            }
            return true;
        }


        public async Task<bool> AddNewUser(AddNewUserDto user)
        {



            var adduser = new User
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(user.UserName),
                //Password = user.UserName,
                GroupsId = user.GroupsId,
                CreatedAt = DateTime.Now,
                CreatedBy = user.CreatedBy,
                RoleId = user.RoleId,
                DepartmentId = user.DepartmentId,
                
            };
            await _context.Customer.AddAsync(adduser);
            await _context.SaveChangesAsync();


            return true;
        }


        public async Task<bool> UpdateUser(UpdateUserDto user)
        {


            var updateuser = await _context.Customer.FirstOrDefaultAsync(info => info.Id == user.Id);
            if (updateuser != null)
            {
                updateuser.UserName = user.UserName;
                updateuser.FullName = user.FullName;
                updateuser.RoleId = user.RoleId;
                updateuser.GroupsId = user.GroupsId;
                updateuser.EditedBy = user.EditedBy;
                updateuser.DepartmentId = user.DepartmentId;


                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }


        // hashpassword 
        //verify
        

        
        public async Task<PagedList<GetUserDto>> CustomerListPagnation(UserParams userParams, bool ? status, string search)
        {

            var result = _context.Customer.Select(x => new GetUserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                CreatedBy = x.CreatedBy,
                CreatedAt = DateTime.Now,
                InActive = x.IsActive,
                RoleId = x.RoleId,
                RoleName = x.Role.RoleName,
                GroupsId = x.GroupsId,
                GroupName = x.Groups.GroupName,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.DepartmentName,
                EditedBy = x.EditedBy,
                
                

            });

            if (status != null)
            {
                result = result.Where(x => x.InActive == status);
            }
           
            if(!string.IsNullOrEmpty(search))
            {
                result = result.Where(x => Convert.ToString(x.Id).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.FullName).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.UserName).ToLower().Contains(search.Trim().ToLower()));
            }
            return await PagedList<GetUserDto>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SetIsActive(int Id)
        {
            var setIsactive = await _context.Customer.FirstOrDefaultAsync(x => x.Id == Id);
            if (setIsactive != null)
            {
                setIsactive.IsActive = !setIsactive.IsActive;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        
        public async Task<bool> PasswordCheck(ChangePasswordDto users)
        {
            var password = await _context.Customer.FirstOrDefaultAsync(x => x.Id == users.Id);
            if(users.Password == password.UserName)
            {
                return true;
            }
            return false;
        }



        public async Task<bool> UpdatePassword(ChangePasswordDto user)
        {
            var updatepassword = await _context.Customer.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (updatepassword != null)
            {
                updatepassword.Password = BCrypt.Net.BCrypt.HashPassword(user.NewPassword);
                updatepassword.UpdatePass = true;
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
        //haschange
        public async Task<bool> ResetPassword(int Id)
        {
            var resetPassword = await _context.Customer.FirstOrDefaultAsync(x => x.Id == Id);
            if(resetPassword != null)
            {
                resetPassword.Password = BCrypt.Net.BCrypt.HashPassword(resetPassword.UserName);
                resetPassword.UpdatePass = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
