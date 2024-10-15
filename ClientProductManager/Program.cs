using ClientProductManager.Application.Services;
using ClientProductManager.Core;
using ClientProductManager.Core.Services.Contract;
using ClientProductManager.Infrastructure;
using ClientProductManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientProductService, ClientProductService>();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


var app = builder.Build();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var _dbContext = services.GetRequiredService<ApplicationDbContext>();

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    await _dbContext.Database.MigrateAsync();

}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error has been accured during apply the application");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", async context =>
{
    context.Response.Redirect("/Clients");
});

app.Run();
