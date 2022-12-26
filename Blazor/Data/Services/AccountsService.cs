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
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Components.Forms;

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
        public async Task<(bool, IList<AccountResponse>?, string?)> GetAllAccounts()
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.GetAccountsAsync(token);
                var accounts = _mapper.Map<IList<AccountResponse>>(response);
                Console.WriteLine(accounts.ToString());
                return (true, accounts, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }

        public async Task<(bool, GetAccountResponse?, string?)> GetAccountDetails(int id)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.GetAccountAsync(token, id);

                //CreateMap<AccountDetails, GetAccountResponse>().ReverseMap();
                //var account = _mapper.Map<GetAccountResponse, AccountDetails>(response);
                return (true, response, null);
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
                var token = String.Join(" ", "Bearer", auth.Value);
                var accountRequest = _mapper.Map<AccountRequest>(account);
                var response = await _httpClient.CreateAccountAsync(token, accountRequest);
                return (true, response, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }

        public async Task<(bool, string?, string?)> PostDocumentAsync(int id,IBrowserFile file)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.UploadDocumentAsync(token, id);
                return (false, "Upload document sucess", null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }

        public async Task<(bool, DocumentResponse?, string?)> DownloadDocumentfromAccountAsync(int id)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.DownloadDocumentAsync(token, id,id );

                //CreateMap<AccountDetails, GetAccountResponse>().ReverseMap();
                //var account = _mapper.Map<GetAccountResponse, AccountDetails>(response);
                return (true, response, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }

        public async Task<(bool, ICollection<DocumentResponse>?, string?)> GetDoccumentAccountAsync(int id)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.GetDoccumentByAccountAsync(token, id);

                //CreateMap<AccountDetails, GetAccountResponse>().ReverseMap();
                //var account = _mapper.Map<GetAccountResponse, AccountDetails>(response);
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
