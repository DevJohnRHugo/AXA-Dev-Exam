using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pointwest.Test.Classes.Helper.Converter;
using Pointwest.Test.Classes.Helper.Http;
using Pointwest.Test.Classes.Utilities.Contants;
using Pointwest.Test.DatabaseContext;
using Pointwest.Test.Interfaces.Converter;
using Pointwest.Test.Interfaces.Http;
using Pointwest.Test.Model;
using Pointwest.Test.Model.ViewModels;
using Polly;
using System;

namespace Pointwest.Test
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
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UserDBConnection")));

            services.AddCors(c => c.AddPolicy("TCAPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
           
            services.AddTransient<ApplicationViewModel>();
            services.AddTransient<FileUpload>();
            services.AddTransient<File>();
            services.AddTransient<IFileConverter, FileConverter>();
            services.AddTransient<IJsonConverter, JsonConverter>();
            services.AddSingleton<ApplicationContantUtilities>();
            services.AddTransient<IApiContentProvider, ApiContentProviderApplication>();
            services.AddTransient<IApplicationService, ApplicationAXAService>();

            services.AddHttpClient("default", client => {
                client.BaseAddress = new Uri($"{Configuration["AXA:UriBase:DevExam"]}");
                client.DefaultRequestHeaders.Accept.Clear();
            })
                .AddTransientHttpErrorPolicy(x =>
                x.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(500, retryAttempt)))
              );

            services.AddHttpClient("addHeaderApiKey", client =>
            {
                client.BaseAddress = new Uri($"{Configuration["AXA:UriBase:DevExam"]}");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Configuration["AXA:ApiConfiguration:CustomContentType"], Configuration["AXA:ApiConfiguration:ApiKey"]);
            })
              .AddTransientHttpErrorPolicy(x =>
                x.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(500, retryAttempt)))
              );



            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSingleton<IConfiguration>(Configuration);
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

            app.UseCors("TCAPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }          

            app.UseRouting();           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
