﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Doge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
                       .Enrich.FromLogContext()
                       .WriteTo.File(new CompactJsonFormatter(),
                       "logs\\doge.log",
                       rollingInterval: RollingInterval.Month)
           .CreateLogger();

            CreateWebHostBuilder(args).Build().Run();
        }

        //usage for logs: Log.ForContext<TimedHostedService>().Warning("message");


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseApplicationInsights();
    }
}
