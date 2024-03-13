using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.DepartmentDto;

namespace RDFSurveyForm.DataAccessLayer.Interface
{
    public interface IDepartmentRepository
    {
        Task<bool> AddDepartment(AddDepartmentDto department);
        Task<bool> UpdateDepartment(UpdateDepartmentDto department);
        Task<bool> DeleteDepartment(int Id);
        Task<bool> SetInActive(int Id);
        Task<PagedList<GetDepartmentDto>> CustomerListPagnation(UserParams userParams, bool ? status, string search);

    }
}
