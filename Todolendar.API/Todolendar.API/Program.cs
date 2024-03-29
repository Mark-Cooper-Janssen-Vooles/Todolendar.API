using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Text;
using Todolendar.API.Data;
using Todolendar.API.Repositories;
using Todolendar.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment.EnvironmentName;

// Add services to the container.
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("http://localhost:3000",
                    "https://localhost:3000", "http://todolender-ui-s3-output.s3-website-ap-southeast-2.amazonaws.com")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme()
    {
        Name = "JWT Authentication",
        Description = "Enter a valid JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference()
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {securityScheme, new string[] {} }
    });
});
builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddAWSService<IAmazonSecretsManager>(new AWSOptions
{
    Region = RegionEndpoint.APSoutheast2
});

string connectionString = "";
string jwtKeyProduction = "";

if (env == "Production") {
    connectionString = await GetAWSSecret.GetSecret("prod/TodolendarDb/ConnectionString", "secret");
    jwtKeyProduction = await GetAWSSecret.GetSecret("prod/Todolendar/Jwt", "jwt");
}

builder.Services.AddDbContext<TodolendarDbContext>(options =>
{
    if (env == "Production")
    {
        options.UseMySQL(connectionString);
    }
    else
    {
        options.UseMySQL("server=localhost;user=root;password=password;database=TodolenderDb");
    }
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IScheduledTodoRepository, ScheduledTodoRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IPlanReminderRepository, PlanReminderRepository>();
builder.Services.AddScoped<ITokenHandler, Todolendar.API.Repositories.TokenHandler>();
builder.Services.AddScoped<IHashHandler, Todolendar.API.Repositories.HashHandler>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

string jwtKeyToUse = "";
if (env == "Production")
{
    jwtKeyToUse = jwtKeyProduction;
} else 
{
    jwtKeyToUse = builder.Configuration["Jwt:Key"];
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKeyToUse))
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user", policy =>
    {
        policy.RequireAssertion(context =>
        {
            var claimUserId = context?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value.ToLower().Trim();
            var httpContextUserId = new HttpContextAccessor().HttpContext.Request.RouteValues["userId"]?.ToString().ToLower().Trim();
            if (claimUserId == null || httpContextUserId == null) return false;

            return claimUserId == httpContextUserId;
        });
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

//app.UseHttpsRedirection(); // this is commented out as we don't have an SSL / TLS certificate (costs $$ or reconfig)

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
