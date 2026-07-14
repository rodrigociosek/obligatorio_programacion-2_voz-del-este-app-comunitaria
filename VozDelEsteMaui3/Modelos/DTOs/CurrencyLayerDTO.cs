using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VozDelEsteMaui3.Modelos.DTOs
{
   public class CurrencyLayerDTO
   {
      [JsonProperty("quotes")]
      public QuotesInfo Cotizaciones { get; set; }
   }

   public class QuotesInfo
   {
      [JsonProperty("UYUEUR")]
      public float UyuEur {  get; set; }

      [JsonProperty("UYUUSD")]
      public float UyuUsd { get; set; }

      [JsonProperty("UYUARS")]
      public float UyuArs { get; set; }

      [JsonProperty("UYUBRL")]
      public float UyuBrl { get; set; }
   }
}
