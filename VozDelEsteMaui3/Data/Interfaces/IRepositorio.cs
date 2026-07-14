
using System.Collections.ObjectModel;

namespace VozDelEsteMaui3.Data.Interfaces
{
   public interface IRepositorio<T> where T : class, new() // new() -> la clase debe tener un ctor sin parametros
   {
      Task<T?> ObtenerPorIdAsync(int id);
      Task<List<T>> ObtenerTodoAsync();
      Task<bool> AgregarAsync(T entidad);
      Task<bool> ActualizarAsync(T entidad);
      Task<bool> EliminarAsync(T entidad);
   }
}
