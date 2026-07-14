
using SQLite;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Data.Repositorios
{
   public class CotizacionRepositorio : ICotizacionRepositorio
   {
      private readonly SQLiteAsyncConnection _contexto;

      public CotizacionRepositorio(SQLiteDbContext contexto)
      {
         _contexto = contexto.Conexion;
         _contexto.CreateTableAsync<ModeloCotizacion>().Wait();
      }

      public async Task GuardarCotizacion(ModeloCotizacion nuevoRegistro)
      {
         await _contexto.InsertAsync(nuevoRegistro);
      }

      public Task<ModeloCotizacion> ObtenerCotizacion()
      {
         return _contexto.Table<ModeloCotizacion>().OrderByDescending(e => e.Fecha).FirstOrDefaultAsync();
      }
   }
}
