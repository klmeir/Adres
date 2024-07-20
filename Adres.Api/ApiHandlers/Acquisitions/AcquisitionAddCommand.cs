using Adres.Api.DataSource;
using Adres.Api.Entities;
using MediatR;

namespace Adres.Api.ApiHandlers.Acquisitions
{
    public record AcquisitionAddCommand(
        decimal Budget,
        string Unit,
        string Type,
        int Quantity,
        decimal UnitValue,
        decimal TotalValue,
        DateOnly AcquisitionDate,
        string Supplier,
        List<DocumentationFile> Documentation) : IRequest<Acquisition>;

    public class AcquisitionAddCommandHHandler : IRequestHandler<AcquisitionAddCommand, Acquisition>
    {
        private readonly IRepository<Acquisition> _repository;

        public AcquisitionAddCommandHHandler(IRepository<Acquisition> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));


        public async Task<Acquisition> Handle(AcquisitionAddCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(new Acquisition(
                request.Budget,
                request.Unit,
                request.Type,
                request.Quantity,
                request.UnitValue,
                request.TotalValue,
                request.AcquisitionDate,
                request.Supplier,
                request.Documentation));
        }        
    }
}
