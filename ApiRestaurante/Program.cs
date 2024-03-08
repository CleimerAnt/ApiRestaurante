using Microsoft.Data.SqlClient;
using ApiRestaurante.Infraestructure.Identity;
using ApiRestaurante.Infraestructure.Persistence;
using ApiRestaurante.Infraestructure.Identity.Seeds;
using ApiRestaurante.Core.Application;
using ApiRestaurante.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityInfraestructure(builder.Configuration);
builder.Services.AddPersistenceInfraestructure(builder.Configuration);
builder.Services.AddAplicationLayer(builder.Configuration);


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddApiVersioning();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
await app.Services.AddIdentitySeedsInfraestructure();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UserSwaggerExtension();
app.UseHealthChecks("/health");
app.UseSession();

app.MapControllers();

app.Run();
