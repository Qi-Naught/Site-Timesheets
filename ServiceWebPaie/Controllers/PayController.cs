using System.Threading.Tasks;
using Common.Data;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using ServiceWebPaie.ApplicationLogic;

namespace ServiceWebPaie.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private readonly TimeSheetContext context;

        public PayController(TimeSheetContext context)
        {
            this.context = context;
        }

        [HttpPost]
        //[EnableCors]
        public async Task<ActionResult<PayInfosDTO>> Post(PayRequestDTO payRequestDto)
        {
            PayInfosDtoProcessing payInfosDtoProcessing = new(context);
            PayInfosDTO payInfosDto = await payInfosDtoProcessing.LoadModel(payRequestDto);
            if (payInfosDto == null) return NotFound("The selected day does not contain a Time Sheet!");
            return payInfosDto;
        }
    }
}