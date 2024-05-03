using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.SetupDto.GroupSurveyDto;

namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Interface
{
    public interface IGroupSurveyRepository
    {
        Task<bool> AddSurvey(AddGroupSurveyDto survey);
        Task<bool> GroupIdDoesnotExist(int? Id);
        Task<PagedList<GetGroupSurveyDto>> GroupSurveyPagination(UserParams userParams, bool? status, string search);
        Task<IReadOnlyList<ViewSurveyDto>> ViewSurvey(int ? id);
        Task<bool> ScoreLimit(AddGroupSurveyDto limit);
        Task<bool> UpdateScore(UpdateSurveyScoreDto score);
        Task<bool> SetIsActive(int Id);
    }
}
