using CVScreeningAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CVScreeningAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create roles
            string[] roles = { "Admin", "HR", "Manager" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create default admin user
            var adminEmail = "admin@company.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Create default HR user
            var hrEmail = "hr@company.com";
            var hrUser = await userManager.FindByEmailAsync(hrEmail);
            if (hrUser == null)
            {
                hrUser = new IdentityUser
                {
                    UserName = hrEmail,
                    Email = hrEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(hrUser, "SecureHR2024!");
                await userManager.AddToRoleAsync(hrUser, "HR");
            }

            // Seed sample data if no candidates exist
            if (!context.Candidates.Any())
            {
                var candidates = new List<Candidate>
                {
                    new Candidate
                    {
                        Name = "John Smith",
                        Email = "john.smith@email.com",
                        Phone = "+1-555-0123",
                        Experience = 5,
                        Skills = "JavaScript,React,Node.js,TypeScript",
                        CvFileName = "john_smith_cv.pdf",
                        Status = "reviewed",
                        Rating = 4
                    },
                    new Candidate
                    {
                        Name = "Sarah Johnson",
                        Email = "sarah.johnson@email.com",
                        Phone = "+1-555-0124",
                        Experience = 3,
                        Skills = "Python,Django,PostgreSQL,AWS",
                        CvFileName = "sarah_johnson_cv.pdf",
                        Status = "pending"
                    },
                    new Candidate
                    {
                        Name = "Michael Chen",
                        Email = "michael.chen@email.com",
                        Phone = "+1-555-0125",
                        Experience = 7,
                        Skills = "Java,Spring Boot,Microservices,Docker",
                        CvFileName = "michael_chen_cv.pdf",
                        Status = "interviewed",
                        Rating = 5,
                        InterviewDate = DateTime.UtcNow.AddDays(7)
                    }
                };

                context.Candidates.AddRange(candidates);
                await context.SaveChangesAsync();
            }

            // Seed skills
            if (!context.Skills.Any())
            {
                var skills = new List<Skill>
                {
                    new Skill { Name = "JavaScript", Description = "JavaScript programming language" },
                    new Skill { Name = "React", Description = "React JavaScript library" },
                    new Skill { Name = "Node.js", Description = "Node.js runtime environment" },
                    new Skill { Name = "Python", Description = "Python programming language" },
                    new Skill { Name = "Java", Description = "Java programming language" },
                    new Skill { Name = "C#", Description = "C# programming language" },
                    new Skill { Name = "SQL", Description = "SQL database language" },
                    new Skill { Name = "Docker", Description = "Docker containerization" },
                    new Skill { Name = "AWS", Description = "Amazon Web Services" },
                    new Skill { Name = "Azure", Description = "Microsoft Azure" }
                };

                context.Skills.AddRange(skills);
                await context.SaveChangesAsync();
            }
        }
    }
}
