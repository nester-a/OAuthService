using OAuthService.Web.DependencyInjection;
using OAuthService.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddRazorPages();
services.AddControllers();
services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddStorages();
services.AddMiddlewareServices();
services.AddTokenEndpointServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<OAuthErrorHandleMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<ClientAuthenticationMiddleware>();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
