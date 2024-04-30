using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Infrastructure;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Models;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IUserRepository _userRepository;
        private readonly IverticalRepository _verticalRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IRoleRepository _rolerepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository
                                        , IRoleRepository roleRepository
                                        , IverticalRepository verticalRepository 
                                        , ICountryRepository countryRepository      
                                        , IMapper mapper
                                        , IEmailService emailService
                                        , ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _verticalRepository = verticalRepository ?? throw new ArgumentNullException(nameof(verticalRepository));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _rolerepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

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

            var user = _mapper.Map<Domain.Entities.User>(request);
            
            user.Role = role;
            user.Vertical= vertical;
            user.Country = country;
            user.Area = area;
            user.Region = region;

            //var UI = await _userRepository.GetUserOrderById(null, b => b.OrderByDescending(b => b.Id));
            //var UI_ID = UI == null ? 1 : UI.Id + 1;
            //user.UserId = "UI" + UI_ID.ToString().PadLeft(UI_ID.ToString().Length + 5 - UI_ID.ToString().Length, '0');
            var entity = await _userRepository.AddUser(user);

            _logger.LogInformation($"user {entity} is successfully created.");

            // await SendMail(newOrder);

            return entity;


        }

        private async Task SendMail(User order)
        {            
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
