using System;
using System.Threading.Tasks;
using Template.Services.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Template.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(
            IBaseServices baseServices)
            : base(baseServices)
        {
            Title = "Prism MVVM";
        }

        public AsyncCommand NavigateToSettingsCommand => new AsyncCommand(NavigateToSettings);

        private Task NavigateToSettings()
        {
            throw new NotImplementedException();

            // return NavigationService.NavigateAsync("");
        }
    }
}