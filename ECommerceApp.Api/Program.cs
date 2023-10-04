using ECommerceApp.Api;
using ECommerceApp.Api.Errors;
using ECommerceApp.Application;
using ECommerceApp.Application.Mapping;
using ECommerceApp.Application.Services.Authentication;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
                .AddApi()
                .AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddRepository(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
