using Azure.Core;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Statuses;
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

    public class StatusApiService : IStatusApiService
    {
        private readonly IHttpClientFactory _httpContextFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StatusApiService(IHttpClientFactory httpContextFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextFactory = httpContextFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<bool>> CreateStatus(StatusCreateRequest request)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/Statuses",httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> DeleteStatus(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.DeleteAsync($"/api/Statuses/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<StatusViewModel>> GetStatusDetail(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/Statuses/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<StatusViewModel>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<StatusViewModel>>(await response.Content.ReadAsStringAsync());
        }

        public  async Task<ApiResponse<List<StatusViewModel>>> GetStatuses()
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync("/api/Statuses");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<List<StatusViewModel>>>(await response.Content.ReadAsStringAsync());
        }

        public async  Task<ApiResponse<bool>> UpdateStatus(int id, StatusUpdateRequest request)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"/api/Statuses/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }
    }
}
