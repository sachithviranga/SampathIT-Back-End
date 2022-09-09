using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user.business.Wrapper;
using user.contracts.Common;
using user.contracts.Managers;
using user.contracts.Repositories;
using user.entities.Common;
using user.entities.DTO;
using user.entities.Entities;

namespace user.business.Managers
{
    public class UserManager : IUserManager
    {
        protected readonly UserManager<ApplicationUser> _identityUserManager;

        private readonly IMapper<object, ServiceResponse> _serviceResponseMapper;

        private readonly IMapper<List<Message>, ServiceResponse> _serviceResponseErrorMapper;

        private readonly IUserRepository _userRepository;

        private readonly IErrorMessagesHandler _errorMessagesHandler;

        private readonly IMapper<UserCreateMapperWrapper, ApplicationUser> _userCreateMapper;

        private readonly ITokenManager _tokenManager;

        private readonly IMapper<AccessTokenMapperWrapper, TokenResponse> _tokenMapper;

        public UserManager(UserManager<ApplicationUser> identityUserManager, IMapper<object, ServiceResponse> serviceResponseMapper, IMapper<List<Message>, ServiceResponse> serviceResponseErrorMapper, IUserRepository userRepository, IErrorMessagesHandler errorMessagesHandler, IMapper<UserCreateMapperWrapper, ApplicationUser> userCreateMapper, ITokenManager tokenManager, IMapper<AccessTokenMapperWrapper, TokenResponse> tokenMapper)
        {
            _identityUserManager = identityUserManager;
            _serviceResponseMapper = serviceResponseMapper;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
            _userRepository = userRepository;
            _errorMessagesHandler = errorMessagesHandler;
            _userCreateMapper = userCreateMapper;
            _tokenManager = tokenManager;
            _tokenMapper = tokenMapper;
        }

        public async Task<ServiceResponse> Register(RegisterDTO request)
        {
            try
            {
                var user = await _identityUserManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    var applicationUser = _userCreateMapper.Map(new UserCreateMapperWrapper
                    {
                        User = request,
                    });

                    var result = await _identityUserManager.CreateAsync(applicationUser, request.Password);

                    if (result.Succeeded)
                    {
                        await AssignRole(applicationUser);
                        return _serviceResponseMapper.Map(true);
                    }
                    else
                    {
                        var messages = new List<Message>();
                        var error = result.Errors.FirstOrDefault();

                        messages.Add(new Message { Code = error.Code, Description = error.Description });

                        return _serviceResponseErrorMapper.Map(messages);
                    }
                }
                return _serviceResponseErrorMapper.Map(new List<Message> { _errorMessagesHandler.GetUserAlreadyExistError() });
            }
            catch (Exception)
            {
                return _serviceResponseErrorMapper.Map(new List<Message> { _errorMessagesHandler.GetServiceError() });
            }
        }

        private async Task AssignRole(ApplicationUser applicationUser)
        {
            try
            {
                List<string> roles = new List<string>();
                roles.Add("User");
                await _identityUserManager.AddToRolesAsync(applicationUser, roles);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ServiceResponse> Login(LoginDTO request)
        {
            try
            {
                var user = await _identityUserManager.FindByEmailAsync(request.Email);

                if (user != null && await _identityUserManager.CheckPasswordAsync(user, request.Password))
                {
                    return await Authentiate(user);
                }
                return _serviceResponseErrorMapper.Map(new List<Message> { _errorMessagesHandler.GetInvalidUserCredentialsError() });
            }
            catch (Exception ex)
            {
                return _serviceResponseErrorMapper.Map(new List<Message> { _errorMessagesHandler.GetServiceError() });
            }
        }


        private async Task<ServiceResponse> Authentiate(ApplicationUser applicationUser)
        {
            try
            {
                if (!string.IsNullOrEmpty(applicationUser.Id))
                {
                    var user =  _userRepository.GetUserById(applicationUser.Id);

                    var accessToken = _tokenManager.GenerateAccessToken(user);
                    var refreshToken = await _tokenManager.GenerateRefreshToken(applicationUser);

                    var tokenResult = _tokenMapper.Map(new AccessTokenMapperWrapper
                    {
                        User = user,
                        Token = accessToken,
                        RefreshToken = refreshToken
                    });

                    return _serviceResponseMapper.Map(tokenResult);
                }
                return _serviceResponseErrorMapper.Map(new List<Message> { _errorMessagesHandler.GetInvalidUserCredentialsError() });
            }
            catch (Exception ex)
            {
                return _serviceResponseErrorMapper.Map(new List<Message> { _errorMessagesHandler.GetServiceError() });
            }
        }

        public async Task<ServiceResponse> GetUsers()
        {
            return _serviceResponseMapper.Map(await _userRepository.GetUsers());
        }
    }
}
