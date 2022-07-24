using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;

namespace b.Mvvm.Prism
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
        }
    }
}
