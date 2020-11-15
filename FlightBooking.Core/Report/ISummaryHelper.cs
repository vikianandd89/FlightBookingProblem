using System.Collections.Generic;
using FlightBooking.Core.Models;

namespace FlightBooking.Core.Report
{
    public interface ISummaryHelper
    {
        SummaryDetail GetSummary(List<Passenger> passengers, FlightRoute flightRoute, Plane airCraft);
    }
}
