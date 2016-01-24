
namespace eSIS.Core
{
    //public sealed class NLogger : ITraceWriter
    //{
    //    private static readonly Logger ClassLogger = LogManager.GetCurrentClassLogger();

    //    private static readonly Lazy<Dictionary<TraceLevel, Action<string>>> loggingMap =
    //        new Lazy<Dictionary<TraceLevel, Action<string>>>(() => new Dictionary<TraceLevel, Action<string>>
    //            {
    //                {TraceLevel.Info, ClassLogger.Info},
    //                {TraceLevel.Debug, ClassLogger.Debug},
    //                {TraceLevel.Error, ClassLogger.Error},
    //                {TraceLevel.Fatal, ClassLogger.Fatal},
    //                {TraceLevel.Warn, ClassLogger.Warn}
    //            });

    //    private Dictionary<TraceLevel, Action<string>> Logger
    //    {
    //        get { return loggingMap.Value; }
    //    }

    //    public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
    //    {
    //        if (level != TraceLevel.Off)
    //        {
    //            var record = new TraceRecord(request, category, level);
    //            traceAction(record);
    //            Log(record);
    //        }
    //    }

    //    private static void Log(TraceRecord record)
    //    {
    //        //todo
    //    }
    //}
}
