using Microsoft.AspNetCore.Mvc;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.EXTENSIONS;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.DepartmentDto;
using RDFSurveyForm.Services;

namespace RDFSurveyForm.Controllers.ModelController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpPost]
        [Route("AddNewDepartment")]
        public async Task<IActionResult> AddDepartment(AddDepartmentDto department)
        {
            await _unitOfWork.Department.AddDepartment(department);

            return Ok("Success");
        }

        [HttpPut("UpdateDepartment/{Id:int}")]

        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto department, [FromRoute] int Id)
        {
            department.Id = Id;

            var dept = await _unitOfWork.Department.UpdateDepartment(department);
            if (dept == false)
            {
                return BadRequest("User does not exist");
            }
            return Ok("Success");
        }


        [HttpDelete("DeleteDepartment/{Id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int Id)
        {
            var dept = await _unitOfWork.Department.DeleteDepartment(Id);
            if (dept == false)
            {
                return BadRequest("ID does not exist");
            }
            return Ok("Deleted!");

        }

        [HttpPut("SetInActive/{Id:int}")]
        public async Task<IActionResult> SetInActive([FromRoute] int Id)
        {
            var setinactive = await _unitOfWork.Department.SetInActive(Id);
            if (setinactive == null)
            {
                return BadRequest("Id does not exist");

            }


            return Ok("Updated");

        }

        [HttpGet]
        [Route("CustomerListPagnation")]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> CustomerListPagnation([FromQuery] UserParams userParams, bool? status, string search)
        {
            var posummary = await _unitOfWork.Department.CustomerListPagnation(userParams, status, search);

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
    }
}
