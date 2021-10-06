using System;

namespace Template.Services.Interfaces
{
    public interface INativeSettingsService
    {
        Uri GetBaseUrl();

        string GetVersion();

        string GetBuildNumber();
    }
}

