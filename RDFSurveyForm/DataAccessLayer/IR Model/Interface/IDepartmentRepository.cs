using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.DepartmentDto;
using RDFSurveyForm.Model;

namespace RDFSurveyForm.DataAccessLayer.Interface
{
    public interface IDepartmentRepository
    {
        Task<bool> ExistingDepartment(string department);
        Task<bool> AddDepartment(AddDepartmentDto department);
        Task<bool> UpdateDepartment(UpdateDepartmentDto department);
        Task<bool> SetIsActive(int Id);
        Task<PagedList<GetDepartmentDto>> CustomerListPagnation(UserParams userParams, bool ? status, string search);
        Task<bool> IsActiveValidation(int Id);
        Task<Department> GetByDepartmentNo(int? departmentNo);
    }
}
