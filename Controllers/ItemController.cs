using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Swashbuckle.AspNetCore.Annotations;
using UI_IOT.Models;
using UI_IOT.Services;

namespace UI_IOT.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ItemController : ControllerBase
	{
        private readonly ItemService _service;
        private readonly IHubContext<ChartHub> _hubContext;
        private readonly NotifyService _notifyService;
        private readonly Config _config;

        public ItemController(ItemService _service, IHubContext<ChartHub> hubContext, NotifyService notify, Config config) 
		{
			this._service = _service;
			this._hubContext = hubContext;
			this._notifyService = notify;
			this._config = config;

        }
		[SwaggerOperation(
			Summary = "Get item collection by filter",
			Description = "Get item collection by filter"
		)]
		[HttpPost("Filter")]
		public async Task<IEnumerable<Item>> Get([FromBody] ItemFilter filter)
		{
			return await _service.Filter(filter);
		}
        [SwaggerOperation(
            Summary = "Live data for streaming & chart",
            Description = "Live data for streaming & chart"
        )]
        [HttpPost("live")]
        public async Task<IActionResult> ReceiveData([FromBody] Item data)
        {
			var response = "OK";
            data.Time = DateTime.UtcNow.AddHours(7);
			data.Status = Utils.Convert.ConvertStatus(data.Status);
            await _hubContext.Clients.All.SendAsync("UpdateChart", data);
			if (this._config.AlertLevel.Equals(data.Status))
			{
				await _notifyService.Notify();
				response = "ALERT";
			}
			if (data.Status != "calculating")
			{
				await _service.AddItem(data);
			}
			return Ok(response);
        }
    }
}
