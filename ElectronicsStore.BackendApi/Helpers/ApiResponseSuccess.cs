namespace ElectronicsStore.BackendApi.Helpers
{
    public class ApiResponseSuccess<T> : ApiResponse<T>
    {
        public ApiResponseSuccess()
        {
            Status = true;
        }

        public ApiResponseSuccess(T objectResult)
        {
            Status = true;
            ObjectResult = objectResult;
        }
    }
}
