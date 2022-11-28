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
        //My Services
        services.AddDatabaseContext(_configuration, _environment);
        services.AddServices();
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddMemoryCache();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHttpsRedirection();
        }
        
        app.UseSerilogRequestLogging();
        app.UseRouting();
        
        //app.UseAuthentication();
        //app.UseAuthorization();
        
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}