using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.Unit_SubUnitDto;

namespace RDFSurveyForm.DataAccessLayer.IR_Unit_Subunit.Interface
{
    public interface ISubunitRepository
    {
        Task<bool> ExistingUnit(string subunit);
        Task<bool> AddSubunit(AddSubunitDto subunit);
        Task<bool> UpdateSubunit(UpdateSubunitDto subunit);
        Task<bool> SetIsActive(int Id);
        Task<PagedList<GetSubunitDto>> SubunitListPagination(UserParams userParams, bool? status, string search);

    }
}
