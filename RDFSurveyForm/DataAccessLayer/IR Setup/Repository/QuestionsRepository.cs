using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.DataAccessLayer.IR_Setup.Interface;
using RDFSurveyForm.Dto.SetupDto.QuestionsDto;
using RDFSurveyForm.Setup;

namespace RDFSurveyForm.DataAccessLayer.IR_Setup.Repository
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly StoreContext _context;

        public QuestionsRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<bool> QuestionAlreadyExist(string question)
        {
            var questionAlreadyExist = await _context.Question.AnyAsync(x =>  x.Question == question);
            if(questionAlreadyExist)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddQuestions(AddQuestionsDto question)
        {
            var addQuestion = new Questions
            {
                Id = question.Id,
                Question = question.Question,
                CreatedAt = DateTime.Now,
                CreatedBy = question.CreatedBy,
                CategoryId = question.CategoryId,
            };
            await _context.Question.AddAsync(addQuestion);
            await _context.SaveChangesAsync(); 
            return true;
        }

        public async Task<bool> UpdateQuestions(UpdateQuestionsDto questions)
        {
            var updateQuestions = await _context.Question.FirstOrDefaultAsync(x => x.Id == questions.Id);
            if(updateQuestions != null)
            {
                updateQuestions.Question = questions.Question;
                updateQuestions.UpdatedAt = DateTime.Now;
                updateQuestions.UpdatedBy = questions.UpdatedBy;
                updateQuestions.CategoryId = questions.CategoryId;
                
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PagedList<GetQuestionsDto>> QuestionListPagnation(UserParams userParams, bool ? status, string search)
        {
            var users = _context.Question.Select(x => new GetQuestionsDto
            {
                Id= x.Id,
                Question = x.Question,
                IsActive = x.IsActive,
                CreatedAt= x.CreatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy,
                CategoryId = x.CategoryId,
            });

            if(status != null)
            {
                users = users.Where(x => x.IsActive == status);
            }

            if(!string.IsNullOrEmpty(search))
            {
                users = users.Where(x => Convert.ToString(x.Id).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.Question).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.CreatedBy).ToLower().Contains(search.Trim().ToLower())
                || Convert.ToString(x.UpdatedBy).ToLower().Contains(search.Trim().ToLower()));
            }
            return await PagedList<GetQuestionsDto>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> DeleteQuestion(int Id)
        {
            var deleteQuestion = await _context.Question.FirstOrDefaultAsync(x => x.Id == Id);
            if(deleteQuestion != null)
            {
                _context.Question.Remove(deleteQuestion);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> SetInactive(int Id)
        {
            var setInactive = await _context.Question.FirstOrDefaultAsync(x => x.Id == Id);
                if(setInactive != null)
            {
                setInactive.IsActive = !setInactive.IsActive;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
