using Application.BusinessServices.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.WebUtilities;
using Application.ApiClient;

namespace Application.BusinessServices
{
    public class PermissionService : IPermissionService
    {
        private readonly IWebApiExecuter _apiExecuter;
        private const string urlAPI = "api/admin/permission";

        public PermissionService(IWebApiExecuter apiExecuter)
        {
            _apiExecuter = apiExecuter;
        }

        public async Task<PagingResponse<PermissionModel>> GetPermissionsList(int pageNumber, string searchValue)
        {
            var response = await _apiExecuter.InvokeGet<PagingResponse<PermissionModel>>(urlAPI +$"?pageNumber={pageNumber}&searchValue={searchValue}");
            return response;
        }
    }
}
