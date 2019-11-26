using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicAspNetCoreWebApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BasicAspNetCoreWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) //(parametre -> appsetting) Connectionı tanıtıyorum
        {
            Configuration = configuration;
        }

        // Services (runtime)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataConnection"));
                options.EnableSensitiveDataLogging(true);
            });

            services.AddMvc();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Token(JWT) Auth
            string securityKey = "this_is_our_long_security_key_for_token_26_11_2019$smesk.in";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //what to validate
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,

                        //setup validate date
                        ValidIssuer = "smesk.in",
                        ValidAudience="readers",
                        IssuerSigningKey = symmetricSecurityKey
                    };
                });

            //For CORS
            //services.AddCors();
            //services.AddCors(o => o.AddPolicy("NamePolicy", builder => {
            //    builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
            //}));  //Controller da [EnableCors("NamePolicy")]
        }

        // Configure the HTTP request pipeline (runtime)
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataContext dataContext)
        {
            //SeedDatabase metodunu ekle!
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed(dataContext);
                dataContext.Database.Migrate();
            }
            else
            {
                app.UseHsts();
            }

            //Hata oluştuğunda hata kodlarını göster!
            app.UseDeveloperExceptionPage();

            //Server tarafından gönderilen hata kodlarını göster!
            app.UseStatusCodePages();

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseMvcWithDefaultRoute();

            //CORS
            //app.UseCors(x => x.WithOrigins("http://localhost:5000"));

            //Veya belirli Headerlara izin verirken:
            //app.UseCors(bldr => bldr
            //.WithOrigins("http://localhost:5000")
            //.WithMethods("GET", "POST")
            //.AllowAnyHeader()
            //);           
        }
    }
}
