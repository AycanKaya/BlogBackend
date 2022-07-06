using Services.Contracts;
using Services.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Singleton Transient Scoped
/*
builder.Services.AddHttpClient<IEpiasService, Episservice>(c =>
{
    c.BaseAddress = new Uri("https://seffaflik.epias.com.tr/transparency/service/");
});
 */ 
builder.Services.AddScoped<IEpiasService, Episservice>();

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

app.Run();
