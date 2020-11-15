using System;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core.Report
{
    public class SummaryReport
    {
        private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
        private readonly string _newLine = Environment.NewLine;
        private const string Indentation = "    ";

        private readonly StringBuilder result = new StringBuilder();

        public string GetSummary(SummaryDetail summaryDetail)
        {
            this.AppendSummary("Flight summary for ", summaryDetail.Title, true);
            this.AppendSummary("Total passengers: ", summaryDetail.TotalPassengers.ToString());
            this.AppendSummary("General sales: ", summaryDetail.GeneralPassengers.ToString(), false, true);
            this.AppendSummary("Loyalty member sales: ", summaryDetail.LoyalPassengers.ToString(), false, true);
            this.AppendSummary("Airline employee comps: ", summaryDetail.AirlinePassengers.ToString(), false, true);
            this.AppendSummary("Discounted employee comps: ", summaryDetail.DiscountedPassengers.ToString(), true, true);
            this.AppendSummary("Total expected baggage: ", summaryDetail.TotalExpectedBaggage.ToString(), true);
            this.AppendSummary("Total revenue from flight: ", summaryDetail.TotalProfit.ToString());
            this.AppendSummary("Total cost from flight: ", summaryDetail.TotalCostOfFlight.ToString());

            this.AppendSummary(summaryDetail.ProfitSurplus > 0 ? "Flight generating profit of: " : "Flight losing money of: ", 
                Convert.ToString(summaryDetail.ProfitSurplus), true);

            this.AppendSummary("Total loyalty points given away: ", summaryDetail.TotalLoyaltyPointsAccrued.ToString());
            this.AppendSummary("Total loyalty points redeemed: ", summaryDetail.TotalLoyaltyPointsRedeemed.ToString(), true);

            this.AppendSummary(summaryDetail.FlightMayProceed ? "THIS FLIGHT MAY PROCEED" : "FLIGHT MAY NOT PROCEED", string.Empty);

            return result.ToString();
        }

        private void AppendSummary(string heading, string value, bool addVerticalWhiteSpace = false, bool addIndentation = false)
        {
            if (addIndentation)
            {
                result.Append($"{Indentation}");
            }

            result.Append($"{heading} {value}");
            result.AppendLine();

            if (addVerticalWhiteSpace)
            {
                result.AppendLine();
            }
        }
    }
}
