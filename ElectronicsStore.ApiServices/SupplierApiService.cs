using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.Suppliers;
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
    public class SupplierApiService : ISupplierApiService
    {
        private readonly IHttpClientFactory _httpContextFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SupplierApiService(IHttpClientFactory httpContextFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextFactory = httpContextFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<bool>> CreateSupplier(SupplierCreateRequest request)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/suppliers", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> DeleteSupplier(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.DeleteAsync($"/api/suppliers/{id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<List<SupplierQuickViewModel>>> GetSuppliers()
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync("/api/suppliers");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<List<SupplierQuickViewModel>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<SupplierViewModel>> GetSupplierDetail(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/suppliers/{id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<SupplierViewModel>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<Pagination<SupplierQuickViewModel>>> GetSupplierPagination(int pageIndex, int pageSize)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/suppliers/pagination?pageIndex={pageIndex}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<Pagination<SupplierQuickViewModel>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> UpdateSupplier(SupplierUpdateRequest request)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"/api/suppliers/{request.Id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }
    }
}
