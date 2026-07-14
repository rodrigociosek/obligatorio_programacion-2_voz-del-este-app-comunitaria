
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Servicios.Interfaces;

namespace VozDelEsteMaui3.Orquestadores
{
   public class OrquestadorCotizacion
   {
      private readonly ICotizacionServicio _cotizacionServicio;
      private readonly ICotizacionRepositorio _cotizacionRepositorio;
      private readonly TimeSpan _intervaloCotizacion = TimeSpan.FromMinutes(20);

      public OrquestadorCotizacion(ICotizacionServicio cotizacionServicio, ICotizacionRepositorio cotizacionRepositorio)
      {
         _cotizacionServicio = cotizacionServicio;
         _cotizacionRepositorio = cotizacionRepositorio;
      }

      public async Task<ModeloCotizacion> ObtenerCotizacionAsync()
      {
         var ahora = DateTime.Now;

         var ultimoRegistro = await _cotizacionRepositorio.ObtenerCotizacion();

         if (ultimoRegistro == null || ahora - ultimoRegistro.Fecha > _intervaloCotizacion)
         {
            var nuevoRegistro = await _cotizacionServicio.ConsultarCotizacionActualAsync();
            await _cotizacionRepositorio.GuardarCotizacion(nuevoRegistro);

            return nuevoRegistro;
         }
         return ultimoRegistro;
      }
   }
}
