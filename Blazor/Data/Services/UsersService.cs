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
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

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


        public async Task<(bool, LoginUserResponse?, string?)> LoginAsync(User user)
        {
            try
            {
                var loginRequest = _mapper.Map<LoginUserRequest>(user);
                var response = await _httpClient.LoginAsync(loginRequest);
                Console.WriteLine(response.AccessToken);                
                await _localStorage.SetAsync("token", response.AccessToken);
                await _localStorage.SetAsync("refreshToken", response.RefreshToken);
                return (true, response, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }
        public async Task<(bool, string?)> RegisterUserAsync(CreateUser user)
        {
            try
            {
                var createUserRequest = _mapper.Map<CreateUser, CreateUserRequest>(user);
                var response = await _httpClient.CreateUserAsync(createUserRequest);
                return (true, "User registered");
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, response.Item2);
            }
        }
    }
}