namespace Authentication.Dtos
{
    public class AuthServiceResult
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
    }
}
