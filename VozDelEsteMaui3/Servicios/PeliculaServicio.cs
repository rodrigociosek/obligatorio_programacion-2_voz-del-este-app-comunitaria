
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Modelos.DTOs;
using VozDelEsteMaui3.Servicios.Interfaces;

namespace VozDelEsteMaui3.Servicios
{
    public class PeliculaServicio : IPeliculaServicio
    {
        private RestClient _restCliente;
        private readonly string _apiKey = "48bd715b8a04b186520b6fd362eba112";

        public async Task<List<ModeloPelicula>> ConsultarPeliculasPorGeneroAsync(List<TheMovieDbGenero> listageneros)
        {
            // TMDB espera IDs separados por comas, no por "OR"
            string generos = string.Join(",", listageneros.Select(g => ((int)g)));

            var opciones = new RestClientOptions("https://api.themoviedb.org/3/discover/movie");

            _restCliente = new RestClient(opciones, configureSerialization: s => s.UseNewtonsoftJson());

            var peticion = new RestRequest()
                .AddQueryParameter("language", "es-ES")
                .AddQueryParameter("page", "1")
                .AddQueryParameter("sort_by", "popularity.desc")
                .AddQueryParameter("with_genres", generos)
                .AddHeader("accept", "application/json")
                .AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0OGJkNzE1YjhhMDRiMTg2NTIwYjZmZDM2MmViYTExMiIsIm5iZiI6MTc1NTcyMDA2Ny4xMDMwMDAyLCJzdWIiOiI2OGE2Mjk4Mzc1MmE4NGE1NWZhODE5YjQiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.xXIfp7PzxiviVVn1_3j5fS7T5wzaLzXA85-XSuPyI9Y");

            var respuesta = await _restCliente.ExecuteAsync<TheMovieDbDTO>(peticion);

            if (!respuesta.IsSuccessful || respuesta.Data?.ListaPosters == null)
            {
                await Shell.Current.DisplayAlert("Lo lamentamos", "No se ha encontrado ninguna pelicula", "Ok");
                return new List<ModeloPelicula>();
            }


            return respuesta.Data.ListaPosters.Select(item => new ModeloPelicula
            {
                PeliculaId = item.PeliculaId,
                Titulo = item.Titulo,
                TituloOriginal = item.TituloOriginal,
                Descripcion = item.Descripcion,
                Anio = item.Anio,
                Generos = item.Generos,
                ImagenUrl = $"https://image.tmdb.org/t/p/w500/{item.CodigoImagen}",
                Valoracion = item.Valoracion,
            }).ToList();
        }

        public async Task<List<ModeloPelicula>> ConsultarPeliculasPorNombreAsync(string palabraclave)
        {
            var opciones = new RestClientOptions("https://api.themoviedb.org/3");
            _restCliente = new RestClient(opciones, configureSerialization: s => s.UseNewtonsoftJson());


            var peticion = new RestRequest("/search/movie", Method.Get);
            peticion.AddHeader("accept", "application/json");
            peticion.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0OGJkNzE1YjhhMDRiMTg2NTIwYjZmZDM2MmViYTExMiIsIm5iZiI6MTc1NTcyMDA2Ny4xMDMwMDAyLCJzdWIiOiI2OGE2Mjk4Mzc1MmE4NGE1NWZhODE5YjQiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.xXIfp7PzxiviVVn1_3j5fS7T5wzaLzXA85-XSuPyI9Y");

            peticion.AddQueryParameter("query", palabraclave);
            peticion.AddQueryParameter("language", "es-ES");
            peticion.AddQueryParameter("page", "1");

            var respuesta = await _restCliente.ExecuteAsync<TheMovieDbDTO>(peticion);

            if (!respuesta.IsSuccessful || respuesta.Data == null)
                return new List<ModeloPelicula>();

            var resultados = respuesta.Data.ListaPosters;

            if (resultados == null || !resultados.Any())
                return new List<ModeloPelicula>();

            return resultados.Select(item => new ModeloPelicula
            {
                PeliculaId = item.PeliculaId,
                Titulo = item.Titulo,
                TituloOriginal = item.TituloOriginal,
                Descripcion = item.Descripcion,
                Anio = item.Anio,
                Generos = item.Generos,
                ImagenUrl = "https://image.tmdb.org/t/p/w500/" + item.CodigoImagen,
                Valoracion = item.Valoracion,
            }).ToList();
        }
    }
}
