using Microsoft.EntityFrameworkCore;
using Tournament_Api.Extensions;
using Tournament_Core.Repositories;
using Tournament_Data.Data;
using Tournament_Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TourDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TourDbContext") ?? throw new InvalidOperationException("Connection string 'TourDbContext' not found.")));


builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUoW, UoW>();
builder.Services.AddAutoMapper(typeof(TournamentMappings));

builder.Services.AddControllers();
builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
.AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
await app.SeedDataAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
