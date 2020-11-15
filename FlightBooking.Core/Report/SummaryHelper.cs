namespace FlightBooking.Core.Report
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FlightBooking.Core.Models;
    using FlightBooking.Core.RulesEngine;

    public class SummaryHelper
    {
        private readonly IProceedFlight _proceedFlight;

        public SummaryHelper(IProceedFlight proceedFlight)
        {
            this._proceedFlight = proceedFlight;
        }

        public SummaryDetail GetSummary(List<Passenger> passengers, FlightRoute flightRoute, Plane airCraft)
        {
            double costOfFlight = 0;
            double profitFromFlight = 0;
            var totalLoyaltyPointsAccrued = 0;
            var totalLoyaltyPointsRedeemed = 0;
            var totalExpectedBaggage = 0;
            var seatsTaken = 0;

            var summaryDetail = new SummaryDetail {Title = flightRoute.Title};

            foreach (var passenger in passengers)
            {
                switch (passenger.Type)
                {
                    case (PassengerType.General):
                        {
                            profitFromFlight += flightRoute.BasePrice;
                            totalExpectedBaggage++;
                            break;
                        }
                    case (PassengerType.LoyaltyMember):
                        {
                            if (passenger.IsUsingLoyaltyPoints)
                            {
                                var loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(flightRoute.BasePrice));
                                passenger.LoyaltyPoints -= loyaltyPointsRedeemed;
                                totalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                            }
                            else
                            {
                                totalLoyaltyPointsAccrued += flightRoute.LoyaltyPointsGained;
                                profitFromFlight += flightRoute.BasePrice;
                            }
                            totalExpectedBaggage += 2;
                            break;
                        }
                    case (PassengerType.AirlineEmployee):
                        {
                            totalExpectedBaggage += 1;
                            break;
                        }
                    case (PassengerType.Discounted):
                    {
                        profitFromFlight += flightRoute.BasePrice/2;
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                costOfFlight += flightRoute.BaseCost;
                seatsTaken++;
            }

            var profitSurplus = profitFromFlight - costOfFlight;

            summaryDetail.TotalPassengers = seatsTaken;
            summaryDetail.GeneralPassengers = GetPassengerCount(passengers, PassengerType.General);
            summaryDetail.LoyalPassengers = GetPassengerCount(passengers, PassengerType.LoyaltyMember);
            summaryDetail.AirlinePassengers = GetPassengerCount(passengers, PassengerType.AirlineEmployee);
            summaryDetail.DiscountedPassengers = GetPassengerCount(passengers, PassengerType.Discounted);
            summaryDetail.TotalExpectedBaggage = totalExpectedBaggage;
            summaryDetail.TotalCostOfFlight = costOfFlight;
            summaryDetail.TotalProfit = profitFromFlight;
            summaryDetail.ProfitSurplus = profitSurplus;
            summaryDetail.TotalLoyaltyPointsAccrued = totalLoyaltyPointsAccrued;
            summaryDetail.TotalLoyaltyPointsRedeemed = totalLoyaltyPointsRedeemed;

            summaryDetail.FlightMayProceed = this._proceedFlight.CanProceedFlight(summaryDetail, airCraft.NumberOfSeats, flightRoute.MinimumTakeOffPercentage);

            return summaryDetail;
        }

        private static int GetPassengerCount(List<Passenger> passengers, PassengerType type)
        {
            return passengers.Count(p => p.Type == type);
        }
    }
}
