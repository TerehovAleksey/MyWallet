using FluentValidation.AspNetCore;
using Azure.Identity;

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
        services.AddApiVersion();
        services.ConfigureIdentity();

        services.AddValidation();
        services.AddFluentValidationAutoValidation();
        services.AddControllers();      
        services.AddSwagger();

        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddJwt(_configuration);
        services.AddMemoryCache();

        //My Services
        services.AddScoped<ITokenService, TokenService>();
        services.AddDatabaseContext(_configuration, _environment);
        services.AddServices();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_environment.IsDevelopment())
        {
           // app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseDeveloperExceptionPage();

        app.UseSerilogRequestLogging();
        app.UseStaticFiles();
        app.UseRouting();   
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}