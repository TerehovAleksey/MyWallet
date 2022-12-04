namespace MyWallet.WebApi;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //AddIdentity должен стоять выше AddJwtBearer, иначе редирект при 401, 403
        services.AddScoped<ITokenService, TokenService>();
        services.AddDatabaseContext(_configuration, _environment);
        services.AddServices();

        services.AddApiVersion();
        services.ConfigureIdentity();

        services.AddValidation();
        services.AddFluentValidationAutoValidation();
        services.AddControllers();      
        services.AddSwagger();

        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddJwt(_configuration);
        services.AddMemoryCache();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseSerilogRequestLogging();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}