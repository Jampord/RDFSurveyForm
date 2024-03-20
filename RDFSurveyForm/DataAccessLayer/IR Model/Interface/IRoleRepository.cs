using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.RoleDto;

namespace RDFSurveyForm.DataAccessLayer.Interface
{
    public interface IRoleRepository
    {
        Task<bool> AddNewRole(AddRoleDto role);
        Task<bool> UpdateRole(UpdateRoleDto role);
        Task<bool> SetInActive(int Id);
        Task<PagedList<GetRoleDto>> CustomerListPagnation(UserParams userParams, bool? status, string search);
        Task<bool> UpdatedPermission(UpdateRoleDto role);

    }
}
