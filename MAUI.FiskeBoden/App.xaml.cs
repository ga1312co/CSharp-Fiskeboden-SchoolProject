using Microsoft.Extensions.DependencyInjection;

namespace Informatics.FiskeBoden
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; }

        public App(IServiceProvider services)
        {
            InitializeComponent();
            Services = services;
            MainPage = new AppShell();
        }
    }
}