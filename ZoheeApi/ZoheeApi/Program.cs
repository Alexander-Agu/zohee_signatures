using FIN.Service.EmailServices;
using Microsoft.EntityFrameworkCore;
using ZoheeApi.Repository;
using ZoheeApi.Service.DocumentService;
using ZoheeApi.Service.TemplateService;
using ZoheeApi.Service.UserService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ZoheeContext>(options =>
    options.UseSqlite("Data Source=zohee.db"));

builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IEmailService, EmailService>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4200",
                "https://yourdomain.com"
            )

            .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")

            .AllowAnyHeader()

            .AllowCredentials();
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("FrontendPolicy");
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
