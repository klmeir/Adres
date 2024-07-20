using Adres.Api.DataSource;
using Adres.Api.Entities;
using MediatR;

namespace Adres.Api.ApiHandlers.Acquisitions
{
    public record AcquisitionsQuery() : IRequest<IEnumerable<Acquisition>>;

    public class AcquisitionsQueryHandler : IRequestHandler<AcquisitionsQuery, IEnumerable<Acquisition>>
    {
        private readonly IRepository<Acquisition> _repository;

        public AcquisitionsQueryHandler(IRepository<Acquisition> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));


        public async Task<IEnumerable<Acquisition>> Handle(AcquisitionsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetManyAsync(includeStringProperties: nameof(Acquisition.Documentation));
        }        
    }
}
