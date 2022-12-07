using Blazor.Data.Services.Base;
using Blazor.Data.Services.Interfaces;
using Blazor.Data.Models;
using Blazor.Data.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.Authorization;
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;


namespace Blazor.Data.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IClient _httpClient;
        private readonly ProtectedLocalStorage _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private IMapper _mapper;
        public UsersService(IClient httpClient, ProtectedLocalStorage localStorage, AuthenticationStateProvider authenticationStateProvider, IMapper mapper)
            : base(httpClient, localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            _mapper = mapper;
        }


        public async Task<LoginUserResponse> LoginAsync(User user)
        {
            try
            {
                var loginRequest = _mapper.Map<LoginUserRequest>(user);

                _mapper.Map<LoginUserRequest>(user);
                var response = await _httpClient.LoginAsync(loginRequest);
                //await _localStorage.SetAsync("token", response.AccessToken);
                //await _localStorage.SetAsync("refreshToken", response.RefreshToken);
                //((CustomAuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(response.User.Username);
                return (response);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                var ret = new LoginUserResponse { User = null };
                return (ret);
            }
        }
        public async Task<string> RegisterUserAsync(CreateUser user)
        {
            try
            {
                var createUserRequest = _mapper.Map<CreateUser, CreateUserRequest>(user);
                var response = await _httpClient.CreateUserAsync(createUserRequest);
                return ("User registered");
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (response.Item2);
            }
        }
    }
}