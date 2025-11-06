using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineLearningMVC.Data;
using OnlineLearningMVC.Models;
using OnlineLearningMVC.Services;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection") 
           ?? "Data Source=onlinelearning.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(conn));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICartService, CookieCartService>();
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed roles, admin and sample data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
