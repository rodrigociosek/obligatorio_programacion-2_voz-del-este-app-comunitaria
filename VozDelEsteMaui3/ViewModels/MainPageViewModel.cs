using System.Windows.Input;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Servicios.Interfaces;
using VozDelEsteMaui3.Vistas;
using VozDelEsteMaui3.Vistas.Modales;

namespace VozDelEsteMaui3.ViewModels
{
   public class MainPageViewModel
   {
      private readonly ISesionServicio _sesionServicio;
      private readonly IServiceProvider _serviceProvider;
      
      public Usuario UsuarioActual { get; }
      public PreferenciasUsuario Preferencias => _sesionServicio.PreferenciasUsuario;

      public ICommand AbrirMenuUsuarioCommand { get; }
      public ICommand VerInformacionCommand { get; }
      public ICommand AbrirAjustesCommand { get; }
      public ICommand CerrarSesionCommand { get; }
      public ICommand CerrarModalCommand { get; }

      public ICommand IrPaginaClimaCommand { get; }
      public ICommand IrPaginaCotizacionesCommand { get; }
      public ICommand IrPaginaNoticiasCommand { get; }
      public ICommand IrPaginaPeliculasCommand { get; }
      public ICommand IrPaginaPatrocinadoresCommand { get; }

      public MainPageViewModel(ISesionServicio sesionServicio, IServiceProvider serviceProvider)
      {
         _sesionServicio = sesionServicio;
         _serviceProvider = serviceProvider;
         UsuarioActual = sesionServicio.UsuarioIngresado;

         AbrirMenuUsuarioCommand = new Command(async () => await AbrirMenuUsuarioModal());
         VerInformacionCommand = new Command(async () => await AbrirInformacion());
         AbrirAjustesCommand = new Command(async () => await AbrirAjustes());
         CerrarSesionCommand = new Command(async () => await CerrarSesion());
         CerrarModalCommand = new Command(async () => await CerrarModal());

         IrPaginaClimaCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(Clima)));
         IrPaginaCotizacionesCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(Cotizaciones)));
         IrPaginaNoticiasCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(Noticias)));
         IrPaginaPeliculasCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(Peliculas)));
         IrPaginaPatrocinadoresCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(Patrocinadores)));
      }

      private async Task AbrirAjustes()
      {
         var modal = new AjustesUsuarioModal()
         {
            BindingContext = new AjustesModalViewModel(_sesionServicio),
         };
         await Application.Current.MainPage.Navigation.PushModalAsync(modal);
      }

      private async Task AbrirInformacion()
      {
         var modal = new InformacionUsuarioModal()
         {
            BindingContext = this,
         };
         await Application.Current.MainPage.Navigation.PushModalAsync(modal);
      }

      private async Task AbrirMenuUsuarioModal()
      {
         var modal = new MenuUsuarioModal()
         {
            BindingContext = this,
         };
         await Application.Current.MainPage.Navigation.PushModalAsync(modal);
      }

      private async Task CerrarModal()
      {
         await Application.Current.MainPage.Navigation.PopModalAsync();
      }

      private async Task CerrarSesion()
      {
         _sesionServicio.CerrarSesion();
         Application.Current.Dispatcher.Dispatch(() =>
         {
            if (Application.Current is App app)
            {
               app.ReiniciarApp();
            }
         });
      }

      
   }

}
