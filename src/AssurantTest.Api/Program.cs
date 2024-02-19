using AssurantTest.Api.Middlewares;
using AssurantTest.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceAndConfiguration()
    .AddControllers(produces =>
    {
        produces.Filters.Add(new ProducesAttribute("application/json"));
        produces.Filters.Add(new ProducesAttribute("application/json"));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });


builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

app.UseCors();
app.GlobalExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
