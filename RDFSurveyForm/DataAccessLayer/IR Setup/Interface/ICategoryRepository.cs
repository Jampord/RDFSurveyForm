using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.SetupDto.CategoryDto;

namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Interface
{
    public interface ICategoryRepository
    {
        Task<bool> CategoryAlreadyExist(string categoryName);
        Task<bool> AddCategory(AddCategoryDto category);
        Task<bool> UdpateCategory(UpdateCategoryDto category);
        Task<PagedList<GetCategoryDto>> CategoryPagnation(UserParams userParams, bool? status, string search);
        Task<bool> DeleteCategory(int Id);
        Task<bool> SetInactive(int Id);
        Task<bool> PercentageChecker(AddCategoryDto category);

    }
}
