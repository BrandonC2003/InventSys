namespace InventSys.Domain.Entities
{
    public class ProductAlertSettings
    {
        public int CheckIntervalSeconds { get; set; }
        public int AlertResendIntervalMinutes { get; set; }
    }
}
