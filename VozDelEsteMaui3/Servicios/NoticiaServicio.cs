
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Modelos.DTOs;
using VozDelEsteMaui3.Servicios.Interfaces;

namespace VozDelEsteMaui3.Servicios
{
    public class NoticiaServicio : INoticiaServicio
    {
        private readonly RestClient _restCliente;
        private readonly string _apiKey = "pub_51d8efb0545646638272ceb9350604e7";

        public NoticiaServicio()
        {
            var options = new RestClientOptions("https://newsdata.io/api/1/");
            _restCliente = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
        }

        public async Task<List<ModeloNoticia>> ConsultarNoticiasAsync(string palabraclave)
        {
            var peticion = new RestRequest("latest", Method.Get);
            peticion.AddParameter("apikey", _apiKey);
            peticion.AddParameter("country", "uy");
            peticion.AddParameter("language", "es");
            peticion.AddParameter("q", $"{palabraclave}");

            var respuesta = await _restCliente.ExecuteAsync<NewsDataDTO>(peticion);

            if (!respuesta.IsSuccessful || respuesta.Data == null)
                throw new Exception($"Error al obtener noticias: {(int)respuesta.StatusCode} - {respuesta.ErrorMessage}");

            var noticiaDTO = respuesta.Data.Resultados;

            List<ModeloNoticia> listaNoticias = new();

            var resultados = respuesta.Data?.Resultados;

            if (resultados == null || !resultados.Any())
                return new List<ModeloNoticia>();

            return resultados.Select(item => new ModeloNoticia
            {
                Titulo = item.Titulo,
                Descripcion = item.Descripcion,
                FechaPublicacion = item.FechaPublicacion,
                Autores = item.Autores,
                Imagen = item.Imagen,
                Link = item.Link
            }).ToList();
        }
    }
}
