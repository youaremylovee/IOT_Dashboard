namespace UI_IOT.Models
{
    public class Config
    {
        public readonly string AlertLevel;
        public readonly int TimeBetweenSend;
        public readonly string AlertMessage;
        public readonly string EndpointApi;
        public Config(IConfiguration configuration)
        {
            this.AlertLevel = configuration.GetValue<string>("AlertLevel") ?? "run";
            this.TimeBetweenSend = configuration.GetValue<int>("TimeBetweenSend");
            this.AlertMessage = configuration.GetValue<string>("AlertMessage") ?? "Cảnh báo";
            this.EndpointApi = configuration.GetValue<string>("EndpointApi") ?? "";
        }
    }
}
