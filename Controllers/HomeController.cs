using Microsoft.AspNetCore.Mvc;
using UI_IOT.Models;
using UI_IOT.Services;

namespace UI_IOT.Controllers
{
	public class HomeController : Controller
	{
		private readonly ItemService _service;

		public HomeController(ItemService service)
		{
			this._service = service;
		}
		public async Task<IActionResult> Filter([FromQuery] ItemFilter filter)
		{
			var items  = await _service.Filter(filter);
			return View(items.ToList());
		}
        public async Task<IActionResult> Index()
        {
			ItemFilter filter = new ItemFilter();
            var items = await _service.Filter(filter);
            return View(items.ToList());
        }
        public IActionResult Dashboard()
		{
			return View();
		}
	}
}
