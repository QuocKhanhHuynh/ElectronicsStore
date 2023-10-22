using Azure.Core;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.SaleBills;
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
    public class SaleBillApiService : ISaleBillApiService
    {
        private readonly IHttpClientFactory _httpContextFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SaleBillApiService(IHttpClientFactory httpContextFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextFactory = httpContextFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<int>> CheckInventory(int productId)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/SaleBills/Inventory/{productId}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<int>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> CreateSaleBill(SaleBillCreateRequest request)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/SaleBills", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<SaleBillInformation>> GetSaleBillDetail(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/SaleBills/{id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<SaleBillInformation>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<Pagination<SaleBillViewModel>>> GetSaleBills(int pageIndex, int pageSize,string? userId, int? statusId, int? id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/SaleBills?pageIndex={pageIndex}&pageSize={pageSize}&userId={userId}&statusId={statusId}&id={id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<Pagination<SaleBillViewModel>>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<ApiResponse<Pagination<RevenueStatisticsViewModel>>> StatisticsRevenue(int pageIndex, int pageSize, int? timeLine)
        { 
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/SaleBills/Statistics?pageIndex={pageIndex}&pageSize={pageSize}&timeLine={timeLine}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<Pagination<RevenueStatisticsViewModel>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> UpdateStatus(BillStatusUpdateRequest request)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync("/api/SaleBills", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }
    }
}
