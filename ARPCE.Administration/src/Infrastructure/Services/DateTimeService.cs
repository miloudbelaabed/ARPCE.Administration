using ARPCE.Administration.Application.Common.Interfaces;

namespace ARPCE.Administration.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
