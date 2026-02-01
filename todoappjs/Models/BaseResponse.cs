namespace todoappjs.Models
{
    public class BaseResponse
    {
        public List<string> Errors { get; set; }
        public bool HasError { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
