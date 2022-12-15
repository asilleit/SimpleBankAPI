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
    public class AccountsService : BaseService, IAccountsService
    {
        private IClient _httpClient;
        private ProtectedLocalStorage _localStorage;
        private IMapper _mapper;
        public AccountsService(IClient httpClient, ProtectedLocalStorage localStorage, IMapper mapper)
            : base(httpClient, localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _mapper = mapper;
        }
        public async Task<(bool, IList<Account>?, string?)> GetAllAccounts()
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.GetAccountsAsync(token);
                var accounts = _mapper.Map<IList<Account>>(response);
                Console.WriteLine(accounts.ToString());
                return (true, accounts, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }
        public async Task<(bool, AccountDetails?, string?)> GetAccountDetails(int id)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.GetAccountAsync(token, id);
                var account = _mapper.Map<AccountResponse, AccountDetails>(response);
                return (true, account, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }
        public async Task<(bool, AccountResponse?, string?)>? PostAccountAsync(CreateAccount account)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                Console.WriteLine("");
                var token = String.Join(" ", "Bearer", auth.Value);
                var accountRequest = _mapper.Map<AccountRequest>(account);
                Console.WriteLine(account);
                var response = await _httpClient.CreateAccountAsync(token, accountRequest);
                return (true, response, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }
    }
}
