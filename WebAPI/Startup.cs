using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Extentios;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using static JwtHelper;

namespace WebAPI
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
            services.AddControllers();
            //Bana arka planda bir referans olustur dedik. IProductService gorursen o ProductManager. Arka planda newler.
            //Data tutmuyorsan singleton uygula.
            //AutoFac altaki singletonlar� yapar. Onu yap�land�r�cag�z.
            //services.AddSingleton<IProductService,ProductManager>();
            //Yukar�dakini yapmaya gitti dediki abi dedi ben gittim productManager e ama oda IProductDal a bag�ml�.
            //Ondan burada IProductDal gorursen bilki EfProductDal o dedik. Arka planda newlendi yani.
            //services.AddSingleton<IProductDal, EfProductDal>();
            services.AddCors();


            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            //Bir cok modul eklemek istiyoruz.
            //Yar�n obur gun coremodule gibi farkl� moduller olusturursak istedigimiz kadar�n� ekliyebiliriz.
            services.AddDependencyResolvers(new ICoreModule[] { new CoreModule () });
        }
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
            //middle wareler

                app.ConfigureCustomExceptionMiddleware();

                app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());
                app.UseCors(builder => builder.WithOrigins("http://localhost:51617").AllowAnyHeader());

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthentication();

                app.Build();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    } 
