using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.RoleDto;

namespace RDFSurveyForm.DataAccessLayer.Interface
{
    public interface IRoleRepository
    {
        Task<bool> RoleExist(string role);
        Task<bool> AddNewRole(AddRoleDto role);
        Task<bool> UpdateRole(UpdateRoleDto role);
        Task<bool> SetIsActive(int Id);
        Task<PagedList<GetRoleDto>> CustomerListPagnation(UserParams userParams, bool? status, string search);
        Task<bool> UpdatedPermission(UpdateRoleDto role);
        Task<bool> IsActiveValidation(int Id);

    }
}
