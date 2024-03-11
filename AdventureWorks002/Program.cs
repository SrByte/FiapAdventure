using AdventureWorks002;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 var connection=string.Empty;
connection=builder.Configuration.GetConnectionString("Sql2netr");

builder.Services.AddDbContext<AdventureFiapContext>(option =>option.UseSqlServer(connection));

//builder.Services.AddCors();
var app = builder.Build();
//app.UseCors(config => config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}
//app.UseCors(x => x
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .SetIsOriginAllowed(origin => true) // allow any origin
//    .AllowCredentials()); // allow credential
app.UseHttpsRedirection();
app.UseRouting();

app.MapGet("/salesOrderHeader/{id}", async (int id, [FromServices] AdventureFiapContext context) =>
{
    return await context.SalesOrderDetails.Where(c=>c.SalesOrderId==id).ToListAsync();
})
.WithName("Get SalesOrderHeader By ID")
.WithOpenApi();
//app.UseCors(config    => config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// global cors policy
//app.UseCors(x => x
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .SetIsOriginAllowed(origin => true) // allow any origin
//    .AllowCredentials()); // allow credentials

//app.UseAuthentication();
//app.UseAuthorization();


app.Run();