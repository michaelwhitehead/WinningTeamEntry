using Prism;
using Prism.Ioc;

namespace Template.iOS
{
    public class PlatformInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Services(containerRegistry);
        }

        private void Services(IContainerRegistry containerRegistry)
        {
        }
    }
}
