using Application.DependencyInjection;
using Infrastructure.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn moreabout configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.ApplicationServices(); // Assuming this is a method that adds application services
builder.Services.AddInfrastructureServices(builder.Configuration);
    /*.ApplicationServices();*/ // Assuming this is a method that adds application services


var app = builder.Build();

// global cors policy
app.UseCors("Cors");
//or
//app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openApi/v1.json", "api");
    });
    
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

