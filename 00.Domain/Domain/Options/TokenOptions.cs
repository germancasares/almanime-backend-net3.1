namespace Domain.Configurations
{
    public class TokenOptions
    {
        public const string Accessor = "TokenOptions";

        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessExpirationDays { get; set; }
        public int RefreshExpirationDays { get; set; }
    }
}
