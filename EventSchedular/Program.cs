using EventSchedularProject.Data;
using EventSchedularProject.Models;
using EventSchedularProject.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
  


var builder = WebApplication.CreateBuilder(args);




// create db is does not exist
// register the dbcontext class
builder.Services.AddDbContext<EventSchedularContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
 );

builder.Services.AddTransient<IEvent, EventSchedular>();
//builder.Services.AddScoped<IEvent, EventSchedular>();
// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<EventSchedularContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        // var logger = services.GetRequiredService<ILogger<Program>>();
        //logger.LogError(ex, "An error occurred creating the DB.");
        app.UseExceptionHandler("/Home/Error");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


