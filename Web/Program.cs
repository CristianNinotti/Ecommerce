using Application.Interfaces;
using Application.Models.Helpers;
using Application.Services;
using Domain.Interfaces;
using Infraestructure.Context;
using Infraestructure.Data;
using Infrastructure.Data;
using Infrastructure.ThirdServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuraci�n de DbContext
builder.Services.AddDbContext<EcommerceDbContext>(dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:DBConnectionString"]));

// Configuraci�n de autenticaci�n
builder.Services.Configure<AuthenticationServiceOptions>(builder.Configuration.GetSection("AuthenticationServiceOptions"));

// Configuraci�n de Swagger
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("EcommerceApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Please, paste the token to login for use all endpoints."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "EcommerceApiBearerAuth"
                        }
                    },
                    new List<string>()
                }
            });
});

// Configuraci�n de autenticaci�n con JWT
builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["AuthenticationServiceOptions:Issuer"],
                    ValidAudience = builder.Configuration["AuthenticationServiceOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationServiceOptions:SecretForKey"]!))
                };
            }
        );

        // Configuraci�n de CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        // Configuraci�n de pol�ticas de autorizaci�n
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("MinoristaOnly", policy => policy.RequireClaim("TypeUser", "Minorista"));
            options.AddPolicy("MayoristaOnly", policy => policy.RequireClaim("TypeUser", "Mayorista"));
            options.AddPolicy("SuperAdminOnly", policy => policy.RequireClaim("TypeUser", "SuperAdmin"));

            options.AddPolicy("MinoristaOrSuperAdmin", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim("TypeUser", "Minorista") ||
                context.User.HasClaim("TypeUser", "SuperAdmin")));

            options.AddPolicy("MayoristaOrSuperAdmin", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim("TypeUser", "Mayorista") ||
                context.User.HasClaim("TypeUser", "SuperAdmin")));

            options.AddPolicy("MinoristaOrMayoristaOrSuperAdmin", policy =>
            policy.RequireAssertion(context =>
                  context.User.HasClaim("TypeUser", "Minorista") ||
                  context.User.HasClaim("TypeUser", "Mayorista") ||
                  context.User.HasClaim("TypeUser", "SuperAdmin")));
        });


// Configuraci�n de servicios de aplicaci�n e infraestructura
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IMinoristaRepository, MinoristaRepository>();
builder.Services.AddScoped<IMinoristaService, MinoristaService>();
builder.Services.AddScoped<IMayoristaRepository, MayoristaRepository>();
builder.Services.AddScoped<IMayoristaService, MayoristaService>();
builder.Services.AddScoped<ISuperAdminService, SuperAdminService>();
builder.Services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

// Habilitaci�n de CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
