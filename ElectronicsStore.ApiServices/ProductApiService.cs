using ElectronicsStore.BackendApi.Data.Entities;
using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Brands;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace ElectronicsStore.ApiServices
{
    public class ProductApiService : IProductApiService
    {
        private readonly IHttpClientFactory _httpContextFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductApiService(IHttpClientFactory httpContextFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextFactory = httpContextFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<bool>> CreateProduct(ProductCreateRequest request)
        {
            var requestContent = new MultipartFormDataContent();
            foreach(var image in request.Image)
            {
                byte[] data;
                using (var br = new BinaryReader(image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Image", image.FileName);
            }
            requestContent.Add(new StringContent(request.Name.ToString()), "Name");
            requestContent.Add(new StringContent(request.SalePrice.ToString()), "SalePrice");
            requestContent.Add(new StringContent(request.Origin.ToString()), "Origin");
            requestContent.Add(new StringContent(request.Introduce.ToString()), "Introduce");
            requestContent.Add(new StringContent(request.Description.ToString()), "Description");
            requestContent.Add(new StringContent(request.BrandId.ToString()), "BrandId");
            requestContent.Add(new StringContent(request.CategoryId.ToString()), "CategoryId");
            if (request.OfferPrice.HasValue)
            {
                requestContent.Add(new StringContent(request.OfferPrice.ToString()), "OfferRate");
            }
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.PostAsync("/api/products", requestContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> DeleteProduct(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.DeleteAsync($"/api/products/{id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponseSuccess<ProductViewModel>> GetDetailInformation(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/products/detail/{id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<ProductViewModel>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<ProductViewModel>> GetProductDetail(int id)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/products/{id}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<ProductViewModel>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<Pagination<ProductBaseViewModel>>> GetProductInformation(string? keyword, int? categoryId, int pageIndex, int pageSize)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/products/Information?keyword={keyword}&categoryId={categoryId}&pageIndex={pageIndex}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<Pagination<ProductBaseViewModel>>>(await response.Content.ReadAsStringAsync());
        }
        public async Task<ApiResponse<Pagination<ProductQuickViewModel>>> GetProductPagination(string? keyword, int? categoryId, int pageIndex, int pageSize)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/products/Pagination?keyword={keyword}&categoryId={categoryId}&pageIndex={pageIndex}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<Pagination<ProductQuickViewModel>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<List<ProductQuickViewModel>>> Getproducts()
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await httpClient.GetAsync("/api/products");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<List<ProductQuickViewModel>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<Pagination<ProductBaseViewModel>>> GetSoldoutProduct(int pageIndex, int pageSize)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var response = await httpClient.GetAsync($"/api/products/soldout?&pageIndex={pageIndex}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<ApiResponseSuccess<Pagination<ProductBaseViewModel>>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> UpdateProduct(ProductUpdateRequest request)
        {
            var httpClient = _httpContextFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("Token"));
            var requestContent = new MultipartFormDataContent();
            if(request.Image != null)
            {
                foreach (var image in request.Image)
                {
                    byte[] data;
                    using (var br = new BinaryReader(image.OpenReadStream()))
                    {
                        data = br.ReadBytes((int)image.OpenReadStream().Length);
                    }
                    ByteArrayContent bytes = new ByteArrayContent(data);
                    requestContent.Add(bytes, "Image", image.FileName);
                }
            }
            requestContent.Add(new StringContent(request.Id.ToString()), "Id");
            requestContent.Add(new StringContent(request.Name.ToString()), "Name");
            requestContent.Add(new StringContent(request.SalePrice.ToString()), "SalePrice");
            requestContent.Add(new StringContent(request.Origin.ToString()), "Origin");
            requestContent.Add(new StringContent(request.Introduce.ToString()), "Introduce");
            requestContent.Add(new StringContent(request.Description.ToString()), "Description");
            requestContent.Add(new StringContent(request.OfferPrice.ToString()), "OfferPrice");
            var response = await httpClient.PutAsync($"/api/products/{request.Id}", requestContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponseSuccess<bool>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponseFailure<bool>>(await response.Content.ReadAsStringAsync());
        }
    }
}
