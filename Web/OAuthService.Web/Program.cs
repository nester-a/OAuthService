using OAuthService.Data.DependencyInjection;
using OAuthService.Infrastructure.DependencyInjection;
using OAuthService.Web.Authentication;
using OAuthService.Web.Filters;
using OAuthService.Web.Middlewares;
using OAuthService.Web.NamingPolicies;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddRazorPages();
services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy());

//������ ���, ����� ��� ���� ���� � ��������� �����
services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

//������������� ����� �������������� Basic
services.AddAuthentication(options => options.DefaultScheme = BasicDefaults.AuthenticationScheme)
        .AddScheme<BasicOptions, BasicHandler>(BasicDefaults.AuthenticationScheme, options => { });

//���������� �������
services.AddScoped<ContainsRequiredParametersActionFilter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddTestOAuthStorages();
services.AddOAuthServiceInfrastructure();



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

app.MapControllers();
app.MapRazorPages();

app.Run();
