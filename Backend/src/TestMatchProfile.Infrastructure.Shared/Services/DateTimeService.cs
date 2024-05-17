using System;
using TestMatchProfile.Application.Interfaces;

namespace TestMatchProfile.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}