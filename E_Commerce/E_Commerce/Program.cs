﻿using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Controllers;
using PayPalCheckoutSdk.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(serviceProvider =>
{
    var environment = new SandboxEnvironment("AQyrmA2lkR0ea6a2M1k6VeV0zrOXy-xGOuIplLZQJF7yjlk3LSpscHp55rziTxW8mBGZ8CPczdCGYODn", "EICAHa2aQHVsT03EvMVeWqHnQcXRQPOfz7jn3QENZenBb4ShbIo3Vh37HZv-8d1NTGNG88zSwK7Hi5f7");
    return new PayPalHttpClient(environment);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>

options.AddPolicy("Development", builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
})
);


builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("Development");

app.Run();
