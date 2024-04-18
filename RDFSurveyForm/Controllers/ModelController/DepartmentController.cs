using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.EXTENSIONS;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.DepartmentDto;
using RDFSurveyForm.Model;
using RDFSurveyForm.Services;
using System.Linq;

namespace RDFSurveyForm.Controllers.ModelController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;


        public DepartmentController(IUnitOfWork unitOfWork, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;

        }
        [HttpPost]
        [Route("AddNewDepartment")]
        public async Task<IActionResult> AddDepartment(AddDepartmentDto department)
        {
            var existingDept = await _unitOfWork.Department.ExistingDepartment(department.DepartmentName);

            if(existingDept == false)
            {
                return BadRequest("Department Name already exist!");
            }
            await _unitOfWork.Department.AddDepartment(department);
            await _unitOfWork.CompleteAsync();
<<<<<<< HEAD
=======

>>>>>>> 49db5c1739dac9ae0c909485b4b60ac1108213bf
            return Ok("Success");
        }

        [HttpPut("UpdateDepartment/{Id:int}")]

        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto department, [FromRoute] int Id)
        {
            department.Id = Id;

            var dept = await _unitOfWork.Department.UpdateDepartment(department);
            if (dept == false)
            {
                return BadRequest("Department does not exist");
            }
            return Ok("Success");
        }


        

        [HttpPatch("SetIsActive/{Id:int}")]
        public async Task<IActionResult> SetIsActive([FromRoute] int Id)
        {
            var isactiveValidation = await _unitOfWork.Department.IsActiveValidation(Id);
            if (isactiveValidation == true)
            {
                return BadRequest("Cannot Deactivate Department");
            }
            var setisactive = await _unitOfWork.Department.SetIsActive(Id);
            if (setisactive == false)
            {
                return BadRequest("Id does not exist");

            }


            return Ok("Updated");

        }

        [HttpGet]
        [Route("DepartmentListPagination")]
        public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> CustomerListPagnation([FromQuery] UserParams userParams, bool? status, string search)
        {
            var deptsummary = await _unitOfWork.Department.CustomerListPagnation(userParams, status, search);

            Response.AddPaginationHeader(deptsummary.CurrentPage, deptsummary.PageSize, deptsummary.TotalCount, deptsummary.TotalPages, deptsummary.HasNextPage, deptsummary.HasPreviousPage);

            var deptsummaryResult = new
            {
                deptsummary,
                deptsummary.CurrentPage,
                deptsummary.PageSize,
                deptsummary.TotalCount,
                deptsummary.TotalPages,
                deptsummary.HasNextPage,
                deptsummary.HasPreviousPage
            };

            return Ok(deptsummaryResult);
        }

        [HttpPut("SyncDepartment")]
        public async Task<IActionResult> SyncDepartment([FromBody] AddDepartmentDto[] department)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Something went wrong!") { StatusCode = 500 };
            }

            var duplicateList = new List<AddDepartmentDto>();
            var availableImport = new List<AddDepartmentDto>();
            var availableUpdate = new List<AddDepartmentDto>();
            var departmentNameEmpty = new List<AddDepartmentDto>();
            var deleteDepartment = new List<AddDepartmentDto>();

            
            foreach (var items in department)
            {
                if (department.Count(x => x.DepartmentName == items.DepartmentName && x.Id == items.Id) > 1)
                {
                    duplicateList.Add(items);
                }
                if (items.DepartmentName == string.Empty || items.DepartmentName == null)
                {
                    departmentNameEmpty.Add(items);
                    continue;
                }
                


                else
                {
                    var existingDepartment = await _unitOfWork.Department.GetByDepartmentNo(items.DepartmentNo);
                    if (existingDepartment != null)
                    {
                        bool hasChanged = false;

                        if (existingDepartment.DepartmentName != items.DepartmentName)
                        {
                            existingDepartment.DepartmentName = items.DepartmentName;
                            hasChanged = true;
                        }

                        if (hasChanged)
                        {
                            existingDepartment.IsActive = items.IsActive;
                            existingDepartment.EditedBy = User.Identity.Name;
                            existingDepartment.EditedAt = DateTime.Now;
                            existingDepartment.StatusSync = "New Update";
                            existingDepartment.SyncDate = DateTime.Now;

                            availableUpdate.Add(items);
                        }

                        if (!hasChanged)
                        {
                            existingDepartment.SyncDate = DateTime.Now;
                            existingDepartment.StatusSync = "No new update";
                        }
                    }
                    else
                    {
                        items.StatusSync = "New Added";
                        availableImport.Add(items);
                        await _unitOfWork.Department.AddDepartment(items);
                    }

                    deleteDepartment.Add(items);
                }

 

            }

            //var alldepartment = deleteDepartment.Where(x => x.DepartmentNo != null).ToList();

            var departmentlist = deleteDepartment.Select(x => x.DepartmentNo);

            var alldepartmentdelete = await _context.Department.ToListAsync();

            var differnce = alldepartmentdelete.Where(x => !departmentlist.Contains(x.DepartmentNo)).ToList();

            foreach (var dept in differnce)
            {
                _context.Remove(dept);
            }

            //var syncDelete = await _unitOfWork.Department.SyncDeleteCheck(deleteDepartment);

            var resultList = new
            {
                AvailableImport = availableImport,
                AvailableUpdate = availableUpdate,
                DuplicateList = duplicateList,
                DepartmentNameEmpty = departmentNameEmpty,
            };

            if (duplicateList.Count == 0 && departmentNameEmpty.Count == 0 )
            {
                await _unitOfWork.CompleteAsync();
                return Ok("Successfully updated and added!");
            }
            else
            {
                return BadRequest(resultList);
            }
        }
    }
}
