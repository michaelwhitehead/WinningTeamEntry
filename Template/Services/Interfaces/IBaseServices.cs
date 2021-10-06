using Prism.Navigation;

namespace Template.Services.Interfaces
{
    public interface IBaseServices
    {
        INavigationService NavigationService { get; }

        ILogger LoggerService { get; }
    }
}
