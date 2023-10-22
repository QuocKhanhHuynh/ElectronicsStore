using ElectronicsStore.BackendApi.Helpers;
using ElectronicsStore.Models.Commons;
using ElectronicsStore.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.ApiServices
{
    public interface IUserApiService
    {
        Task<ApiResponse<bool>> RigerterAccount(AccountRegisterRequest request);
        Task<ApiResponse<string>> LoginUserAccount(LoginRequest request);
        Task<ApiResponse<string>> LoginAdminAccount(LoginRequest request);
        Task<ApiResponse<bool>> UpdateAccount(string id, UserUpdateRequest request);
        Task<ApiResponse<bool>> ForgetPassword(PasswordForgetRequest request);
        Task<ApiResponse<bool>> UpdatePassword(string id, PasswordUpdateRequest request);
        Task<ApiResponse<Pagination<UserQuickViewModel>>> GetUsers(int pageIndex, int pageSize, string? userName, string? roleId);
        Task<ApiResponse<UserViewModel>> GetUserDetail(string id);
        Task<ApiResponse<List<UserQuickViewModel>>> GetAdmins();
        Task<ApiResponse<Pagination<UserQuickViewModel>>> GetStatistics(int pageIndex, int pageSize, int? timeLine);
    }
}
