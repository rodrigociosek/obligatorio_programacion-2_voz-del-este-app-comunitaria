using SQLite;
using System.Collections.ObjectModel;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Data.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SQLiteAsyncConnection _contexto;

        public UsuarioRepositorio(SQLiteDbContext contexto)
        {
            _contexto = contexto.Conexion;
            _contexto.CreateTableAsync<Usuario>().Wait();
        }

        public async Task<bool> ActualizarAsync(Usuario entidad)
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

        public async Task<bool> AgregarAsync(Usuario entidad)
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

        public async Task<bool> EliminarAsync(Usuario entidad)
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

        public Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return _contexto.FindAsync<Usuario?>(id);
        }

        public async Task<List<Usuario>> ObtenerTodoAsync()
        {
            return await _contexto.Table<Usuario>().ToListAsync();
        }

        public Task<Usuario?> ObtenerUsuarioPorAlias(string alias)
        {
            return _contexto.Table<Usuario?>().Where(e => e.Alias == alias).FirstOrDefaultAsync();
        }
    }
}
