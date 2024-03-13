using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.Dto.SetupDto.GroupDto;
using RDFSurveyForm.Services;

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

        //[HttpGet("GroupListPagnation")]
        //public async Task<IActionResult<IEnumerable<GetGroupDto>>
    }

}
