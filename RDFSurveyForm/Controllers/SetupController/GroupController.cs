using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.EXTENSIONS;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.SetupDto.GroupDto;
using RDFSurveyForm.Services;
using System.Runtime.CompilerServices;

namespace RDFSurveyForm.Controllers.SetupController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;

        public GroupController(IUnitOfWork unitOfWork, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroup(AddGroupDto group)
        {
            var groupExist = await _unitOfWork.Groups.GroupAlreadyExist(group.GroupName);
            if(groupExist == false)
            {
                return BadRequest("Group Name Already Exist!");
            }
            await _unitOfWork.Groups.AddGroup(group);
            return Ok("Group Added!");
        }

        [HttpPut("UpdateGroup/{Id:int}")]
        public async Task<IActionResult> UpdateGroup([FromBody]UpdateGroupDto group,[FromRoute] int Id)
        {
            group.Id = Id;
            var groupExist = await _unitOfWork.Groups.GroupAlreadyExist(group.GroupName);
            var updateGroup = await _context.Groups.FirstOrDefaultAsync(x => x.Id == group.Id);
            if(groupExist == false && group.GroupName != updateGroup.GroupName) 
            {
                return Ok("Group Name Already Exist!");

       
            }

            var groupId = await _unitOfWork.Groups.UpdateGroup(group);
            if(groupId == false)
            {
                return BadRequest("Group Id Not Found!");
            }
            return Ok("Update Successfuly!");
        }

        [HttpGet("GroupListPagination")]
        public async Task<ActionResult<IEnumerable<GetGroupDto>>> GroupListPagnation([FromQuery] UserParams userParams, bool ? status, string search)
        {
            var gcsummary = await _unitOfWork.Groups.GroupListPagnation(userParams, status, search);
            Response.AddPaginationHeader(gcsummary.CurrentPage, gcsummary.PageSize, gcsummary.TotalCount, gcsummary.TotalPages, gcsummary.HasNextPage, gcsummary.HasPreviousPage);

            var gcsummaryResult = new
            {
                gcsummary,
                gcsummary.CurrentPage,
                gcsummary.PageSize,
                gcsummary.TotalCount,
                gcsummary.TotalPages,
                gcsummary.HasNextPage,
                gcsummary.HasPreviousPage
            };
            return Ok(gcsummaryResult);
        }

        [HttpPatch("Setisactive/{Id:int}")]
        public async Task<IActionResult> SetIsactive([FromRoute] int Id)
        {
            var setisactive = await _unitOfWork.Groups.SetIsactive(Id);
            if(setisactive == false)
            {
                return BadRequest("Group does not exist!");
            }
            return Ok("Updated");
        }

        
    }

}
