using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Brands;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Commons;
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
    public class BrandApiService : IBrandApiService
    {
        private readonly IHttpClientFactory _httpContextFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandApiService(IHttpClientFactory httpContextFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextFactory = httpContextFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<bool>> CreateBrand(BrandCreateRequest request)
        {
            var requestContent = new MultipartFormDataContent();
            byte[] data;
            using (var br = new BinaryReader(request.Logo.OpenReadStream()))
            {
                data = br.ReadBytes((int)request.Logo.OpenReadStream().Length);
            }
            ByteArrayContent bytes = new ByteArrayContent(data);
            requestContent.Add(bytes, "Logo", request.Logo.FileName);
            requestContent.Add(new StringContent(request.Name.ToString()), "Name");
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.PostAsync("/api/brands", requestContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> DeleteBrand(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.DeleteAsync($"/api/brands/{id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<BrandViewModel>> GetBrandDetail(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/brands/{id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<BrandViewModel>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<Pagination<BrandViewModel>>> GetBrandPagination(int pageIndex, int pageSize)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/brands/pagination?pageIndex={pageIndex}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<Pagination<BrandViewModel>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<List<BrandQuickViewModel>>> GetBrands()
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await httpClient.GetAsync("/api/brands");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<List<BrandQuickViewModel>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> UpdateBrand(BrandUpdateRequest request)
        {
            var requestContent = new MultipartFormDataContent();
            if (request.Logo != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Logo.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Logo.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Logo", request.Logo.FileName);
            }
            requestContent.Add(new StringContent(request.Name.ToString()), "Name");
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.PutAsync($"/api/brands/{request.Id}", requestContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }
    }
}
