using erbildaphneAPI.DataAccess.Data;
using erbildaphneAPI.DataAccess.Repositories;
using erbildaphneAPI.Entity.Repositories;
using erbildaphneAPI.Service.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")), ServiceLifetime.Scoped
);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


var configuration = builder.Configuration;

builder.Services.AddExtensions(configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        //policy.WithOrigins("https://www.erbildaphne.com", "https://erbildaphne.com","https://localhost:7000")
        //      .AllowAnyHeader()
        //      .AllowAnyMethod().WithMethods("GET", "POST", "PUT", "DELETE");
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod().WithMethods("GET", "POST", "PUT", "DELETE"); ;
    });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//ConfigureCulture();

app.UseCors("MyCorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
