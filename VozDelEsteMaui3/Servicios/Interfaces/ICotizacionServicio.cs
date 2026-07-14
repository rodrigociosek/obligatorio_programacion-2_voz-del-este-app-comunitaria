
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Servicios.Interfaces
{
   public interface ICotizacionServicio
   {
      Task<ModeloCotizacion> ConsultarCotizacionActualAsync();
   }
}
