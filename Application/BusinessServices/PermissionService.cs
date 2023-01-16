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

namespace Application.BusinessServices
{
    public class PermissionService : IPermissionService
    {
        private readonly HttpClient _client;

        public PermissionService(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PagingResponse<PermissionModel>> GetPermissions(int? pageNumber, string? searchValue)
        {
            var response = await _client.GetAsync($"api/admin/permission?pageNumber={pageNumber}&searchValue={searchValue}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var perms = await response.Content.ReadFromJsonAsync<PagingResponse<PermissionModel>>();
            return perms;
        }
    }
}
