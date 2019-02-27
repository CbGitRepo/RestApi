using AngularNetCoreSample.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AngularNotCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _Config = configuration;
        }

         IConfiguration _Config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_Config);//CB 1-11-2018
            //Data services db ctx
            services.AddDbContext<ClientDBContext>(cfg =>
            {
                cfg.UseSqlServer(_Config.GetConnectionString("myCnxString")); // we need to add where the c
            }, ServiceLifetime.Scoped
             );
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddTransient<ClientDbInitializer>();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            //register CORS
            services.AddCors(options => options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerDocument(document => document.DocumentName = "ClientDocument");
            //services.AddSwaggerGen(c=> 
            //{
            //    c.SwaggerDoc("doc", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Sample Api", Description = "My .net core api" });
            //    //string pathxml = System.AppDomain.CurrentDomain.BaseDirectory + @"AngularNotCore.xml";
            //    //c.IncludeXmlComments(pathxml);
            //}
            //);
            var appSettingsSection = _Config.GetSection("Jwt");
            services.Configure<Jwt>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<Jwt>();
            var key = Encoding.ASCII.GetBytes(appSettings.signingKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env,ClientDbInitializer initDB)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();


            //////////////////////////This needs to go before MVC ///////////
            app.UseSwagger();
            app.UseSwaggerUi3();///URl : /swagger
            ////////////////////////////////////////////////////////////////



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
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
            //initDB.Seed().Wait();

        }
    }
}
