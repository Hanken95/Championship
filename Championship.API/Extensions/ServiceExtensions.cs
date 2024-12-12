namespace Championship.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, string name = "Any")
        {
            if (name == "Any")
            {
                services.AddCors(builder =>
                {
                    builder.AddPolicy("AllowAny", policyBuilder =>
                        policyBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        );
                });
            }
        }
    }
}
