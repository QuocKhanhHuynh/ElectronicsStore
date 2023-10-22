using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public interface IRoleApiService
    {
        Task<ApiResponse<bool>> CreateRole(RoleCreateRequest request);
        Task<ApiResponse<bool>> UpdateRole(string id, RoleUpdateRequest request);
        Task<ApiResponse<bool>> DeleteRole(string id);
        Task<ApiResponse<List<RoleViewModel>>> GetRoles();
        Task<ApiResponse<RoleViewModel>> GetRoelDetail(string id);
    }
}
