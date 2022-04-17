using Common.Models;

namespace Common.DTOs
{
    public class PayInfosDTO
    {
        public PayInfosDTO()
        {
            TimeSheet = new TimeSheet();
            FederalTaxRate = 0.15m;
            ProvincialTaxRate = 0.20m;
            HourlyRate = 20m;
            TotalPay = 0m;
            TotalPayBeforeTaxes = 0m;
            TotalHours = 0m;
            TotalFederalTaxes = 0m;
            TotalProvincialTaxes = 0m;
        }

        public TimeSheet TimeSheet { get; set; }
        public decimal TotalHours { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal FederalTaxRate { get; set; }
        public decimal ProvincialTaxRate { get; set; }
        public decimal TotalPayBeforeTaxes { get; set; }

        public decimal TotalPay { get; set; }
        public decimal TotalFederalTaxes { get; set; }
        public decimal TotalProvincialTaxes { get; set; }
    }
}