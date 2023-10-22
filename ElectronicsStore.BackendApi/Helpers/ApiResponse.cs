namespace ElectronicsStore.BackendApi.Helpers
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public T ObjectResult { get; set; }
        public string Message { get; set; }
    }
}
