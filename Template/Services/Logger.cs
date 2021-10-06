using System;
using System.Collections.Generic;
using Template.Services.Interfaces;

namespace Template.Services
{
    public class Logger : ILogger
    {
        public void LogError(Exception exception = null, Dictionary<string, string> properties = null)
        {
            Console.WriteLine(exception.Message, properties);

            // Crashes.TrackError(exception, properties);
        }

        public void LogEvent(string name, string detail, Dictionary<string, string> properties = null)
        {
            Console.WriteLine($"{name} : {detail}", properties);

            // Analytics.TrackEvent($"{name} : {detail}", properties);
        }

        public void LogEvent(string name, Dictionary<string, string> properties = null)
        {
            Console.WriteLine($"Log: {name}", properties);

            // Analytics.TrackEvent($"Log: {name}", properties);
        }

        public void LogWarning(string name, Dictionary<string, string> properties = null)
        {
            Console.WriteLine($"Warning! {name}", properties);

            // Analytics.TrackEvent($"Warning! {name}", properties);
        }
    }
}
