using Adres.Api.ApiHandlers.Files;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adres.Api.ApiHandlers.Acquisitions
{
    public static class FilesApi
    {
        public static RouteGroupBuilder MapFiles(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapPost("/", async (IMediator mediator, [FromForm] IFormFile file) =>
            {
                FileDto? fileDto = null;
                if (file is not null)
                {
                    fileDto = new FileDto
                    {
                        Content = file.OpenReadStream(),
                        Name = file.FileName,
                        ContentType = file.ContentType
                    };
                }
                var fileUrl = await mediator.Send(new UploadFileCommand(fileDto!));                
                return new FileUploadResponseDto { Name = file.FileName, Url = fileUrl };
            })
            .Produces(StatusCodes.Status200OK, typeof(string))
            .DisableAntiforgery();

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
