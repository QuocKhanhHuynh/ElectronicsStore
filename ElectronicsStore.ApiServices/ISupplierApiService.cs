using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public interface ISupplierApiService
    {
        Task<ApiResponse<bool>> CreateSupplier(SupplierCreateRequest request);
        Task<ApiResponse<bool>> UpdateSupplier(SupplierUpdateRequest request);
        Task<ApiResponse<bool>> DeleteSupplier(int id);
        Task<ApiResponse<List<SupplierQuickViewModel>>> GetSuppliers();
        Task<ApiResponse<Pagination<SupplierQuickViewModel>>> GetSupplierPagination(int pageIndex, int pageSize);
        Task<ApiResponse<SupplierViewModel>> GetSupplierDetail(int id);
    }
}
