using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UI_IOT.Models
{
	public class ItemInfo
	{
		[Key]
		[JsonIgnore]
		public string ItemID { get; set; } = string.Empty;
        public double AccelerationX { get; set; } = 0;
		public double AccelerationY { get; set; } = 0;
		public double AccelerationZ { get; set; } = 0;
		public double GyroX { get; set; } = 0;
		public double GyroY { get; set; } = 0;
		public double GyroZ { get; set; } = 0;
		public double AverageAcc { get; set; } = 0;
		public double AverageRo { get; set; } = 0;
		public double AccSD { get; set; } = 0;
		public double GyroSD { get; set; } = 0;

    }
	public class Item
	{
		[Key]
		[JsonIgnore]
		public string ID { get; set; } = string.Empty;
		[JsonPropertyName("thoi_gian")]
		[Required]
		public DateTime Time { get; set; }
		[JsonPropertyName("trang_thai")]
		[Required]
		public string Status { get; set; } = string.Empty;
		[JsonPropertyName("gia_tri")]
		[Required]
		public ItemInfo? ItemInfo { get; set; } = default;
	}
}
