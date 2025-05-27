namespace Presentation.Dtos
{
    public class LocationServiceResult
    {
        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class LocationServiceResult<T> : LocationServiceResult
    {
        public T? Result { get; set; }
    }
}
