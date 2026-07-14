
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Servicios.Interfaces
{
    public interface INoticiaServicio
    {
        Task<List<ModeloNoticia>> ConsultarNoticiasAsync(string palabraclave);
    }
}
