namespace FlightBooking.Core.Data
{
    using FlightBooking.Core.Models;

    public class DataLoader : IDataLoader
    {
        public FlightRoute LoadFlightRoute()
        {
            var londonToParis = new FlightRoute("London", "Paris")
            {
                BaseCost = 50,
                BasePrice = 100,
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };

            return londonToParis;
        }
    }
}
