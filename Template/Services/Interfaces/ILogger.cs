﻿using System;
using System.Collections.Generic;

namespace Template.Services.Interfaces
{
    public interface ILogger
    {
        void LogError(Exception exception, Dictionary<string, string> properties = null);

        void LogEvent(string name, string detail = "", Dictionary<string, string> properties = null);

        void LogEvent(string name, Dictionary<string, string> properties = null);

        void LogWarning(string name, Dictionary<string, string> properties = null);
    }
}
