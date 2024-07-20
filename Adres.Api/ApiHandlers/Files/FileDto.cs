namespace Adres.Api.ApiHandlers.Files
{
    public class FileDto
    {
        public Stream Content { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string ContentType { get; set; } = default!;
    }
}
