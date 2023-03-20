using ITBanking.Core.Application;
using ITBanking.Infrastructure.Identity;
using ITBanking.Infrastructure.Identity.Entities;
using ITBanking.Infrastructure.Identity.Seeds;
using ITBanking.Infrastructure.Persistence;
using ITBanking.Infrastructure.Shared;
using ITBanking.Presentation.Middleware;
using ITBanking.Presentation.WebApp.Middleware;
using ITBanking.Web.Middleware;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddScoped<SaveAuthorize>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidateSessions, ValidateSessions>();


var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
  var services = scope.ServiceProvider;

  try {

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DefaultRoles.SeedAsync(userManager, roleManager);
    await DefaultBasicUser.SeedAsync(userManager, roleManager);
    await DefaultAdminUser.SeedAsync(userManager, roleManager);
    await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);

  } catch {
    throw;
  }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
