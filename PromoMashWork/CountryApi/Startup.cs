namespace CountryApi
{
    using Microsoft.AspNetCore.DataProtection;
    using EntityFramework.PostgreSQL.Extensions;
    using CountryApi.Extensions;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        private readonly IConfiguration _config;
        private ApiConfig _apiConfig;

        private const string ApiScopePolicy = "ApiScope";

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _apiConfig = _config.GetSection("ApiConfig").Get<ApiConfig>();

            services.AddSingleton(_apiConfig);

            services
                .AddCountryDbContext(_config)
                .AddServices(_config);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"CountryAPI", Version = "v1" });
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = _config["Authority"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ApiScopePolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", _apiConfig.Scope);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization(ApiScopePolicy); });
        }
    }
}
