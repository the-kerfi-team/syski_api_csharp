using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using csharp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using Microsoft.AspNetCore.HttpOverrides;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography.X509Certificates;
using csharp.Services;
using Microsoft.AspNetCore.Authorization;

namespace csharp
{
    public class Startup
    {

        private readonly IHostingEnvironment env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Make all the urls lowercase as this is good web practice
            services.AddRouting(options => options.LowercaseUrls = true);

            if (env.IsProduction())
            {
                // Use certificate passed in with docker for the key encryption
                services.AddDataProtection()
                        .SetApplicationName("api.syski.co.uk")
                        .ProtectKeysWithCertificate(new X509Certificate2("/https/ssl.pfx", "password"))
                        .PersistKeysToFileSystem(new DirectoryInfo("etc/keys"));
            }             

            // Load the connection string from the settings file and use it for storing data
            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))    
                    );

            // Add Identity to the application
            services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            // Java Web Tokens Authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                    })
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
                    {
                        cfg.RequireHttpsMetadata = true;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                            ValidateIssuer = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidateAudience = true,
                            ValidAudiences = new string[] { Configuration["Jwt:Audience"] },
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                            //ClockSkew = TimeSpan.FromMinutes(5)
                        };
                    })
                    .AddJwtBearer("refresh", cfg =>
                    {
                        cfg.RequireHttpsMetadata = true;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                            ValidateIssuer = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidateAudience = true,
                            ValidAudiences = new string[] { Configuration["Jwt:Audience"] },
                            ValidateLifetime = false,
                            ClockSkew = TimeSpan.Zero
                            //ClockSkew = TimeSpan.FromMinutes(5)
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("refreshtoken", new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddAuthenticationSchemes("refresh").Build());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<UserTokenManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Using Nginx As Reverse Proxy for Docker
            if (env.IsProduction())
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            // Make sure HTTPS is used for the API
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Ensure the database has been created
            dbContext.Database.EnsureCreated();
        }
    }
}
