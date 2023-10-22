using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Categories;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public interface ICategoryApiService
    {
        Task<ApiResponse<bool>> CreateCategory(CategoryCreateRequest request);
        Task<ApiResponse<bool>> UpdateCategory(CategoryUpdateRequest request);
        Task<ApiResponse<bool>> DeleteCategory(int id);
        Task<ApiResponse<List<CategoryViewModel>>> GetCategories();
        Task<ApiResponse<Pagination<CategoryViewModel>>> GetCategoryPagination(int pageIndex, int pageSize);
        Task<ApiResponse<CategoryViewModel>> GetCategoryDetail(int id);
    }
}
