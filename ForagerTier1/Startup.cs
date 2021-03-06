using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ForagerTier1.Models;
using Microsoft.AspNetCore.Components.Authorization;
using ForagerTier1.Persistance;
using System.Security.Claims;
using Syncfusion.Blazor;
using ForagerTier1.Shared;

namespace ForagerTier1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<ISocketService, SocketService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddSyncfusionBlazor();
            services.AddScoped<IRefreshService, RefreshService>();

            services.AddAuthorization(options => {
                options.AddPolicy("Admin", a =>
                   a.RequireAuthenticatedUser().RequireClaim("SecurityLevel", "4"));
                options.AddPolicy("Moderator", a =>
                   a.RequireAuthenticatedUser().RequireClaim("SecurityLevel", "4", "3"));
                options.AddPolicy("VR", a =>
                   a.RequireAuthenticatedUser().RequireClaim("SecurityLevel", "4","3","2"));
                options.AddPolicy("VA", a =>
                   a.RequireAuthenticatedUser().RequireClaim("SecurityLevel", "4", "3", "2","1"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
