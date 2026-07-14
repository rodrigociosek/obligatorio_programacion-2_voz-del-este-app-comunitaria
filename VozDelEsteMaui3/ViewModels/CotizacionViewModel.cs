using CommunityToolkit.Mvvm.ComponentModel;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Orquestadores;

namespace VozDelEsteMaui3.ViewModels
{
   public partial class CotizacionViewModel : ObservableObject
   {
      private readonly OrquestadorCotizacion _orquestadorCotizacion;

      [ObservableProperty]
      private ModeloCotizacion cotizacionActual;

      public CotizacionViewModel(OrquestadorCotizacion orquestadorCotizacion)
      {
         _orquestadorCotizacion = orquestadorCotizacion;
      }

      public async Task CargarDatosAsync()
      {
         CotizacionActual = await _orquestadorCotizacion.ObtenerCotizacionAsync();
      }
   }
}
