using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;

namespace Attendance
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddIdentity(services);
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("AttendanceDev"));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }



        private void AddIdentity(IServiceCollection services)
        {
            // services.AddDbContext<ApiIdentityDbContext>(config =>
            // config.UseNpgsql(_config.GetConnectionString("Default")));

            //services.AddDataProtection()
            //    .SetApplicationName("Attendance")
            //    .PersistKeysToDbContext<DataContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                if (_env.IsDevelopment())
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                }
                else
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                }
            })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
                //config.Cookie.Domain = _config["CookieDomain"];
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AttendanceConstants.Policies.Teacher, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(AttendanceConstants.Claims.Role,
                        AttendanceConstants.Roles.Teacher,
                        AttendanceConstants.Roles.Admin)
                );
                options.AddPolicy(AttendanceConstants.Policies.Admin, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(AttendanceConstants.Claims.Role,
                        AttendanceConstants.Roles.Admin)
                );
                options.AddPolicy(AttendanceConstants.Policies.Student, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(AttendanceConstants.Claims.Role,
                        AttendanceConstants.Roles.Student)
                );
            });
        }
    }
}
