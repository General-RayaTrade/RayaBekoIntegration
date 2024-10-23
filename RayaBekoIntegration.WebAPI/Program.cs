using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.EF;
using RayaBekoIntegration.EF.IRepositories;
using RayaBekoIntegration.EF.Repositories;
using RayaBekoIntegration.Service.Services;
using RayaBekoIntegration.Services;
using System.Text;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);
builder.Services.AddRateLimiter(options =>
{
    // Define a fixed window rate limiter (for example, 1 request per minute)
    options.AddFixedWindowLimiter("Fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 10;  // Maximum number of allowed requests
        limiterOptions.Window = TimeSpan.FromMinutes(1); // Time window
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; // Process oldest requests first
        limiterOptions.QueueLimit = 0; // **Set QueueLimit to 0 to reject immediately once the limit is hit**
    });

    // Handle rejected requests globally
    options.OnRejected = async (context, token) =>
    {
        // Return a custom response when the request is rate-limited
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "application/json";

        var errorResponse = new
        {
            message = "Too many requests. Please try again later.",
            retryAfter = "1 minute." // Retry time in seconds
        };

        await context.HttpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken: token);
    };
});


builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IDistrictService, DistrictService>();
//builder.Services.AddTransient<IResponseService<T>, ResponseService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// In Startup.cs
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "RAYA BEKO API", Version = "v1.0" });
});
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
        ValidIssuer = "YourIssuer",  // Replace with your issuer
        ValidAudience = "YourAudience",  // Replace with your audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a_secure_key_for_jwt_tokens_123456"))  // Replace with your key
    };
    // Enable detailed error reporting for debugging
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Use the rate limiter middleware
app.UseRateLimiter();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
