using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.EXTENSIONS;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.Unit_SubUnitDto;
using RDFSurveyForm.Services;

namespace RDFSurveyForm.Controllers.Unit_SubunitController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubunitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;


        public SubunitController(IUnitOfWork unitOfWork, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;

        }
        [HttpPost]
        [Route("AddNewsubunit")]
        public async Task<IActionResult> AddUnit(AddSubunitDto unit)
        {
            var existingunit = await _unitOfWork.Unit.ExistingUnit(unit.SubunitName);

            if (existingunit == false)
            {
                return BadRequest("Unit Name already exist!");
            }
            await _unitOfWork.Subunit.AddSubunit(unit);
            await _unitOfWork.CompleteAsync();

            return Ok("Success");
        }

        [HttpPut("UpdateSubunit/{Id:int}")]

        public async Task<IActionResult> UpdateSubunit([FromBody] UpdateSubunitDto unit, [FromRoute] int Id)
        {
            unit.Id = Id;

            var units = await _unitOfWork.Subunit.UpdateSubunit(unit);
            if (units == false)
            {
                return BadRequest("Unit does not exist");
            }
            return Ok("Success");
        }




        [HttpPatch("SetIsActive/{Id:int}")]
        public async Task<IActionResult> SetIsActive([FromRoute] int Id)
        {
            var setisactive = await _unitOfWork.Subunit.SetIsActive(Id);
            if (setisactive == false)
            {
                return BadRequest("Subunit Id does not exist");

            }


            return Ok("Updated");

        }

        [HttpGet]
        [Route("SubunitListPagination")]
        public async Task<ActionResult<IEnumerable<GetUnitDto>>> SubunitListPagnation([FromQuery] UserParams userParams, bool? status, string search)
        {
            var subunitsummary = await _unitOfWork.Unit.UnitListPagination(userParams, status, search);

            Response.AddPaginationHeader(subunitsummary.CurrentPage, subunitsummary.PageSize, subunitsummary.TotalCount, subunitsummary.TotalPages, subunitsummary.HasNextPage, subunitsummary.HasPreviousPage);

            var subunitsummaryResult = new
            {
                subunitsummary,
                subunitsummary.CurrentPage,
                subunitsummary.PageSize,
                subunitsummary.TotalCount,
                subunitsummary.TotalPages,
                subunitsummary.HasNextPage,
                subunitsummary.HasPreviousPage
            };

            return Ok(subunitsummaryResult);
        }
    }
}
