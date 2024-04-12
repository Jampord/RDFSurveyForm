using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.SetupDto.BranchDto;

namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Interface
{
    public interface IBranchRepository
    {
        Task<bool> BranchAlreadyExist(string branchName);
        Task<bool> BranchCodeExist(string branchCode);
        Task<bool> AddBranch(AddBranchDto branch);
        Task<bool> UpdateBranch(UpdateBranchDto bran);
        Task<PagedList<GetBranchDto>> BranchListPagnation(UserParams userParams, bool? status, string search);
        Task<bool> SetIsactive(int Id);
        Task<bool> IsActiveValidation(int Id);

    }
}
