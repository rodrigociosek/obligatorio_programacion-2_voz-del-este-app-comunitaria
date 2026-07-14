
using SQLite;
using System.Collections.ObjectModel;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Data.Repositorios
{
    public class PatrocinadorRepositorio : IPatrocinadorRepositorio
    {
        private readonly SQLiteAsyncConnection _contexto;

        public PatrocinadorRepositorio(SQLiteDbContext contexto)
        {
            _contexto = contexto.Conexion;
            _contexto.CreateTableAsync<Patrocinador>().Wait();
        }

        public async Task<bool> ActualizarAsync(Patrocinador entidad)
        {
            try
            {
                await _contexto.UpdateAsync(entidad);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AgregarAsync(Patrocinador entidad)
        {
            try
            {
                await _contexto.InsertAsync(entidad);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EliminarAsync(Patrocinador entidad)
        {
            try
            {
                await _contexto.DeleteAsync(entidad);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<Patrocinador?> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Table<Patrocinador>().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Patrocinador>> ObtenerTodoAsync()
        {
            try
            {
               return await _contexto.Table<Patrocinador>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al obtener la lista de patrocinadores", ex);
            }
        }
    }
}
