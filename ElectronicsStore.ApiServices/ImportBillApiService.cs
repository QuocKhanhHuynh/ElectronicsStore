using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.ImportBills;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public class ImportBillApiService : IImportBillApiService
    {
        private readonly IHttpClientFactory _httpContextFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImportBillApiService(IHttpClientFactory httpContextFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextFactory = httpContextFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<bool>> CreateImportBill(ImportBillCreateRequest request)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/ImportBills", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<List<ImportBillDetailViewModel>>> GetImportBillDetail(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/ImportBills/{id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<List<ImportBillDetailViewModel>>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<ApiResponse<Pagination<ImportBillViewModel>>> GetImportBills(int? id, string? userId, int pageIndex, int pageSize)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/ImportBills?id={id}&userId={userId}&pageIndex={pageIndex}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<Pagination<ImportBillViewModel>>>(await response.Content.ReadAsStringAsync());
        }
    }
}
