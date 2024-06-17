﻿using Microsoft.AspNetCore.ResponseCompression;
using System.Configuration;
using MAS_19c_Cieślak_Jan_s24110.Server.Data;
using Microsoft.EntityFrameworkCore;
using MAS_19c_Cieślak_Jan_s24110.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TrainContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDepartureService, DepartureService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();   

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
