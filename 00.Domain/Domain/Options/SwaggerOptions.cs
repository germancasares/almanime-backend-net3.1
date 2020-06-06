using Domain.Options.Swagger;

namespace Domain.Options
{
    public class SwaggerOptions
    {
        public const string Accessor = "SwaggerOptions";

        public SwaggerDoc Doc { get; set; }
        public SwaggerSecurityDefinition SecurityDefinition { get; set; }
        public SwaggerSecurityRequirement SecurityRequirement { get; set; }
        public SwaggerUI UI { get; set; }
    }
}
