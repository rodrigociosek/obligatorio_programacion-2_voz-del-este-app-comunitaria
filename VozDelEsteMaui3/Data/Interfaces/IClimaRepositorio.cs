
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Data.Interfaces
{
   public interface IClimaRepositorio
   {
      Task<ModeloClima> ObtenerClimaActualAsync();
      Task GuardarClimaActualAsync(ModeloClima modeloClima);

      Task<List<ModeloClima>> PronosticoFuturosAsync();
      Task ActualizarPronosticosAsync(List<ModeloClima> nuevos);
   }
}
