using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using csharp.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            // Recreate the database in the development environment
         //   var host = CreateWebHostBuilder(args).Build();

          //  using (var scope = host.Services.CreateScope())
          //  {
           //     var services = scope.ServiceProvider;
           //     var env = services.GetRequiredService<IHostingEnvironment>();
            //    if (env.IsDevelopment())
            //    {
            //        var context = services.GetRequiredService<ApplicationDbContext>();
             //       context.Database.EnsureDeleted();
             //      context.Database.Migrate();
            //    }
            //}

            //host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false);
    }
}
