using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RDFSurveyForm.Data;
using RDFSurveyForm.DATA_ACCESS_LAYER.EXTENSIONS;
using RDFSurveyForm.DATA_ACCESS_LAYER.HELPERS;
using RDFSurveyForm.Dto.ModelDto.UserDto;
using RDFSurveyForm.Services;

namespace RDFSurveyForm.Controllers.ModelController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;

        public UserController(IUnitOfWork unitOfWork, StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;

        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddNewUser(AddNewUserDto user)
        {
            var fname = await _unitOfWork.Customer.UserAlreadyExist(user.FullName);
            var uname = await _unitOfWork.Customer.UserNameAlreadyExist(user.UserName);


            if (fname == false)
            {
                return BadRequest("User Already Exist");
            }
            if (uname == false)
            {
                return BadRequest("Username Already Exist");
            }

            await _unitOfWork.Customer.AddNewUser(user);

            return Ok("User Added!");
        }

        [HttpPut("UpdateUser/{Id:int}")]

        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto user, [FromRoute] int Id)
        {
            user.Id = Id;
            var fname = await _unitOfWork.Customer.UserAlreadyExist(user.FullName);
            var uname = await _unitOfWork.Customer.UserNameAlreadyExist(user.UserName);

            var existingUser = await _context.Customer.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (fname == false && user.FullName != existingUser.FullName)
            {
                return BadRequest("Name already Exist!");

            }
            if (uname == false && user.UserName != existingUser.UserName)
            {
                return BadRequest("User Name already Exist!");
            }

            var users = await _unitOfWork.Customer.UpdateUser(user);
            if (users == false)
            {
                return BadRequest("User Not Found!");
            }
            return Ok("User Updated Successfuly!");
        }


        [HttpPut("UpdatePassword/{Id:int}")]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangePasswordDto user, [FromRoute] int Id)
        {
            user.Id = Id;


            var notExist = await _context.Customer.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (notExist == null)
            {
                return BadRequest("id not exist");
            }

            if (notExist.Password != user.Password)
            {
                return BadRequest("Wrong password");
            }

            if (notExist.Password == user.NewPassword)
            {
                return BadRequest("Password is the same as old Password");
            }

            if (user.NewPassword != user.ConfirmPassword)
            {
                return BadRequest("Confirmation error");
            }


            await _unitOfWork.Customer.WrongPassword(user);
            await _unitOfWork.CompleteAsync();

            return Ok("Password Updated!");




        }


        [HttpDelete("DeleteUser/{Id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int Id)
        {
            var userss = await _unitOfWork.Customer.DeleteUser(Id);
            if (userss == false)
            {
                return BadRequest("ID does not exist");
            }
            return Ok("Deleted!");

        }

        [HttpGet]
        [Route("CustomerListPagnation")]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> CustomerListPagnation([FromQuery] UserParams userParams, bool? status, string search)
        {
            var posummary = await _unitOfWork.Customer.CustomerListPagnation(userParams, status, search);

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

        [HttpPut("SetInActive/{Id:int}")]
        public async Task<IActionResult> SetInActive([FromRoute] int Id)
        {
            var setinactive = await _unitOfWork.Customer.SetInActive(Id);
            if (setinactive == null)
            {
                return BadRequest("Id does not exist");
            }
            return Ok("Updated");
        }

        //[HttpGet("ListUsers/{Inactivity:int}")]
        //public async Task<IActionResult> ListUser([FromRoute] GetUserDto user)
        //{
        //    var listUser = await _unitOfWork.Customer.ListUser(user);
        //    if(listUser == null)
        //    {
        //        return BadRequest("Nada");
        //    }
        //    return Ok(listUser);
        //}
    }
}
