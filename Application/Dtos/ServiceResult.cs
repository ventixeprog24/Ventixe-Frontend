namespace Application.Dtos
{
    public class ServiceResult
    {
        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
    }
}
