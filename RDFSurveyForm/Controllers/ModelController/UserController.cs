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

            if (string.IsNullOrEmpty(user.FullName))
            {
                return BadRequest("Enter FullName");
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                return BadRequest("Enter UserName");
            }
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

            if (string.IsNullOrEmpty(user.FullName))
            {
                return BadRequest("Enter FullName");
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                return BadRequest("Enter UserName");
            }
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


        

        [HttpGet]
        [Route("CustomerListPagnation")]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> CustomerListPagnation([FromQuery] UserParams userParams, bool? status, string search)
        {
            var usersummary = await _unitOfWork.Customer.CustomerListPagnation(userParams, status, search);

            Response.AddPaginationHeader(usersummary.CurrentPage, usersummary.PageSize, usersummary.TotalCount, usersummary.TotalPages, usersummary.HasNextPage, usersummary.HasPreviousPage);

            var usersummaryResult = new
            {
                usersummary,
                usersummary.CurrentPage,
                usersummary.PageSize,
                usersummary.TotalCount,
                usersummary.TotalPages,
                usersummary.HasNextPage,
                usersummary.HasPreviousPage
            };

            return Ok(usersummaryResult);
        }

        [HttpPatch("SetInActive/{Id:int}")]
        public async Task<IActionResult> SetInActive([FromRoute] int Id)
        {
            var setinactive = await _unitOfWork.Customer.SetInActive(Id);
            if (setinactive == null)
            {
                return BadRequest("Id does not exist");
            }
            return Ok("Updated");
        }

        
    }
}
