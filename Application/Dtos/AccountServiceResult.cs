namespace Application.Dtos
{
    public class AccountServiceResult
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
    }
}
