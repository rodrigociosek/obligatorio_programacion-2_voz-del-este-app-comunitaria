using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VozDelEsteMaui3.Modelos.DTOs
{
   public class ClimaForecastDTO
   {
      [JsonProperty("list")]
      public List<ForecastList> ListaPronosticos { get; set; }
   }

   public class ForecastList
   {
      [JsonProperty("main")]
      public ForecastMainInfo Principal { get; set; }
      
      [JsonProperty("weather")]
      public List<ForecastWheaterInfo> Clima { get; set; }
      
      [JsonProperty("wind")]
      public ForecastWindInfo Viento { get; set; }

      [JsonProperty("dt_txt")]
      public DateTime Fecha { get; set; }
   }

   public class ForecastWheaterInfo
   {
      [JsonProperty("main")]
      public string Estado { get; set; }

      [JsonProperty("description")]
      public string Descripcion { get; set; }
   }

   public class ForecastMainInfo
   {
      [JsonProperty("temp_min")]
      public double TemperaturaMinima { get; set; }
      
      [JsonProperty("temp_max")]
      public double TemperaturaMaxima { get; set; }

      [JsonProperty("temp")]
      public double Temperatura { get; set; }


      [JsonProperty("humidity")]
      public long PorcentajeHumedad { get; set; }
   }

   public class ForecastWindInfo
   {
      [JsonProperty("speed")]
      public double VelocidadViento { get; set; }
   }
}
