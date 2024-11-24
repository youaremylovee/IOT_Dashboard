using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace UI_IOT.Models
{
	public class ItemFilter
	{
		public string Status { get; set; } = string.Empty;
		public DateTime? From { get; set; }
		public DateTime? To { get; set; }
		private string _dateSetted = string.Empty;
		public string DateSetted
		{
			get
			{
				return _dateSetted;
			}
			set
			{
				string[] strings = value.Split('-');
                if (strings.Length == 2)
				{
                    From = DateTime.ParseExact(strings[0].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    To = DateTime.ParseExact(strings[1].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
				_dateSetted = value;
			}
		}
		
	}
}
