using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Brands;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public interface IBrandApiService
    {
        Task<ApiResponse<bool>> CreateBrand(BrandCreateRequest request);
        Task<ApiResponse<bool>> UpdateBrand(BrandUpdateRequest request);
        Task<ApiResponse<bool>> DeleteBrand(int id);
        Task<ApiResponse<List<BrandQuickViewModel>>> GetBrands();
        Task<ApiResponse<Pagination<BrandViewModel>>> GetBrandPagination(int pageIndex, int pageSize);
        Task<ApiResponse<BrandViewModel>> GetBrandDetail(int id);
    }
}
