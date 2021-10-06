using System;
using Foundation;
using Template.Services.Interfaces;

namespace Template.iOS.Services
{
    public class NativeSettingsService : INativeSettingsService
    {
        public Uri GetBaseUrl()
        {
            return new Uri(NSBundle.MainBundle.ObjectForInfoDictionary("BaseUrl").ToString());
        }

        public string GetBuildNumber()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }

        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }
    }
}
