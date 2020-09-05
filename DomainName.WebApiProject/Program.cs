using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// microsoft
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
// user defined
using Data.AuthenticationTypes;




namespace AuthenticationHandler_DotNetCore_Nirjhar_Choudhury_2019_12_24
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<AppToken> ApplicationInstanceTokens = new List<AppToken>();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
