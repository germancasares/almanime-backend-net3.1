namespace Domain.Configurations
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessExpirationDays { get; set; }
        public int RefreshExpirationDays { get; set; }
    }
}
