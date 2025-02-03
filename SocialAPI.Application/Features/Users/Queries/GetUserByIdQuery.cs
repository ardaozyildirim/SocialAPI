using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SocialAPI.Application.DTOs;
using SocialAPI.Domain.Entities;
using SocialAPI.Domain.Interfaces;

namespace SocialAPI.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
                return _mapper.Map<UserDto>(user);
            }
        }
    }
} 