namespace IdentityServerCore
{
    using EntityFramework.PostgreSQL.Extensions;

    public static class Startup
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddIS4DbContext(builder.Configuration);
        }
    }
}
