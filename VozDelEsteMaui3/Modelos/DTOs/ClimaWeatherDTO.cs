using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VozDelEsteMaui3.Modelos.DTOs
{
   public class ClimaWeatherDTO
   {
      [JsonProperty("weather")]
      public List<WheaterInfo> Clima { get; set; }

      [JsonProperty("main")]
      public WeatherMainInfo Principal { get; set; }

      [JsonProperty("wind")]
      public WeatherWindInfo Viento { get; set; }
   }

   public class WheaterInfo
   {
      [JsonProperty("main")]
      public string Estado { get; set; }

      [JsonProperty("description")]
      public string Descripcion { get; set; }
   }

   public class WeatherMainInfo
   {
      [JsonProperty("temp")]
      public double Temperatura { get; set; }

      [JsonProperty("temp_min")]
      public double TemperaturaMinima { get; set; }

      [JsonProperty("temp_max")]
      public double TemperaturaMaxima { get; set; }

      [JsonProperty("humidity")]
      public long PorcentajeHumedad { get; set; }
   }

   public class WeatherWindInfo
   {
      [JsonProperty("speed")]
      public double VelocidadViento { get; set; }
   }
}
