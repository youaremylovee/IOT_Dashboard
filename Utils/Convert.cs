namespace UI_IOT.Utils
{
    public class Convert
    {
        public static string ConvertStatus(string status) 
        {
            return status.ToLower().Trim() switch
            {
                "ngã" => "fall",
                "đang tính toán" => "calculating",
                "ngồi" => "sit",
                "đi bộ" => "walk",
                "nằm" => "lie",
                "chạy" => "run",
                _ => "unknown"
            };
        }
    }
}
