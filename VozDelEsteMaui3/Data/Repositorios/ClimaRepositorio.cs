
using SQLite;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Data.Repositorios
{
   public class ClimaRepositorio : IClimaRepositorio
   {
      private readonly SQLiteAsyncConnection _contexto;

      public ClimaRepositorio(SQLiteDbContext contexto)
      {
         _contexto = contexto.Conexion;
         _contexto.CreateTableAsync<ModeloClima>().Wait();
      }

      public async Task ActualizarPronosticosAsync(List<ModeloClima> pronosticos)
      {
         var existentes = await _contexto.Table<ModeloClima>().ToListAsync();

         var actualizables = pronosticos
             .Where(nuevo => existentes.Any(e => e.Fecha == nuevo.Fecha))
             .ToList();
         var nuevos = pronosticos.Where(nuevo => existentes.All(e => e.Fecha != nuevo.Fecha))
            .ToList();

         foreach (var registro in actualizables)
         {
            await _contexto.UpdateAsync(registro);
         }

         foreach (var registro in nuevos)
         {
            await _contexto.InsertAsync(registro);
         }
      }

      public Task GuardarClimaActualAsync(ModeloClima modeloClima)
      {
         return _contexto.InsertAsync(modeloClima);
      }

      public async Task<ModeloClima> ObtenerClimaActualAsync()
      {
         return await _contexto.Table<ModeloClima>().Where(e => e.Fecha <= DateTime.Now)
            .OrderByDescending(e => e.Fecha)
            .FirstOrDefaultAsync();
      }

      public async Task<List<ModeloClima>> PronosticoFuturosAsync()
      {
         var existentes = await _contexto.Table<ModeloClima>().ToListAsync();

         var futuros = existentes.Where(e => e.Fecha > DateTime.Now)
            .OrderByDescending(e => e.Fecha).ToList();

         return futuros;
      }
   }
}
