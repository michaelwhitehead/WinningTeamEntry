using Prism.Navigation;
using Template.Services.Interfaces;

namespace Template.Services
{
    public class BaseServices : IBaseServices
    {
        public BaseServices(
            INavigationService navigationService,
            ILogger logger)
        {
            NavigationService = navigationService;
            LoggerService = logger;
        }

        public INavigationService NavigationService { get; }

        public ILogger LoggerService { get; }
    }
}
