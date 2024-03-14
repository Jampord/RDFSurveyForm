using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.SetupDto.GroupDto;

namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Interface
{
    public interface IGroupRepository
    {
        Task<bool> GroupAlreadyExist(string groupName);
        Task<bool> AddGroup(AddGroupDto group);
        Task<bool> UpdateGroup(UpdateGroupDto group);
        Task<PagedList<GetGroupDto>> GroupListPagnation(UserParams userParams, bool? status, string search);
        Task<bool> DeleteGroup(int Id);
        Task<bool> SetInactive(int Id);
    }
}
