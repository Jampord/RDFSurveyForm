using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.UserDto;

namespace RDFSurveyForm.DataAccessLayer.Interface
{
    public interface IUserRepository
    {
        Task<bool> UserAlreadyExist(string fullname);
        Task<bool> UserNameAlreadyExist(string username);
        Task<bool> AddNewUser(AddNewUserDto user);
        Task<bool> UpdateUser(UpdateUserDto user);
        Task<PagedList<GetUserDto>> CustomerListPagnation(UserParams userParams, bool? status, string search);
        Task<bool> SetInActive(int Id);
        Task<bool> PasswordCheck(ChangePasswordDto users);
        Task<bool> UpdatePassword(ChangePasswordDto user);
        Task<bool> ResetPassword(int Id);



    }
}
