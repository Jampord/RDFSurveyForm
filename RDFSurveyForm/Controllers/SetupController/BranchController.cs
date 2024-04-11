using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.EXTENSIONS;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.UserDto;
using RDFSurveyForm.Dto.SetupDto.BranchDto;
using RDFSurveyForm.Services;

namespace RDFSurveyForm.Controllers.SetupController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;

        public BranchController(IUnitOfWork unitOfWork, StoreContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddBranch")]
        public async Task<IActionResult> AddBranch(AddBranchDto branch)
        {
            var branchExist = await _unitOfWork.Branches.BranchAlreadyExist(branch.BranchName);
            var codeExist = await _unitOfWork.Branches.BranchCodeExist(branch.BranchCode);
            if(branchExist == false)
            {
                return BadRequest("Branch Name Already Exist!");
            }
            if(codeExist == false)
            {
                return BadRequest("Branch Code Already Exist!");
            }
            await _unitOfWork.Branches.AddBranch(branch);
            return Ok("Branch Added!");
        }

        [HttpPut("UpdateBranch/{Id:int}")]
        public async Task<IActionResult> UpdateBranch([FromBody]UpdateBranchDto branch, [FromRoute] int Id)
        {
            branch.Id = Id;
            var branchExist = await _unitOfWork.Branches.BranchAlreadyExist(branch.BranchName);
            var updateBranch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == branch.Id);

            
            if(branchExist == false && branch.BranchName != updateBranch.BranchName)
            {
                return Ok("Branch Name Already Exist!");
            }

            var branchId = await _unitOfWork.Branches.UpdateBranch(branch);
            if(branchId == false)
            {
                return BadRequest("Branch Id Not Found!");
            }
            return Ok("Updated Successfuly!"); 
        }

        [HttpGet("BranchListPagination")]
        public async Task<ActionResult<IEnumerable<GetBranchDto>>> CustomerListPagnation([FromQuery] UserParams userParams, bool? status, string search)
        {
            var branchsummary = await _unitOfWork.Branches.BranchListPagnation(userParams, status, search);

            Response.AddPaginationHeader(branchsummary.CurrentPage, branchsummary.PageSize, branchsummary.TotalCount, branchsummary.TotalPages, branchsummary.HasNextPage, branchsummary.HasPreviousPage);

            var branchsummaryResult = new
            {
                branchsummary,
                branchsummary.CurrentPage,
                branchsummary.PageSize,
                branchsummary.TotalCount,
                branchsummary.TotalPages,
                branchsummary.HasNextPage,
                branchsummary.HasPreviousPage
            };

            return Ok(branchsummaryResult);
        }

        [HttpPatch("SetIsactive/{Id:int}")]
        public async Task<IActionResult> SetIsactive([FromRoute]int Id)
        {
            var isactiveValidation = await _unitOfWork.Branches.IsActiveValidation(Id);
            if (isactiveValidation == true)
            {
                return BadRequest("Cannot Deavtivate Department");
            }
            var setisactive = await _unitOfWork.Branches.SetIsactive(Id);
            if(setisactive == false)
            {
                return BadRequest("Branch does not exist!");
            }
            return Ok("Updated!");
        }

        
    }
}
