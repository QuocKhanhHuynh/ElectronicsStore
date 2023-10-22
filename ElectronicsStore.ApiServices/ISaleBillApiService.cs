using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.SaleBills;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public interface ISaleBillApiService
    {
        Task<ApiResponse<bool>> CreateSaleBill(SaleBillCreateRequest request);
        Task<ApiResponse<Pagination<SaleBillViewModel>>> GetSaleBills(int pageIndex, int pageSize,string? userId, int? statusId, int? id);
        Task<ApiResponse<SaleBillInformation>> GetSaleBillDetail(int id);
        Task<ApiResponse<bool>> UpdateStatus(BillStatusUpdateRequest request);
        Task<ApiResponse<int>> CheckInventory(int productId);
        Task<ApiResponse<Pagination<RevenueStatisticsViewModel>>> StatisticsRevenue(int pageIndex, int pageSize, int? timeLine);
    }
}
