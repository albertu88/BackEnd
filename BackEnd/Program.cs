using BackEnd.Applicaton.Services.Contracts;
using BackEnd.Applicaton.Services.Implementations;
using BackEnd.Domain.Services.Contracts;
using BackEnd.Domain.Services.Implementations;
using BackEnd.Infrastructure.Models.Model;
using BackEnd.Infrastructure.Models.Repository;
using BackEnd.Infrastructure.Models.RepositoryContracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7090",
            ValidAudience = "https://localhost:7090",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mySecretKey@12345"))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<UserContext>(opts =>
    opts.UseNpgsql(builder.Configuration["ConnectionString:UserDB"]));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<IUserDomainService, UserDomainService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<ITokenService, TokenService>();








builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseCors("EnableCORS");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
