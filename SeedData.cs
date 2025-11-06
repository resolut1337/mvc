using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using OnlineLearningMVC.Data;
using OnlineLearningMVC.Models;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var context = services.GetRequiredService<ApplicationDbContext>();

        string[] roles = new[] { "Admin", "Teacher", "Student" };
        foreach (var r in roles)
            if (!await roleManager.RoleExistsAsync(r))
                await roleManager.CreateAsync(new IdentityRole(r));

        var adminEmail = "admin@local.dev";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new ApplicationUser { UserName = "admin", Email = adminEmail, DisplayName = "Administrator" };
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Programming" },
                new Category { Name = "Mathematics" },
                new Category { Name = "Design" }
            );
            await context.SaveChangesAsync();
        }

        if (!context.Courses.Any())
        {
            var cat = context.Categories.First();
            context.Courses.AddRange(
                new Course { Title = "Intro to C#", ShortDescription = "Basics of C#", Price = 0, CategoryId = cat.Id },
                new Course { Title = "ASP.NET MVC", ShortDescription = "Build web apps", Price = 19.99m, CategoryId = cat.Id }
            );
            await context.SaveChangesAsync();
        }
    }
}
