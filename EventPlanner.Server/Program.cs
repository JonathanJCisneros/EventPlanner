using Microsoft.Net.Http.Headers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string CORS_POLICY = "debugCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: CORS_POLICY,
        policy =>
        {
            policy.WithOrigins("https://localhost:7134", "https://localhost:59931")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
    );
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(CORS_POLICY);
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
