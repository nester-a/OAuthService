using OAuthService.Data.DependencyInjection;
using OAuthService.Services.DependencyInjection;
using OAuthService.WebApi;
using OAuthService.Middleware.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOAuthServices();
builder.Services.AddTestOAuthStorages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string pagePath = Path.Combine(Environment.CurrentDirectory, "Pages", "Page.html");

app.UseOAuthService(new UserAuthorizationPageOptions("/authorization", pagePath));

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
