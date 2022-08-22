using BlazorDownloadFile;
using Blazored.LocalStorage;
using Blazored.Modal;
using ElectronNET.API;
using ElectronNET.API.Entities;
using KH3Randomizer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KH3Randomizer
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
            services.AddSingleton<RandomizerService>();
            services.AddSingleton<HintService>();
            services.AddBlazoredModal();
            services.AddBlazoredLocalStorage();
            services.AddBlazorDownloadFile(ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
                app.UseBrowserLink();
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

            if (HybridSupport.IsElectronActive)
            {
                CreateWindow();
            }
        }

        private static async void CreateWindow()
        {
            var options = new BrowserWindowOptions
            {
                AutoHideMenuBar = true,
                DarkTheme = true,
                FullscreenWindowTitle = true,
                Title = "Kingdom Hearts 3 Randomizer",
                Icon = "/wwwroot/icon.ico",
                Width = 1920,
                Height = 1080
            };

            var window = await Electron.WindowManager.CreateWindowAsync(options);
            window.Maximize();
            window.OnClosed += () => {
                Electron.App.Quit();
            };
        }
    }
}
