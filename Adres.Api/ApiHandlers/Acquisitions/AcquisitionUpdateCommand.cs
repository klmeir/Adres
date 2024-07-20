using Adres.Api.DataSource;
using Adres.Api.Entities;
using MediatR;

namespace Adres.Api.ApiHandlers.Acquisitions
{
    public record AcquisitionUpdateCommand(
        int Id,
        decimal Budget,
        string Unit,
        string Type,
        int Quantity,
        decimal UnitValue,
        decimal TotalValue,
        DateOnly AcquisitionDate,
        string Supplier,
        List<DocumentationFile> Documentation) : IRequest<Acquisition>;

    public class AcquisitionUpdateCommandHHandler : IRequestHandler<AcquisitionUpdateCommand, Acquisition>
    {
        private readonly IRepository<Acquisition> _repository;

        public AcquisitionUpdateCommandHHandler(IRepository<Acquisition> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));


        public async Task<Acquisition> Handle(AcquisitionUpdateCommand request, CancellationToken cancellationToken)
        {
            var acquisition = await _repository.GetOneAsync(request.Id);

            acquisition.Update(
                request.Budget,
                request.Unit,
                request.Type,
                request.Quantity,
                request.UnitValue,
                request.TotalValue,
                request.AcquisitionDate,
                request.Supplier,
                request.Documentation);

            _repository.UpdateAsync(acquisition);

            return acquisition;
        }        
    }
}
