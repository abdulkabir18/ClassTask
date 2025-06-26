using ClassTask.Context;
using ClassTask.Repositories.Implementaions;
using ClassTask.Repositories.Interfaces;
using ClassTask.Services.Implementations;
using ClassTask.Services.Interfaces;
using ClassTask.UnitOfWork.Implementation;
using ClassTask.UnitOfWork.Interface;
using ClassTask.Validations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DevString")!));

builder.Services.AddScoped<IMediaUserRepository, MediaUserRepository>();
builder.Services.AddScoped<IMediaUserService, MediaUserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddValidatorsFromAssemblyContaining<SignUpValidator>();


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


public partial class Program
{

}