using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class Audit : IDisposable
    {
        private static EventLog customLog = null;
        const string sourceName = "LogInManager";
        const string logName = "RentACarLogger";

        static Audit()
        {
            try
            {
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, logName);
                }
                customLog = new EventLog(logName, Environment.MachineName, sourceName);
            }
            catch (Exception e)
            {
                customLog = null;
                Console.WriteLine("Error while trying to create log handle. Error: {0}.", e.Message);
            }
        }

        public static void AnnotateEvent(string message)
        {
            customLog = new EventLog(logName, Environment.MachineName, sourceName);
            customLog.WriteEntry(message);
        }

        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }
    }
}
