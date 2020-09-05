using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// microsoft
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
// user defined
using DomainName.Domain.Services;
using Data.AuthenticationTypes;
using DomainName.Domain.Repositories;
using DomainName.Infrastructure.Repositories;
// other
using Dapper;
using DomainName.Domain.Extensions;

namespace AuthenticationHandler_DotNetCore_Nirjhar_Choudhury_2019_12_24
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
            //services.AddRepositories();

            services.AddScoped< IUserRepository>(f => 
                    new UserRepository("connectionString"));

            services.AddServices();
            services.AddMappers();

            #region Authentication

            var algorithmSecret = Configuration.GetSection("Tokens:Jwt:Secret").Value;
            // GetBytes => Make the algoSecret into 'bytes'
            var secretAsBytes = Encoding.ASCII.GetBytes(algorithmSecret);

            services.AddAuthentication(authenticationOptions =>
           {
               authenticationOptions.DefaultAuthenticateScheme =
                                JwtBearerDefaults.AuthenticationScheme;
               authenticationOptions.DefaultChallengeScheme =
                                JwtBearerDefaults.AuthenticationScheme;
           })
                // default JwtBearer auth scheme
                .AddJwtBearer(options => // JwtBearerOptions options
                //.AddJwtBearer uses 'AuthenticationBuilder.AddScheme'
               {
                   options.RequireHttpsMetadata = false;// false in 'dev' only
                   options.SaveToken = true;

                   //  TokenValidationParameters
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey =
                                   new SymmetricSecurityKey(secretAsBytes),
                       ValidateIssuer = false,
                       ValidateAudience = false

                       #region TokenValidationParameters Defaults
                       //ValidateLifetime   = true,
                       //ValidateIssuer     = true,
                       //ValidateAudience   = true,
                       //ValidateIssuerSigningKey = false,
                       //RoleClaimType      = n/a; check against 'Roles' present
                       #endregion Defaults
                   };
               })
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options => Configuration.Bind("CookieSettings", options));
            
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(algorithmSecret));
            #endregion Authentication

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // after routing
            app.UseAuthentication();
            app.UseAuthorization();
            // before endpoints

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
