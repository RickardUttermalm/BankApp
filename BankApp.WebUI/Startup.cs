using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BankApp.Persistence;
using MediatR;
using MediatR.Pipeline;
using BankApp.Application.Bank.Queries;
using System.Reflection;
using BankApp.Application.Interfaces;
using AutoMapper;
using BankApp.Application.Infrastructure.AutoMapper;
using FluentValidation.AspNetCore;
using BankApp.Application.Customers.Commands.CreateCustomer;
using System.Security.Claims;

namespace BankApp.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                             .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>());

            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            services.AddDbContext<BankAppDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IBankAppDataContext), typeof(BankAppDataContext));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<BankAppDataContext>()
                    .AddDefaultTokenProviders();

            //services.AddMediatR(typeof(GetBankInfoQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetBankInfoQuery).GetTypeInfo().Assembly);

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Adminonly", policy => policy.RequireClaim("Admin"));
                options.AddPolicy("Cashieronly", policy => policy.RequireClaim("Cashier"));
                options.AddPolicy("Regularonly", policy => policy.RequireClaim("Regular"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
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

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            //var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await UserManager.FindByNameAsync("Admin") == null)
            {
                var userIdentity = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin@admin.com"
                };

                var result = await UserManager.CreateAsync(userIdentity, "Admin123");

                if (result.Succeeded)
                {
                    var resultRole = await UserManager.AddClaimAsync(userIdentity, new Claim("Admin", "true"));
                }

            }
        }
    }
}
