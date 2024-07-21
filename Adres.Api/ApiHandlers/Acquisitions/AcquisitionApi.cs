using Adres.Api.Entities;
using Adres.Api.Filters;
using MediatR;

namespace Adres.Api.ApiHandlers.Acquisitions
{
    public static class AcquisitionApi
    {
        public static RouteGroupBuilder MapAcquisitions(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapGet("/", async (IMediator mediator, [Validate][AsParameters] AcquisitionsQuery acquisitionsQuery) =>
            {
                return Results.Ok(await mediator.Send(acquisitionsQuery));
            })
            .Produces(StatusCodes.Status200OK, typeof(List<Acquisition>));

            routeHandler.MapGet("/{id}", async (IMediator mediator, int id) =>
            {
                return Results.Ok(await mediator.Send(new AcquisitionQuery(id)));
            })
            .WithName("GetAcquisition")
            .Produces(StatusCodes.Status200OK, typeof(Acquisition))
            .Produces(StatusCodes.Status404NotFound);

            routeHandler.MapPost("/", async (IMediator mediator, [Validate] AcquisitionAddCommand acquisition) =>
            {
                var savedAcquisition = await mediator.Send(acquisition);
                return Results.CreatedAtRoute("GetAcquisition", new { id = savedAcquisition.Id }, savedAcquisition);
            })
            .Produces(StatusCodes.Status201Created, typeof(Acquisition))
            .Produces(StatusCodes.Status400BadRequest);

            routeHandler.MapPut("/{id}", async (IMediator mediator, int id, [Validate] AcquisitionUpdateCommand acquisition) =>
            {
                if (id != acquisition.Id) return Results.BadRequest();
                return Results.Ok(await mediator.Send(acquisition));
            })
            .Produces(StatusCodes.Status200OK, typeof(Acquisition))
            .Produces(StatusCodes.Status400BadRequest);

            routeHandler.MapDelete("/{id}", async (IMediator mediator, int id) =>
            {
                return Results.Ok(await mediator.Send(new AcquisitionDeleteCommand(id)));
            })           
           .Produces(StatusCodes.Status200OK);

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
