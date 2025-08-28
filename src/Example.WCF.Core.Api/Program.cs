using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Example.WCF.Core.Application;
using Example.WCF.Core.Infrastructure;
using Example.WCF.Core.Infrastructure.Contracts;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add CoreWCF services
builder.Services.AddServiceModelServices(); // <-- register CoreWCF services
builder.Services.AddServiceModelMetadata(); // optional, for WSDL publishing
builder.Services.AddInfrastructure();

builder.Services.AddScoped<CalculatorService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseServiceModel(serviceBuilder =>
{
	serviceBuilder.AddService<CalculatorService>(serviceOptions =>
    {
        // ✅ Show server exceptions in SOAP fault (debug only!)
        serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });
	serviceBuilder.AddServiceEndpoint<CalculatorService, ICalculatorService>(
		new BasicHttpBinding(), "/CalculatorService.svc");

	// Enable WSDL publishing
	ServiceMetadataBehavior serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
	serviceMetadataBehavior.HttpGetEnabled = true;
});

app.UseHttpsRedirection();

app.Run();
