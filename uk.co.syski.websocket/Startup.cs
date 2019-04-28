using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syski.Data;
using Syski.WebSocket.Services.WebSockets;
using System;

namespace Syski.WebSocket
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Make all the urls lowercase as this is good web practice
            services.AddRouting(options => options.LowercaseUrls = true);

            // Load the connection string from the settings file and use it for storing data
            services.AddDbContext<SyskiDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            // Add Identity to the application
            services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<SyskiDBContext>();

            services.AddHostedService<WebSocketTaskScheduler>();

            services.AddTransient<IWebSocketHandler, WebSocketHandler>();
            services.AddSingleton<Services.WebSockets.WebSocketManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            { 
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Check if behind a reverse proxy if so use Forwarded Headers for the connection information
            try
            {
                if (Convert.ToBoolean(Configuration["ReverseProxy"]))
                {
                    app.UseForwardedHeaders(new ForwardedHeadersOptions
                    {
                        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                    });
                }
            }
            /*catch (FormatException fe)
            {
                // Error parsing config, do nothing assume not behind a reverse proxy
            }
            */
            catch
            {

            }

            // Load WebSocket options from the config file
            var wsOptions = new WebSocketOptions();
            try
            {
                wsOptions.KeepAliveInterval = TimeSpan.FromSeconds(Convert.ToInt32(Configuration["WebSocket:KeepAliveInterval"]));
            }
            catch
            {
                // Default option of keep alive set to 2 minutes
                wsOptions.KeepAliveInterval = TimeSpan.FromSeconds(120);
            }
            try
            {
                wsOptions.ReceiveBufferSize = Convert.ToInt32(Configuration["WebSocket:ReceiveBufferSize"]);
            }
            catch
            {
                // Default option of buffer set to 4096 bytes
                wsOptions.ReceiveBufferSize = 4096;
            }

            app.UseHttpsRedirection();

            app.UseWebSockets(wsOptions);

            app.UseWebSocketMiddleware();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Error: Not a websocket");
            });
        }
    }
}
