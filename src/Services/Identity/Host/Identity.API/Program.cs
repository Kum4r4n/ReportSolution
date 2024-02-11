using Common.Authentication;
using Identity.Application.Interfaces.IRepositories;
using Identity.Application.Interfaces.IServices;
using Identity.Application.Services;
using Identity.Infrastructure.Configuration;
using Identity.Infrastructure.Context;
using Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//db
builder.Services.AddDbContext<IdentityDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));


//services
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

//repos
builder.Services.AddScoped<IUserRepository, UserRepository>();

var tokenSetting = builder.Configuration.GetSection("TokenSetting").Get<TokenSetting>();
builder.Services.AddSingleton(tokenSetting);
builder.Services.AddScoped<Identity.Application.Interfaces.IRepositories.IConfigurationProvider, Identity.Infrastructure.Configuration.ConfigurationProvider>();
builder.Services.AddAuth(tokenSetting.Secret);

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) //this line is commented because we need to show the swagger in production server
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
db.Database.Migrate();

app.UseHttpsRedirection();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuth();

app.MapControllers();

app.Run();
