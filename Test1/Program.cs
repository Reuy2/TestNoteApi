using Microsoft.Extensions.Hosting.Internal;
using Test1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CancellationToken shutDownToken =  app.Lifetime.ApplicationStopping;

shutDownToken.Register(DataOperation.SaveClients);

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

DataOperation.LoadClients();

DataOperation.PeriodicSaveClients(new TimeSpan(0, 0, 20), shutDownToken);

app.Run();
