
using Newtonsoft.Json;

namespace VozDelEsteMaui3.Modelos.DTOs
{
    public class NewsDataDTO
    {
        [JsonProperty("results")]
        public List<NewsDataList> Resultados { get; set; }
    }
    public class NewsDataList
    {
        [JsonProperty("title")]
        public string Titulo { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("creator")]
        public List<string> Autores { get; set; }

        [JsonProperty("description")]
        public string Descripcion { get; set; }

        [JsonProperty("image_url")]
        public string Imagen { get; set; }

        [JsonProperty("pubDate")]
        public string FechaPublicacion { get; set; }
    }
}
