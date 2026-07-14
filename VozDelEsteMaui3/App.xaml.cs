using Microsoft.Extensions.DependencyInjection;
using VozDelEsteMaui3.Servicios.Interfaces;
using VozDelEsteMaui3.Vistas;

namespace VozDelEsteMaui3
{
    public partial class App : Application
    {
        private readonly ISesionServicio _sesionServicio;
        private readonly IServiceProvider _serviceProvider;

        public App(ISesionServicio sesionServicio, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _sesionServicio = sesionServicio;
            _serviceProvider = serviceProvider;
            Application.Current.UserAppTheme = AppTheme.Dark;
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Page paginaInicial;

            if (_sesionServicio.EstaAutenticado)
            {
                paginaInicial = new AppShell();
            }
            else
            {
                paginaInicial = new NavigationPage(_serviceProvider.GetRequiredService<Login>());
            }

            return new Window(paginaInicial);
        }

        public void ReiniciarApp()
        {
            var nuevaPagina = new NavigationPage(_serviceProvider.GetRequiredService<Login>());
            Application.Current.Windows.FirstOrDefault().Page = nuevaPagina;
        }
    }
}