
namespace UserInfoService.Services.Clients
{
    public class ServiceResult<T>
    {
        public T? Result { get; set; }
        public Error? Error { get; set; }
        public bool IsSuccess =>  Error == null;

    }
    public class Error
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
    }
}
