
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VozDelEsteMaui3.Modelos.DTOs
{
    public class TheMovieDbDTO
    {
        [JsonProperty("results")]
        public List<TheMovideDbPoster> ListaPosters { get; set; }
    }
    public class TheMovideDbPoster
    {
        [JsonProperty("id")]
        public int PeliculaId { get; set; }

        [JsonProperty("original_title")]
        public string TituloOriginal { get; set; }

        [JsonProperty("release_date")]
        public DateTime Anio { get; set; }

        [JsonProperty("title")]
        public string Titulo { get; set; }
        
        [JsonProperty("overview")]
        public string Descripcion { get; set; }

        [JsonProperty("vote_average")]
        public string Valoracion { get; set; }

        [JsonProperty("poster_path")]
        public string CodigoImagen { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> Generos { get; set; } 
    }
    public enum TheMovieDbGenero
    {
        SinGenero = -1,

        [Display(Name = "Acción")]
        Action = 28,

        [Display(Name = "Aventura")]
        Adventure = 12,

        [Display(Name = "Animación")]
        Animation = 16,

        [Display(Name = "Comedia")]
        Comedy = 35,

        [Display(Name = "Crimen")]
        Crime = 80,

        [Display(Name = "Documental")]
        Documentary = 99,

        [Display(Name = "Drama")]
        Drama = 18,

        [Display(Name = "Familiar")]
        Family = 10751,

        [Display(Name = "Fantasía")]
        Fantasy = 14,

        [Display(Name = "Historia")]
        History = 36,

        [Display(Name = "Terror")]
        Horror = 27,

        [Display(Name = "Música")]
        Music = 10402,

        [Display(Name = "Misterio")]
        Mystery = 9648,

        [Display(Name = "Romance")]
        Romance = 10749,

        [Display(Name = "Ciencia Ficción")]
        ScienceFiction = 878,

        [Display(Name = "Película de TV")]
        TVMovie = 10770,

        [Display(Name = "Suspenso")]
        Thriller = 53,

        [Display(Name = "Guerra")]
        War = 10752,

        [Display(Name = "Western")]
        Western = 37
    }
}
