namespace ems.application.Wrappers
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            
        }

        //success response
        public ApiResponse(T data, string? message, bool isSuccecced)
        {


            IsSuccecced = isSuccecced;
            Message = message;
            Data = data;

        }

        //faild response
        
        public bool IsSuccecced { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
