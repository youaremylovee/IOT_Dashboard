using System.Runtime.CompilerServices;
using System.Text;
using UI_IOT.Models;

namespace UI_IOT.Services
{
	public class NotifyService
	{
		private DateTime _now = DateTime.Now;
        public readonly Config _config;

        public NotifyService(Config config)
		{
			this._config = config;
		}
		public async Task<int> Notify()
		{
			if (DateTime.Now > _now.AddSeconds(5))
			{
				_now = DateTime.Now;
                using (HttpClient client = new HttpClient())
                {
                    var url = _config.EndpointApi;
                    var data = _config.AlertMessage;
                    var content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
                    try
                    {
                        var response = await client.PostAsync(url, content);
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }
			return 1;
		}
	}
}
