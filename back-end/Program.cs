using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NaturguidenServerPrototype.Data;
using NaturguidenServerPrototype.Services;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions {
    EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
});

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        policy => {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()  // Allow all HTTP methods (GET, POST, etc.)
            .AllowAnyHeader()  // Allow all headers
            .AllowCredentials(); // Allow cookies/sessions
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<NatureSpotsService>();
builder.Services.AddScoped<CustomAuthenticationService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.Cookie.Name = "NaturguidenCookie";
    options.Events.OnRedirectToAccessDenied =
    options.Events.OnRedirectToLogin = c => {
        c.Response.StatusCode = StatusCodes.Status401Unauthorized;
        c.Response.ContentType = "application/json";
        return c.Response.WriteAsJsonAsync(new {
            message = "Unauthorized"
        });
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
