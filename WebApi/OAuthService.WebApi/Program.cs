using OAuthService.Data.DependencyInjection;
using OAuthService.MVC;
using OAuthService.Services.DependencyInjection;
using OAuthService.WebApi.Middlewares;
using OAuthService.Middleware;

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


app.UseMiddleware<OAuthService.Middleware.OAuthErrorHandleMiddleware>();
app.UseMiddleware<ClientAuthenticationMiddleware>();

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
