using HorseApp.Api.Services;
using HorseApp.Application.Common.Interfaces;
using HorseApp.Application.Mapping;
using HorseApp.Application.Users.Commands;
using HorseApp.Infrastructure.Auth;
using HorseApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Supabase;

namespace HorseApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Swagger + Bearer auth knapp
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HorseApp.Api",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Skriv: Bearer {din JWT token}"
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

            // ===== Auth (Supabase JWT) =====
            var supabaseProjectUrl = builder.Configuration["Supabase:ProjectUrl"];
            var supabaseAudience = builder.Configuration["Supabase:JwtAudience"] ?? "authenticated";
            var supabaseKey = builder.Configuration["Supabase:Key"];

            if (string.IsNullOrWhiteSpace(supabaseProjectUrl))
            {
                throw new InvalidOperationException("Supabase:ProjectUrl saknas i appsettings.json");
            }

            builder.Services.AddScoped<Supabase.Client>(_ =>
            new Supabase.Client(
            supabaseProjectUrl ?? "https://fake.url/",
            supabaseKey ?? "fake-key",
            new SupabaseOptions { AutoRefreshToken = false }
             )
             );

            var supabaseIssuer = $"{supabaseProjectUrl}/auth/v1";
            var metadataAddress = $"{supabaseIssuer}/.well-known/openid-configuration";

            builder.Services
                .AddAuthentication("SupabaseAuth")
                .AddScheme<SupabaseAuthenticationOptions, SupabaseAuthenticationHandler>(
                "SupabaseAuth", _ => { });


            builder.Services.AddAuthorization();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            // ===== Auth (Supabase JWT) =====

            // ===== Db =====
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<AppDbContext>());
            // ===== Db =====

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
            });

            builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);

            var app = builder.Build();

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
        }
    }
}
