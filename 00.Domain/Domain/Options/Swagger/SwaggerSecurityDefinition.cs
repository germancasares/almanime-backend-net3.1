namespace Domain.Options.Swagger
{
    public class SwaggerSecurityDefinition
    {
        public string AuthorizationUrl { get; set; }
        public string Description { get; set; }
        public string In { get; set; }
        public string Name { get; set; }
        public string RefreshUrl { get; set; }
        public string Scheme { get; set; }
        public string TokenUrl { get; set; }
        public string Type { get; set; }
    }
}
