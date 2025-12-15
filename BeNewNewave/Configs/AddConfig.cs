using BeNewNewave.Data;
using BeNewNewave.Sevices;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Sevices;
using BeNewNewave.Interface.Services;
namespace BeNewNewave.Configs
{
    public static class AddConfig
    {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowWebHost",
                poli => poli
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            return services;
        }

        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["appsetting:token"]!)
                        ),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["appsetting:issuer"],
                    ValidateAudience = false,
                    ValidAudience = configuration["appsetting:audience"],

                    ValidateLifetime = true

                };
            });
            return services;
        }


        public static IServiceCollection DbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:mydb"]);
            });
            services.AddDbContext<ImageDBContext>(options => {
                options.UseSqlServer(configuration["ConnectionStrings:myImageDB"]);
            });
            return services;
        }

        public static IServiceCollection AddScopedConfig(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserRepository, UserRepo>();

            services.AddScoped<IAuthorServices, AuthorServices>();
            services.AddScoped<IAuthorRepository, AuthorRepo>();

            services.AddScoped<IBookRepository, BookRepo>();
            services.AddScoped<IBookServices, BookServices>();

            services.AddScoped<IImageServices, ImageServices>();
            services.AddScoped<IImageRepository, ImageRepo>();

            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<ICartRepository, CartRepo>();

            services.AddScoped<ICartBookRepository, CartBookRepo>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            return services;
        }



    }
}
