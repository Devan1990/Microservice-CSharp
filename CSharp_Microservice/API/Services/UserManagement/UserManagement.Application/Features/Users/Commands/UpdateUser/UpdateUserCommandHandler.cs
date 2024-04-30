using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Exceptions;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,long>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IverticalRepository _verticalRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IRoleRepository _rolerepository;
        public UpdateUserCommandHandler(IUserRepository userRepository, 
                                            IMapper mapper, 
                                            ILogger<UpdateUserCommandHandler> logger, 
                                            IRoleRepository roleRepository, 
                                            IverticalRepository verticalRepository, 
                                            ICountryRepository countryRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _verticalRepository = verticalRepository ?? throw new ArgumentNullException(nameof(verticalRepository));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _rolerepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<long> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(request.Id);
            if (userToUpdate == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }
            var country = await _countryRepository.GetCountryById(request.CountryId);
            if (country == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Country), request.CountryId);
            }

            var area = country.Areas.FirstOrDefault(a => a.Id == request.AreaId);
            if (area == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Area), request.AreaId);
            }

            var region = area.Regions.FirstOrDefault(a => a.Id == request.RegionId);
            if (region == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Region), request.RegionId);
            }

            var role = await _rolerepository.GetRoleById(request.RoleId);

            if (role == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Role), request.RoleId);
            }

            var vertical = await _verticalRepository.GetVerticalById(request.VerticaId);

            userToUpdate.Role = role;
            userToUpdate.Vertical = vertical;
            userToUpdate.Country = country;
            userToUpdate.Area = area;
            userToUpdate.Region = region;

            _mapper.Map(request, userToUpdate, typeof(UpdateUserCommand), typeof(User));


            var entity = await _userRepository.UpdateUser(userToUpdate);

            _logger.LogInformation($"Order {userToUpdate.Id} is successfully updated.");

            return entity;
        }
    }
}
