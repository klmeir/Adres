using Adres.Api.DataSource;
using Adres.Api.Entities;
using MediatR;

namespace Adres.Api.ApiHandlers.Acquisitions
{
    public record AcquisitionDeleteCommand(int Id) : IRequest;

    public class AcquisitionDeleteCommandHandler : IRequestHandler<AcquisitionDeleteCommand>
    {
        private readonly IRepository<Acquisition> _repository;

        public AcquisitionDeleteCommandHandler(IRepository<Acquisition> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));


        public async Task<Unit> Handle(AcquisitionDeleteCommand request, CancellationToken cancellationToken)
        {
            var acquisition = await _repository.GetOneAsync(request.Id);

            _repository.DeleteAsync(acquisition);

            return Unit.Value;
        }        
    }
}
