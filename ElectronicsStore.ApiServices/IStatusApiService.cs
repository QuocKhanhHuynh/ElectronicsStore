using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Statuses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public interface IStatusApiService
    {
        Task<ApiResponse<bool>> CreateStatus(StatusCreateRequest request);
        Task<ApiResponse<bool>> UpdateStatus(int id, StatusUpdateRequest request);
        Task<ApiResponse<bool>> DeleteStatus(int id);
        Task<ApiResponse<List<StatusViewModel>>> GetStatuses();
        Task<ApiResponse<StatusViewModel>> GetStatusDetail(int id);
    }
}
