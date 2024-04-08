using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WarrantyRepository.IRepositories;
using WarrantyManagement.Entities;
using WarrantyManagement.Repositories;
using WarrantyRepository.Repositories;

namespace WarrantyManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
            // Singleton - Scoped - Trans
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
            builder.Services.AddScoped<IDeviceHistoryRepository, DeviceHistoryRepository>();
            builder.Services.AddScoped<IWarrantyRepository, Repositories.WarrantyRepository>();
            builder.Services.AddScoped<IWarrantyHistoryRepository, WarrantyHistoryRepository>();
            builder.Services.AddScoped<IWarrantyDeviceRepository, WarrantyDeviceRepository>();
            builder.Services.AddScoped<IWarrantyDeviceHistoryRepository, WarrantyDeviceHistoryRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

            // UnitOfWork

            builder.Services.AddDbContext<WarrantyManagementDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("WarrantyManagement"));
                //options.UseSqlServer(b => b.MigrationsAssembly("WarrantyManagement"));
            });

            builder.Services.AddCors(options => options.AddDefaultPolicy(
                policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WarrantyManagementDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //});

            builder.Services.AddAuthorization(async options =>
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());

                var serviceProvider = builder.Services.BuildServiceProvider();
                var permissionRepository = serviceProvider.GetRequiredService<IPermissionRepository>();

                var permissions = permissionRepository.GetAll().Result;

                foreach (var permission in permissions)
                {
                    options.AddPolicy(permission.Name, policy => policy.RequireClaim("Permission", permission.Name));
                }
                //options.AddPolicy("VIEW_ROLE", policy => policy.RequireClaim("Permission", "VIEW_ROLE"));
            });

            var app = builder.Build();

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

            app.UseCors();

            app.Run();
        }
    }
}