using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.SetupDto.QuestionsDto;

namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Interface
{
    public interface IQuestionsRepository
    {
        Task<bool> QuestionAlreadyExist(string question);
        Task<bool> AddQuestions(AddQuestionsDto question);
        Task<bool> UpdateQuestions(UpdateQuestionsDto questions);
        Task<PagedList<GetQuestionsDto>> QuestionListPagnation(UserParams userParams, bool? status, string search);
        Task<bool> DeleteQuestion(int Id);
        Task<bool> SetInactive(int Id);
    }
}
