using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Vite.AspNetCore;
using WeChooz.TechAssessment.Application;
using WeChooz.TechAssessment.Persistence;
using WeChooz.TechAssessment.Persistence.Seeds;
using WeChooz.TechAssessment.Shared;
using WeChooz.TechAssessment.Web;
using WeChooz.TechAssessment.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

var sqlServerConnectionString = builder.Configuration.GetConnectionString("Course") ?? throw new InvalidOperationException("Connection string 'Course' not found.");
var redisConnectionString = builder.Configuration.GetConnectionString("cache") ?? throw new InvalidOperationException("Connection string 'cache' not found.");

builder.Services.AddControllersWithViews();
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/{1}/_Views/{0}" + RazorViewEngine.ViewExtension);
});
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/authentication"; // for browser
        options.Cookie.Name = "AspireAuthCookie";
        options.Events.OnRedirectToLogin = context =>
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            }
            context.Response.Redirect(context.RedirectUri);
            return Task.CompletedTask;
        };
    });
builder.Services.AddAuthorization(options =>
{
    var defaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("Formation", policy => policy.Combine(defaultPolicy).RequireRole("formation"));
    options.AddPolicy("Sales", policy => policy.Combine(defaultPolicy).RequireRole("sales"));
});


builder.Services.AddViteServices(options =>
{
    options.Server.Port = 5180;
    options.Server.Https = true;
    options.Server.AutoRun = true;
    options.Server.UseReactRefresh = true;
    options.Manifest = "vite.manifest.json";
});

builder.Services.AddApplicationLayer();
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();   
app.UseCors(WeChooz.TechAssessment.Web.ServiceExtensions.AllowSpecificOrigins);
app.UseAntiforgery();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapStaticAssets();
app.MapControllers();
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseWebSockets();
    app.UseViteDevelopmentServer(true);
}


app.MapControllerRoute(
        name: "fallback_admin",
        pattern: "admin/{*subpath}",
        constraints: new { subpath = @"^(?!swagger).*$" },
        defaults: new { controller = "Admin", action = "Handle" }
);
app.MapControllerRoute(
        name: "fallback_admin_root",
        pattern: "admin/",
        defaults: new { controller = "Admin", action = "Handle" }
    );
app.MapControllerRoute(
        name: "fallback_home",
        pattern: "{*subpath}",
        constraints: new { subpath = @"^(?!swagger).*$" },
        defaults: new { controller = "Home", action = "Handle" }
);
app.MapControllerRoute(
        name: "fallback_home_root",
        pattern: "",
        defaults: new { controller = "Home", action = "Handle" }
    );

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
context.CreateDatabase();

app.Run();
