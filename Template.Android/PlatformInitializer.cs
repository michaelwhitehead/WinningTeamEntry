using Prism;
using Prism.Ioc;
using Template.Droid.Services;
using Template.Services.Interfaces;

namespace Template.Droid
{
    public class PlatformInitializer : IPlatformInitializer
    {

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Services(containerRegistry);
        }

        private void Services(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<INativeSettingsService, NativeSettingsService>();
        }
    }
}