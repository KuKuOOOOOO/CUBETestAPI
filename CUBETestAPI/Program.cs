using CUBETestAPI.Helpers;
using CUBETestAPI.Repository.Data;
using CUBETestAPI.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

#region Dependency Injection
builder.Services.AddSingleton<IDatabaseService>(sp => new DatabaseService(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<RsaCryptoService>();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Add Request and Response Logging Middleware
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
