using Adres.Api.DataSource;
using Adres.Api.Entities;
using MediatR;
using System.Globalization;

namespace Adres.Api.ApiHandlers.Acquisitions
{
    public record AcquisitionsQuery(string StartDate, string EndDate, string? Search) : IRequest<IEnumerable<Acquisition>>;

    public class AcquisitionsQueryHandler : IRequestHandler<AcquisitionsQuery, IEnumerable<Acquisition>>
    {
        private readonly IRepository<Acquisition> _repository;
        private readonly string DateFormat = "yyyy-MM-dd";

        public AcquisitionsQueryHandler(IRepository<Acquisition> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));


        public async Task<IEnumerable<Acquisition>> Handle(AcquisitionsQuery request, CancellationToken cancellationToken)
        {
            if (!DateTime.TryParseExact(request.StartDate, DateFormat, null, DateTimeStyles.None, out var startDateDt))
            {
                startDateDt = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            }

            if (!DateTime.TryParseExact(request.EndDate, DateFormat, null, DateTimeStyles.None, out var endDateDt))
            {
                endDateDt = startDateDt.AddMonths(1).AddDays(-1);
            }

            DateOnly start = new DateOnly(startDateDt.Year, startDateDt.Month, startDateDt.Day);
            DateOnly end = new DateOnly(endDateDt.Year, endDateDt.Month, endDateDt.Day);
            string search = request.Search;

            return await _repository
                .GetManyAsync(filter:
                    x => x.AcquisitionDate >= start
                    && x.AcquisitionDate <= end &&
                    (
                        string.IsNullOrWhiteSpace(search) || 
                        x.Unit.Contains(search) ||
                        x.Type.Contains(search) ||
                        x.Supplier.Contains(search)
                    ),
                includeStringProperties: nameof(Acquisition.Documentation));
        }

        private bool ValidDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return true;

            return DateTime.TryParseExact(date, DateFormat, null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
