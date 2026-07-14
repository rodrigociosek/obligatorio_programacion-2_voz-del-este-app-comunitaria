
using VozDelEsteMaui3.Modelos.DTOs;

namespace VozDelEsteMaui3.Modelos
{
    public class ModeloPelicula
    {
        public int PeliculaId { get; set; }

        public string TituloOriginal { get; set; }

        public DateTime Anio { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Valoracion { get; set; }

        public string ImagenUrl { get; set; }

        public List<int> Generos { get; set; }
        public string GenerosTexto => Generos?
        .Select(id => ((TheMovieDbGenero)id).ToString())
        .Where(nombre => nombre != nameof(TheMovieDbGenero.SinGenero))
        .Aggregate((a, b) => $"{a}, {b}") ?? string.Empty;
    }
}
