
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Servicios.Interfaces;

namespace VozDelEsteMaui3.Orquestadores
{
   public class OrquestadorClima
   {
      private readonly IClimaServicio _climaServicio;
      private readonly IClimaRepositorio _climaRepositorio;
      private readonly TimeSpan _intervaloClima = TimeSpan.FromMinutes(3);
      private readonly int _minimoPronosticos = 40;


      public OrquestadorClima(IClimaServicio climaServicio, IClimaRepositorio climaRepositorio)
      {
         _climaServicio = climaServicio;
         _climaRepositorio = climaRepositorio;
      }

      public async Task<ModeloClima> ObtenerClimaActualAsync()
      {
         var ahora = DateTime.Now;

         var ultimoRegistro = await _climaRepositorio.ObtenerClimaActualAsync();
         if (ultimoRegistro == null || ahora - ultimoRegistro.Fecha > _intervaloClima)
         {
            var nuevoRegistro = await _climaServicio.ConsultarClimaActualAsync();
            await _climaRepositorio.GuardarClimaActualAsync(nuevoRegistro);
            return nuevoRegistro;
         }
         

         return ultimoRegistro;
      }

      public async Task<List<ModeloClima>> ObtenerPronosticoAsync()
      {
         var ahora = DateTime.Now;

         var existentes = await _climaRepositorio.PronosticoFuturosAsync();
         var cantidadPronosticoFuturos = existentes.Count;

         if (existentes == null || cantidadPronosticoFuturos < _minimoPronosticos)
         {
            var nuevos = await _climaServicio.ConsultarPronosticoAsync();
            await _climaRepositorio.ActualizarPronosticosAsync(nuevos);
            return nuevos;
         }
         return existentes;
      }

   }
}
