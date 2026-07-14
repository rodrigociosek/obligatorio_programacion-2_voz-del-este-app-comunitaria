
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Modelos.DTOs;

namespace VozDelEsteMaui3.Servicios.Interfaces
{
    public interface IPeliculaServicio
    {
        Task<List<ModeloPelicula>> ConsultarPeliculasPorNombreAsync(string palabraclave);
        Task<List<ModeloPelicula>> ConsultarPeliculasPorGeneroAsync(List<TheMovieDbGenero> listageneros);
    }
}
