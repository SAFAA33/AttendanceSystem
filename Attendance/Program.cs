using Attendance.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Attendance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Seeding some initial data
            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<DataContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var saraUser = new IdentityUser("sara") { Email = "sara@test.com" };
                userManager.CreateAsync(saraUser, "password").GetAwaiter().GetResult();
                userManager.AddClaimAsync(saraUser, new Claim(
                    AttendanceConstants.Claims.Role,
                    AttendanceConstants.Roles.Admin)
                    )
                    .GetAwaiter().GetResult();



                ctx.Add(new Course
                {
                    Id = 1,
                    Name = "Java",
                    Description = "Java beginner",
                    UserId = saraUser.Id,
                    Sessions = new List<Session>()
                    {
                        new Session
                        {
                            Id = 1,
                            Name = "Session 1",
                            Duration = 1
                        },
                        new Session
                        {
                            Id = 2,
                            Name = "Session 2",
                            Duration = 1
                        },
                        new Session
                        {
                            Id = 3,
                            Name = "Session 3",
                            Duration = 1
                        },
                    }
                });
                ctx.SaveChangesAsync().GetAwaiter().GetResult();
                ctx.Add(new Course
                {
                    Id = 2,
                    Name = "System Analysis",
                    Description = "System Analysis beginner",
                    UserId = saraUser.Id,
                    Sessions = new List<Session>()
                    {
                        new Session
                        {
                            Id = 4,
                            Name = "Session 1",
                            Duration = 1
                        },
                        new Session
                        {
                            Id = 5,
                            Name = "Session 2",
                            Duration = 1
                        }
                    }
                });

                ctx.SaveChangesAsync().GetAwaiter().GetResult();
                
                



                //List<AttendanceModel> attendances = new List<AttendanceModel>()
                //{
                //    new AttendanceModel
                //    {

                //        SessionId = 1,
                //        StudentId = 1,
                //    },
                //    new AttendanceModel
                //    {

                //        SessionId = 1,
                //        StudentId = 2,
                //    }
                //};
                //ctx.AddRange(attendances);
                //ctx.SaveChangesAsync().GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
