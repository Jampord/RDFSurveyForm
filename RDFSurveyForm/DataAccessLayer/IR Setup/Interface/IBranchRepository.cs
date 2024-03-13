using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.SetupDto.BranchDto;

namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Interface
{
    public interface IBranchRepository
    {
        Task<bool> BranchAlreadyExist(string branchName);
        Task<bool> AddBranch(AddBranchDto branch);
        Task<bool> UpdateBranch(UpdateBranchDto bran);
        Task<PagedList<GetBranchDto>> CustomerListPagnation(UserParams userParams, bool? status, string search);
        Task<bool> DeleteBranch(int Id);
        Task<bool> SetInactive(int Id);

    }
}
