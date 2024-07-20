using Adres.Api.DataSource;
using Adres.Api.Entities;
using MediatR;

namespace Adres.Api.ApiHandlers.Acquisitions
{
    public record AcquisitionQuery(int Id) : IRequest<Acquisition>;

    public class AcquisitionGetQueryHandler : IRequestHandler<AcquisitionQuery, Acquisition>
    {
        private readonly IRepository<Acquisition> _repository;

        public AcquisitionGetQueryHandler(IRepository<Acquisition> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));


        public async Task<Acquisition> Handle(AcquisitionQuery request, CancellationToken cancellationToken)
        {
            return (await _repository.GetManyAsync(filter: x => x.Id == request.Id, includeStringProperties: nameof(Acquisition.Documentation))).FirstOrDefault();
        }        
    }
}
