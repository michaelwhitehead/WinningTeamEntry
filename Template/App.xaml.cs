using Prism;
using Prism.Ioc;
using Template.Constants;
using Template.Services;
using Template.Services.Interfaces;
using Template.ViewModels;
using Template.Views;
using Xamarin.Forms;

namespace Template
{
    public partial class App
    {
        public App()
        {
        }

        public App(IPlatformInitializer platformInitializer)
            : base(platformInitializer)
        {
        }

        public App(IPlatformInitializer platformInitializer, bool setFormsDependencyResolver)
            : base(platformInitializer, setFormsDependencyResolver)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(MainPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterNavigation(containerRegistry);
            RegisterServices(containerRegistry);
        }

        private void RegisterNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(NavUriConst.MainPage);
        }

        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ILogger, Logger>();
            containerRegistry.Register<IBaseServices, BaseServices>();
        }
    }
}
