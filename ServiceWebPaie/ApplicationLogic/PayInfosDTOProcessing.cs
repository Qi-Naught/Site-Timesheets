using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.DTOs;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ServiceWebPaie.ApplicationLogic
{
    public class PayInfosDtoProcessing
    {
        private readonly TimeSheetContext context;

        public PayInfosDtoProcessing(TimeSheetContext context)
        {
            this.context = context;
        }

        public async Task<PayInfosDTO> LoadModel(PayRequestDTO requestedData)
        {
            PayInfosDTO model = new()
            {
                TimeSheet = await LoadTimeSheet(requestedData)
            };
            if (model.TimeSheet == null) return null;
            CalculateHours(model);
            CalculatePay(model);
            return model;
        }

        private static void CalculateHours(PayInfosDTO model)
        {
            TimeSpan interval = model.TimeSheet.EndDateTime.Subtract(model.TimeSheet.StartDateTime);
            model.TotalHours += decimal.Divide((decimal)interval.TotalMinutes, 60);
        }

        private async Task<TimeSheet> LoadTimeSheet(PayRequestDTO requestedData)
        {
            return (await context.TimeSheets.Include(c => c.User).ToListAsync()).FirstOrDefault(timesheet =>
                timesheet.User.Id == requestedData.Id &&
                requestedData.StartDateTime.Date == timesheet.StartDateTime.Date);
        }

        private static void CalculatePay(PayInfosDTO model)
        {
            model.TotalPay = model.HourlyRate * model.TotalHours;
            model.TotalPayBeforeTaxes = model.TotalPay;
            model.TotalFederalTaxes = model.TotalPay * model.FederalTaxRate;
            model.TotalProvincialTaxes = model.TotalPay * model.ProvincialTaxRate;
            model.TotalPay -= model.TotalFederalTaxes + model.TotalProvincialTaxes;
        }
    }
}