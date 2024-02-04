using Dairy.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, app.Environment);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
  services.AddDbContext<ApplicationDbContext>(options =>
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

  services.AddScoped<IUserService, UserService>();
  services.AddEndpointsApiExplorer();
  services.AddControllers();
  services.AddSwaggerGen(c =>
  {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  });
}

void Configure(WebApplication app, IWebHostEnvironment env)
{
  if (env.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
  }

  app.UseHttpsRedirection();
  app.UseRouting();
  app.UseAuthorization();

  app.UseExceptionHandler(c => c.Run(async context =>
  {
    var exception = context.Features
          .Get<IExceptionHandlerPathFeature>()
          ?.Error;

    var response = new { Error = exception?.Message };
    await context.Response.WriteAsJsonAsync(response);
  }));

  app.MapControllers();
}
