using Library.WebApi.Models;
using Library.WebApi.Repositories.Interfaces;
using Library.WebApi.Repositories.RepositoryClasses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IRepositoryBase<Book>, RepositoryBase<Book>>();
builder.Services.AddScoped<IRepositoryBase<Student>, RepositoryBase<Student>>();
builder.Services.AddScoped<IRepositoryBase<BorrowInfo>, RepositoryBase<BorrowInfo>>();

builder.Services.AddScoped<SchoolLibraryDbContext>();

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
