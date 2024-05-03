using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.EXTENSIONS;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.UserDto;
using RDFSurveyForm.Dto.SetupDto.GroupSurveyDto;
using RDFSurveyForm.Model;
using RDFSurveyForm.Model.Setup;
using RDFSurveyForm.Services;

namespace RDFSurveyForm.Controllers.SetupController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupSurveyController : ControllerBase
    {

        public readonly IUnitOfWork _unitofWork;
        public readonly StoreContext _context;

        public GroupSurveyController(IUnitOfWork unitOfWork, StoreContext context)
        {
            _context = context;
            _unitofWork = unitOfWork;
        }

        [HttpPost("AddGroupSurvey")]

        public async Task<IActionResult> AddGroupSurvey(AddGroupSurveyDto survey)
        {
            var groupexist = await _unitofWork.GroupSurvey.GroupIdDoesnotExist(survey.GroupsId);
            if (groupexist == false)
            {
                    return BadRequest("Group Id does not exist!");
            }

            var addgroupSurvey = await _unitofWork.GroupSurvey.AddSurvey(survey);
            if (addgroupSurvey == false)
            {
                    return BadRequest("Error!");
            }

            await _unitofWork.CompleteAsync();

            return Ok("Survey Added");
        }

        [HttpGet]
        [Route("GroupSurveyPagination")]
        public async Task<ActionResult<IEnumerable<GetGroupSurveyDto>>> GroupSurveyPagnation([FromQuery] UserParams userParams, bool? status, string search)
        {
            //List
            var GSsummary = await _unitofWork.GroupSurvey.GroupSurveyPagination(userParams, status, search);

            Response.AddPaginationHeader(GSsummary.CurrentPage, GSsummary.PageSize, GSsummary.TotalCount, GSsummary.TotalPages, GSsummary.HasNextPage, GSsummary.HasPreviousPage);

            var gssummaryResult = new
            {
                GSsummary,
                GSsummary.CurrentPage,
                GSsummary.PageSize,
                GSsummary.TotalCount,
                GSsummary.TotalPages,
                GSsummary.HasNextPage,
                GSsummary.HasPreviousPage
            };

            return Ok(gssummaryResult);
        }

        [HttpPatch("ViewSurveyGenerator/{Id:int}")]
        public async Task<IActionResult> ViewSurveyGenerator([FromRoute] int Id)
        {
            var users = await _unitofWork.GroupSurvey.ViewSurvey(Id);

            return Ok(users);
        }

        [HttpPut("UpdateScore")]
        public async Task<IActionResult> UpdateScore([FromBody] UpdateSurveyScoreDto score)
        {
 
            var surveyscore = await _context.GroupSurvey.FirstOrDefaultAsync(x => x.GroupsId == score.GroupsId);
            if (surveyscore == null)
            {
                return BadRequest("ID does not exist!");
            }
            var scores = await _unitofWork.GroupSurvey.UpdateScore(score);
            if (scores == false)
            {
                return BadRequest("Error!");
            }

            return Ok("Score Approved!");

        }


    }
}
