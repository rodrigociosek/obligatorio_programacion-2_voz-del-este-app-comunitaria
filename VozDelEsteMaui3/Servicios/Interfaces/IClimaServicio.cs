
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Servicios.Interfaces
{
   public interface IClimaServicio
   {
      Task<ModeloClima> ConsultarClimaActualAsync();
      Task<List<ModeloClima>> ConsultarPronosticoAsync();
   }
}
