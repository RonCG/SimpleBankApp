using Microsoft.AspNetCore.Mvc.Infrastructure;
using SimpleBankApp.Api.Errors;
using SimpleBankApp.Application.Common;
using SimpleBankApp.Infrastructure.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ProblemDetailsFactory, SimpleBankAppProblemDetailsFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseAuthorization();

app.MapControllers();

app.Run();
