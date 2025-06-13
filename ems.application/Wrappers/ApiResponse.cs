namespace ems.application.Wrappers
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }

        // Success response constructor
        public ApiResponse(T data, string? message, bool isSucceeded)
        {
            IsSucceeded = isSucceeded;
            Message = message ?? string.Empty;
            Data = data;
            Errors = new List<string>();
        }

        public bool IsSucceeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public T Data { get; set; }

        // ✅ Static method for failure
        public static ApiResponse<T> Fail(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                IsSucceeded = false,
                Message = message,
                Data = default,
                Errors = errors ?? new List<string> { message }
            };
        }

        // ✅ Static method for success
        public static ApiResponse<T> Success(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                IsSucceeded = true,
                Message = message,
                Data = data,
                Errors = new List<string>()
            };
        }
    }

}
