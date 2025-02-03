using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialAPI.Application.DTOs;
using SocialAPI.Application.Features.Users.Commands;
using SocialAPI.Application.Services;
using SocialAPI.Domain.Entities;
using SocialAPI.Domain.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace SocialAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public AuthController(IMediator mediator, IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(CreateUserDto createUserDto)
        {
            var command = new CreateUserCommand { UserDto = createUserDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserDto loginDto)
        {
            var users = await _unitOfWork.Repository<User>().FindAsync(u => u.Email == loginDto.Email);
            var user = users.FirstOrDefault();

            if (user == null || !BC.Verify(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password");
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }
    }
} 