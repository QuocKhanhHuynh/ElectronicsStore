using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public interface IProductApiService
    {
        Task<ApiResponse<bool>> CreateProduct(ProductCreateRequest request);
        Task<ApiResponse<bool>> UpdateProduct(ProductUpdateRequest request);
        Task<ApiResponse<bool>> DeleteProduct(int id);
        Task<ApiResponse<List<ProductQuickViewModel>>> Getproducts();
        Task<ApiResponse<Pagination<ProductQuickViewModel>>> GetProductPagination(string? keyword, int? categoryId, int pageIndex, int pageSize);
        Task<ApiResponse<ProductViewModel>> GetProductDetail(int id);
        Task<ApiResponse<Pagination<ProductBaseViewModel>>> GetProductInformation(string? keyword, int? categoryId, int pageIndex, int pageSize);
        Task<ApiResponse<Pagination<ProductBaseViewModel>>> GetSoldoutProduct(int pageIndex, int pageSize);
        Task<ApiResponseSuccess<ProductViewModel>> GetDetailInformation(int id);
    }
}
