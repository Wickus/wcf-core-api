using Example.WCF.Core.Api.Formatters;
using Example.WCF.Core.Application;
using Example.WCF.Core.Infrastructure;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

builder.WebHost.ConfigureKestrel((context, options) =>
{
  options.Configure(context.Configuration.GetSection("Kestrel"));
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddControllers(options =>
{
  options.InputFormatters.Insert(0, new SoapInputFormatter());
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
