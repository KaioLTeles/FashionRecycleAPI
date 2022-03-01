using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.Application.Utils
{
    public static class Logger
    {
        static Logger()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.File(@"logs\Info.log", rollingInterval: RollingInterval.Day))
            .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo.File(@"logs\Debug.log", rollingInterval: RollingInterval.Day))
            .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo.File(@"logs\Warning.log", rollingInterval: RollingInterval.Day))
            .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo.File(@"logs\Error.log", rollingInterval: RollingInterval.Day))
            .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal).WriteTo.File(@"logs\Fatal.log", rollingInterval: RollingInterval.Day))
            .WriteTo.File("logs/FashionRecycleAPI-Log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }

        public static void WriteLog(string msg)
        {
            Log.Information(msg);
        }

        public static void WriteError(string msg, Exception ex)
        {
            Log.Error(msg + " - " + ex.Message + " - " + ex.StackTrace, ex);
        }
    }
}
