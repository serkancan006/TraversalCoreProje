using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Container;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DTOLayer.DTOs.AnnouncementDTOs;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TraversalCoreProje.CQRS.Handlers.DestinationHandlers;
using TraversalCoreProje.Models;

namespace TraversalCoreProje
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
            services.AddScoped<GetAllDestinationQueryHandler>();
            services.AddScoped<GetDestinationByIDQueryHandler>();
            services.AddScoped<CreateDestinationCommandHandler>();
            services.AddScoped<RemoveDestinationCommandHandler>();
            services.AddScoped<UpdateDestinationCommandHandler>();

            //typeof bir JavaScript operatörüdür. Bu operatör, belirli bir deðerin veri türünü döndürmek için kullanýlýr. JavaScript'te typeof operatörü genellikle deðiþkenlerin veya deðerlerin veri türünü kontrol etmek veya karþýlaþtýrmak için kullanýlýr.
            //typeof operatörü, bir argüman olarak alýnan deðerin veri türünü bir dize olarak döndürür.

            //Hocam tahminim bizzden assambly bir kod isityor her ne kadar programlama dilleri deðiþkenlik göstersede arka planda hepsi assamble koduna dönüþtürülüp( 0 ve 1 lere yani ) bize response olarak tekrar 0 ve 1 den bildiðimiz þekle döndürür. startup ise ilgili tüm configure'leri vs vs program cs ile asssambly koduna dönüþtürüldüðü yerdir bu yüzden parametre olarak startup verdik :)

            //typeof(startup) dediðimiz kýsýmda startup dosyasýyla ayný namespacede olan dosyalar için verdiðimiz dependency injection çalýþsýn demek istiyoruz. .Net6 için typeof(Program) diyoruz.
            services.AddMediatR(typeof(Startup));

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            services.AddLogging(x =>
            {
                x.ClearProviders();
                x.SetMinimumLevel(LogLevel.Debug);
                x.AddDebug();
            });

            services.AddDbContext<Context>();
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<Context>();

            services.AddHttpClient();

            services.ContainerDependencies();

            services.AddAutoMapper(typeof(Startup));

            services.CustomerValidator();

            services.AddControllersWithViews().AddFluentValidation();
            //Proje seviyesinde Authonticate iþlemleri için
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log1.txt");

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

            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
			// UseAuthentication kýsmýnýn UseAuthorization önce olmasý gerek. olmaz ise Sisteme üye olmadan veya giriþ yapmadan yetkilendirme iþlemi uygulayacak ve bunun sonucunda hata alacaðýz.
			app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
