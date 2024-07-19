using FluentValidation;
using Inforce_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using static Inforce_Api.Utility.SD;
using Inforce_Api.Helpers;
using Inforce_Api.Models.DTO.User.Requests;
using AutoMapper;
using Inforce_Api.Interfaces;
using Inforce_Api.Models.DTO.User.Responses;
using Microsoft.EntityFrameworkCore;

namespace Inforce_Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest registerDto, [FromServices] IValidator<RegisterRequest> validator)
        {
            ModelStateDictionary errors = ValidateModel.Validate(validator, registerDto);

            if (errors.Count > 0)
            {
                return ValidationProblem(errors);
            }

            var user = _mapper.Map<ApplicationUser>(registerDto);
            user.UserName = user.Email;
            user.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, nameof(UserRoles.User));
            if (!roleResult.Succeeded)
                return BadRequest(result.Errors);


            return Ok("Registration successfull");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginRequest loginDto, [FromServices] IValidator<LoginRequest> validator)
        {
            ModelStateDictionary errors = ValidateModel.Validate(validator, loginDto);

            if (errors.Count > 0)
            {
                return ValidationProblem(errors);
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.NormalizedEmail == loginDto.Email.ToUpper());

            if (user == null)
                return BadRequest("Invalid Email or Password");

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return BadRequest("Invalid Email or Password");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest("Email verification required");


            return Ok(new UserResponse
            {
                Id = user.Id,
                Email = user.Email!,
                Token = await _jwtTokenGenerator.GenerateToken(user)
            });
        }
    }
}
