using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.EXTENSIONS;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.DepartmentDto;
using RDFSurveyForm.Dto.Unit_SubUnitDto;
using RDFSurveyForm.Services;

namespace RDFSurveyForm.Controllers.Unit_SubunitController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;


        public UnitController(IUnitOfWork unitOfWork, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;

        }
        [HttpPost]
        [Route("AddNewUnit")]
        public async Task<IActionResult> AddUnit(AddUnitDto unit)
        {
            var existingunit = await _unitOfWork.Unit.ExistingUnit(unit.UnitName);

            if (existingunit == false)
            {
                return BadRequest("Unit Name already exist!");
            }
            await _unitOfWork.Unit.AddUnit(unit);
            await _unitOfWork.CompleteAsync();

            return Ok("Success");
        }

        [HttpPut("UpdateUnit/{Id:int}")]

        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateUnitDto unit, [FromRoute] int Id)
        {
            unit.Id = Id;

            var units = await _unitOfWork.Unit.UpdateUnit(unit);
            if (units == false)
            {
                return BadRequest("Unit does not exist");
            }
            return Ok("Success");
        }




        [HttpPatch("SetIsActive/{Id:int}")]
        public async Task<IActionResult> SetIsActive([FromRoute] int Id)
        {
            var setisactive = await _unitOfWork.Unit.SetIsActive(Id);
            if (setisactive == false)
            {
                return BadRequest("Unit Id does not exist");

            }


            return Ok("Updated");

        }

        [HttpGet]
        [Route("DepartmentListPagination")]
        public async Task<ActionResult<IEnumerable<GetUnitDto>>> CustomerListPagnation([FromQuery] UserParams userParams, bool? status, string search)
        {
            var unitsummary = await _unitOfWork.Unit.UnitListPagination(userParams, status, search);

            Response.AddPaginationHeader(unitsummary.CurrentPage, unitsummary.PageSize, unitsummary.TotalCount, unitsummary.TotalPages, unitsummary.HasNextPage, unitsummary.HasPreviousPage);

            var unitsummaryResult = new
            {
                unitsummary,
                unitsummary.CurrentPage,
                unitsummary.PageSize,
                unitsummary.TotalCount,
                unitsummary.TotalPages,
                unitsummary.HasNextPage,
                unitsummary.HasPreviousPage
            };

            return Ok(unitsummaryResult);
        }       
    }
}
