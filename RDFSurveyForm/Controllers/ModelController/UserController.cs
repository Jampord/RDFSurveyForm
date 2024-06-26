﻿using Microsoft.AspNetCore.Http;
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
                return BadRequest("Enter Full name");
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                return BadRequest("Enter Username");
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
                return BadRequest("Enter Full name");
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                return BadRequest("Enter Username");
            }
            if (fname == false && user.FullName != existingUser.FullName)
            {
                return BadRequest("Name already Exist!");

            }
            if (uname == false && user.UserName != existingUser.UserName)
            {
                return BadRequest("Username already Exist!");
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
            if (!BCrypt.Net.BCrypt.Verify(user.Password, notExist.Password))
            {
                return BadRequest("Wrong password");
            }

            //var passwordCheck = await _unitOfWork.Customer.PasswordCheck(user);
           
            //if (notExist.Password == user.NewPassword)
            if(BCrypt.Net.BCrypt.Verify(user.NewPassword , notExist.Password))
            {
                return BadRequest("Password is the same as old Password");
            }

            if (user.NewPassword != user.ConfirmPassword)
            {
                return BadRequest("Password Confirmation error");
            }

            await _unitOfWork.Customer.UpdatePassword(user);
            //await _unitOfWork.Customer.WrongPassword(user);
            await _unitOfWork.CompleteAsync();

            return Ok("Password Updated!");
        }


        

        [HttpGet]
        [Route("CustomerListPagination")]
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

        [HttpPatch("SetIsActive/{Id:int}")]
        public async Task<IActionResult> SetIsActive([FromRoute] int Id)
        {
            var setisactive = await _unitOfWork.Customer.SetIsActive(Id);
            if (setisactive == false)
            {
                return BadRequest("Id does not exist!");
            }
            return Ok("Updated");
        }

        [HttpPut("Resetpassword/{Id:int}")]
        public async Task<IActionResult> ResetPassword([FromRoute]int Id)
        {
            var resetPassord = await _unitOfWork.Customer.ResetPassword(Id);
            if(resetPassord == false)
            {
                return BadRequest("Id does not exist!");
            }
            return Ok("Password Reset");
        }

    }
}
