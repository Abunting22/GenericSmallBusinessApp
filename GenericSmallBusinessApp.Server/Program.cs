
using System.Data;
using System.Text;

using GenericSmallBusinessApp.Server.AuthenticationAndAuthorization;
using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;
using GenericSmallBusinessApp.Server.Repositories;
using GenericSmallBusinessApp.Server.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GenericSmallBusinessApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            // Add services to the container.
            builder.Services.AddScoped<IDbConnection>(provider =>
            {
                string connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
                return new SqlConnection(connectionString);
            });

            builder.Services.AddScoped<IBaseRepository, BaseRepository>();
            builder.Services.AddScoped(typeof(IPrimaryRepository<>), typeof(PrimaryRepository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
            builder.Services.AddScoped<ILoginAuthentication, LoginAuthentication>();
            builder.Services.AddScoped<User>();
            builder.Services.AddScoped<UserDto>();
            builder.Services.AddScoped<LoginDto>();
            builder.Services.AddScoped<IdAuthorizationAttribute>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<Product>();
            builder.Services.AddScoped<ProductDto>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
                x.IncludeErrorDetails = true;
            });
            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
