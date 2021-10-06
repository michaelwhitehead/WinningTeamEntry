using System;
using System.Threading.Tasks;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using Template.Services.Interfaces;
using PropertyChanged;

namespace Template.ViewModels
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : BindableBase, IApplicationLifecycleAware, IInitializeAsync, INavigatedAware, IDestructible, IConfirmNavigationAsync, IPageLifecycleAware
    {
        public BaseViewModel(IBaseServices baseServices)
        {
            Console.WriteLine($"{GetType().Name}: ctor");
            NavigationService = baseServices.NavigationService;
            Logger = baseServices.LoggerService;
        }

        public string Title { get; set; }

        public bool IsBusy { get; set; }

        public INavigationService NavigationService { get; }

        public ILogger Logger { get; set; }

        public virtual Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            Console.WriteLine($"{GetType().Name}: CanNavigateAsync");
            return Task.FromResult(true);
        }

        public virtual void Destroy()
        {
            Console.WriteLine($"{GetType().Name}: Destroy");
        }

        public virtual void OnAppearing()
        {
            IsBusy = false;
            Console.WriteLine($"{GetType().Name}: OnAppearing");
            Console.WriteLine($"NavigationStack: {NavigationService.GetNavigationUriPath()}");
        }

        public virtual void OnDisappearing()
        {
            Console.WriteLine($"{GetType().Name}: OnDisappearing");
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            Console.WriteLine($"{GetType().Name}: OnNavigatedFrom");
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            Console.WriteLine($"{GetType().Name}: OnNavigatedTo");
        }

        public virtual void OnResume()
        {
            Console.WriteLine($"{GetType().Name}: OnResume");
        }

        public virtual void OnSleep()
        {
            Console.WriteLine($"{GetType().Name}: OnSleep");
        }

        public virtual Task InitializeAsync(INavigationParameters parameters)
        {
            Console.WriteLine($"{GetType().Name}: InitializeAsync");
            return Task.CompletedTask;
        }
    }
}
