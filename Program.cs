using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using PM_MS.Models;
using PM_MS.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddDbContext<customdbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("ProdConn")));


builder.Services.AddCors(c=>c.AddPolicy("AllowOrigin",Options=>Options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));



builder.Services.AddControllersWithViews().AddNewtonsoftJson(Opt =>
Opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(Opt => Opt.SerializerSettings.ContractResolver = new DefaultContractResolver());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
