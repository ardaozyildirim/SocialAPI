using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SocialAPI.Application.DTOs;
using SocialAPI.Domain.Entities;
using SocialAPI.Domain.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace SocialAPI.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public CreateUserDto UserDto { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = new User
                {
                    Username = request.UserDto.Username,
                    Email = request.UserDto.Email,
                    PasswordHash = BC.HashPassword(request.UserDto.Password),
                    CreatedAt = System.DateTime.UtcNow
                };

                await _unitOfWork.Repository<User>().AddAsync(user);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<UserDto>(user);
            }
        }
    }
} 