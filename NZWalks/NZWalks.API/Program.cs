// it creates WebApplication Builder class. which we can use to inject the dependencies into the services collection

using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// inject NZWalksDbContext into our services collection
builder.Services.AddDbContext<NZWalksDbContext>(
    options=>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));

    }

    );

builder.Services.AddScoped<IRegionRepository, RegionRepository>();


builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddScoped<IWalkDifficultyRepository, WalkDifficultyRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly); // to transfer Domain model to DTOs model
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
