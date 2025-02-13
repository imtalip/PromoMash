namespace IdentityServerCore
{
    using EntityFramework.PostgreSQL.Services;

    public class Program
    {
        public static void Main(string[] args)
        {
            var isFillFakeData = args.Contains("fillFakeData");
            if (isFillFakeData)
            {
                args = args.Except(new[] { "fillFakeData" }).ToArray();
            }

            var builder = WebApplication.CreateBuilder(args);

            if (isFillFakeData)
            {
                IdentityServerFillService.FillData(builder.Configuration);
            }

            Startup.ConfigureServices(builder);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}
