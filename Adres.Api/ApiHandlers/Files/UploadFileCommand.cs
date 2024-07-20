using Adres.Api.Services;
using MediatR;

namespace Adres.Api.ApiHandlers.Files
{
    public record UploadFileCommand(FileDto File) : IRequest<string>;

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, string>
    {
        private readonly IFileStorageService _fileStorage;

        public UploadFileCommandHandler(IFileStorageService fileStorage) =>
            _fileStorage = fileStorage ?? throw new ArgumentNullException(nameof(fileStorage));


        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            return await _fileStorage.UploadAsync(request.File.Content, request.File.Name, request.File.ContentType);
        }
    }
}
