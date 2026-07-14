using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Data.Interfaces
{
   public interface IUsuarioRepositorio : IRepositorio<Usuario>
   {
      Task<Usuario?> ObtenerUsuarioPorAlias(string alias);
   }
}
