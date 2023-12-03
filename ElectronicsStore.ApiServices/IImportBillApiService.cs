using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.ImportBills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public interface IImportBillApiService
    {
        Task<ApiResponse<bool>> CreateImportBill(ImportBillCreateRequest request);
        Task<ApiResponse<Pagination<ImportBillViewModel>>> GetImportBills(int? id, string? userId, int pageIndex, int pageSize);
        Task<ApiResponse<ImportBillDetailViewModel>> GetImportBillDetail(int id);

    }
}
