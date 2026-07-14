
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Data.Interfaces
{
   public interface ICotizacionRepositorio
   {
      Task GuardarCotizacion(ModeloCotizacion nuevoRegistro);
      Task<ModeloCotizacion> ObtenerCotizacion();
   }
}
