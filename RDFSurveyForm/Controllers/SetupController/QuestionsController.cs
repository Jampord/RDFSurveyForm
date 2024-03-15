using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.EXTENSIONS;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.SetupDto.QuestionsDto;
using RDFSurveyForm.Services;

namespace RDFSurveyForm.Controllers.SetupController
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;
        public QuestionsController(IUnitOfWork unitOfWork, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpPost("AddQeustions")]
        public async Task<IActionResult> AddQuestions(AddQuestionsDto question)
        {
            var questionexist = await _unitOfWork.Question.QuestionAlreadyExist(question.Question);
            if(questionexist == false)
            {
                return BadRequest("Question already Exist!");
            }
            await _unitOfWork.Question.AddQuestions(question);
            return Ok("Question Added!");
        }

        [HttpPut("UpdateQuestion/{Id:int}")]
        public async Task<IActionResult> UpdateQuestion([FromBody]UpdateQuestionsDto question,[FromRoute] int Id)
        {
            question.Id = Id;
            var questionExist = await _unitOfWork.Question.QuestionAlreadyExist(question.Question);
            var updateQuestion = await _context.Question.FirstOrDefaultAsync(x => x.Id == question.Id);
            if(questionExist == false && question.Question != updateQuestion.Question)
            {
                return BadRequest("Question already Exist!");
            }

            var questionId = await _unitOfWork.Question.UpdateQuestions(question);
            if(questionId == false)
            {
                return BadRequest("Question Id Not Found!");
            }
            return Ok("update Successfuly");
        }

        [HttpGet("QuestionListPagnation")]
        public async Task<ActionResult<IEnumerable<GetQuestionsDto>>> QuestionListPagnation([FromQuery] UserParams userParams, bool ? status, string search)
        {
            var posummary = await _unitOfWork.Question.QuestionListPagnation(userParams, status, search);
            Response.AddPaginationHeader(posummary.CurrentPage, posummary.PageSize, posummary.TotalCount, posummary.TotalPages, posummary.HasNextPage, posummary.HasPreviousPage);

            var posummaryResult = new
            {
                posummary,
                posummary.CurrentPage,
                posummary.PageSize,
                posummary.TotalCount,
                posummary.TotalPages,
                posummary.HasNextPage,
                posummary.HasPreviousPage
            };
            return Ok(posummaryResult);
        }

        [HttpGet("SetInactive/{Id:int}")]
        public async Task<IActionResult> SetInactive([FromRoute]int Id)
        {
            var setinactive = await _unitOfWork.Question.SetInactive(Id);
            if(setinactive  == false)
            {
                return BadRequest("Question Id does not exist!");
            }
            return Ok("Updated");
        }

        [HttpDelete("DeleteQuestion/{Id:int}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int Id)
        {
            var deletQuestion = await _unitOfWork.Question.DeleteQuestion(Id);
            if(deletQuestion == false)
            {
                return BadRequest("Question Id does not exist!");
            }
            return Ok("Deleted!");
        }

    }
}
