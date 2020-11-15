namespace FlightBooking.Core.Data
{
    using FlightBooking.Core.Models;

    public interface IDataLoader
    {
        FlightRoute LoadFlightRoute();
    }
}
