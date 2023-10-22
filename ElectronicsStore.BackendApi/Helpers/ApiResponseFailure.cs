using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ElectronicsStore.BackendApi.Helpers
{
    public class ApiResponseFailure<T> : ApiResponse<T>
    {
        public ApiResponseFailure()
        {
            Status = false;
        }

        public ApiResponseFailure(string message)
        {
            Status = false;
            Message = message;
        }
    }
}
