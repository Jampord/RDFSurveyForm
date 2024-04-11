using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.Unit_SubUnitDto;
using RDFSurveyForm.Model;

namespace RDFSurveyForm.DataAccessLayer.IR_Unit_Subunit.Interface
{
    public interface IUnitRepository
    {
        Task<bool> ExistingUnit(string unit);
        Task<bool> AddUnit(AddUnitDto unit);
        Task<bool> UpdateUnit(UpdateUnitDto unit);
        Task<bool> SetIsActive(int Id);
        Task<PagedList<GetUnitDto>> UnitListPagination(UserParams userParams, bool? status, string search);

    }
}
