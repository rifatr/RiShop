using API.Helpers;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using API.Middleware;
using Microsoft.OpenApi.Models;
using API.Extensions;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var config = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
    return ConnectionMultiplexer.Connect(config);
});

builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen(c => // need to explore more Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RiShop API", Version = "V1" });
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("https://localhost:4200");
    });
});

var app = builder.Build();

// Auto migration of DB and data seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured while db migration.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => // need to explore more
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RiShop API v1");
    });
}

// And in your middleware configuration
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("CorsPolicy");

app.UseStaticFiles();
app.MapControllers();


app.Run();
