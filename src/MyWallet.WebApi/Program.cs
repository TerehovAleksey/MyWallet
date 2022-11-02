using Microsoft.EntityFrameworkCore;
using MyWallet.Core.Dal;
using MyWallet.Services;
using MyWallet.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

//My Services
builder.Services.AddDatabaseContext(builder.Configuration, builder.Environment);
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IExpenseService, ExpenseService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//DB Migration
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
