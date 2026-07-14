
using System.Windows.Input;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Servicios.Interfaces;

namespace VozDelEsteMaui3.ViewModels
{
   public class AjustesModalViewModel
   {
      private readonly ISesionServicio _sesionServicio;

      public PreferenciasUsuario Preferencias => _sesionServicio.PreferenciasUsuario;
      public ICommand AplicarCambiosCommand { get; }

      public AjustesModalViewModel(ISesionServicio sesionServicio)
      {
         _sesionServicio = sesionServicio;
         AplicarCambiosCommand = new Command(async () => await AplicarCambios());
      }

      private async Task AplicarCambios()
      {
         _sesionServicio.GuardarPreferencias();
         await Application.Current.MainPage.Navigation.PopModalAsync();
      }
   }
}
