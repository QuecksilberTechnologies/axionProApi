using ems.api.Common;
using ems.api.Middlewares;
using ems.application;
using ems.infrastructure;
using ems.persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console() // Console sink
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // File sink (optional)
                                                                        // .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs" }) // Database sink (if needed)
    .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.WebHost.UseIISIntegration(); // UseIISIntegration ko builder ke saath use karna

    // JWT settings ko configuration se read karna
    var jwtSettings = builder.Configuration.GetSection("JWTSettings");
    var secretKey = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
    };
});

    // Serilog ko builder ke saath integrate karna
    builder.Host.UseSerilog();  // IMPORTANT: Serilog ko host mein add karna

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp", policy =>
        {


            policy.WithOrigins("http://localhost:3001", "http://localhost:3000", "http://quecksilber.in", "http://localhost:4200") // React app URL
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "EMS API", Version = "v1" });
        c.SchemaFilter<NullSchemaFilter>(); // ✅ This line is important
    });

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddHttpContextAccessor();

    var app = builder.Build();
    app.UseCors("AllowReactApp");
    app.UseAuthentication();
    app.UseAuthorization();

    // Configure the HTTP request pipeline.
    // Swagger har environment me enable
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EMS API V1");
        c.RoutePrefix = string.Empty; // Swagger root par open ho
    });


    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}