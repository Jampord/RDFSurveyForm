using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.DepartmentDto;

namespace RDFSurveyForm.DataAccessLayer.Interface
{
    public interface IDepartmentRepository
    {
        Task<bool> ExistingDepartment(string department);
        Task<bool> AddDepartment(AddDepartmentDto department);
        Task<bool> UpdateDepartment(UpdateDepartmentDto department);
        Task<bool> SetInActive(int Id);
        Task<PagedList<GetDepartmentDto>> CustomerListPagnation(UserParams userParams, bool ? status, string search);

    }
}
