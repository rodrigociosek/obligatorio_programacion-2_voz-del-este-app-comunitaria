
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Newtonsoft.Json;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Modelos.DTOs;
using VozDelEsteMaui3.Servicios.Interfaces;

namespace VozDelEsteMaui3.Servicios
{
   public class ClimaServicio : IClimaServicio
   {
      private readonly RestClient _restCliente;
      private readonly string _apiKey = "14c116c1be304a8540aba98c52fe6398";

      public ClimaServicio()
      {
         var options = new RestClientOptions("https://api.openweathermap.org/data/2.5/");
         _restCliente = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
      }

      public async Task<ModeloClima> ConsultarClimaActualAsync()
      {
         var peticion = new RestRequest("weather", Method.Get);
         peticion.AddParameter("q", "Maldonado,UY");
         peticion.AddParameter("appid", _apiKey);
         peticion.AddParameter("units", "metric");
         peticion.AddParameter("lang", "es");

         var respuesta = await _restCliente.ExecuteAsync<ClimaWeatherDTO>(peticion);

         if (!respuesta.IsSuccessful || respuesta.Data == null)
            throw new Exception($"Error al obtener clima: {(int)respuesta.StatusCode} - {respuesta.ErrorMessage}");

         var climaDTO = respuesta.Data;

         return new ModeloClima
         {
            Estado = climaDTO.Clima[0].Estado,
            Descripcion = climaDTO.Clima[0].Descripcion,
            Temperatura = climaDTO.Principal.Temperatura,
            TemperaturaMaxima = climaDTO.Principal.TemperaturaMaxima,
            TemperaturaMinima = climaDTO.Principal.TemperaturaMinima,
            PorcentajeHumedad = climaDTO.Principal.PorcentajeHumedad,
            VelocidadViento = climaDTO.Viento.VelocidadViento,
            Fecha = DateTime.Now
         };
      }

      public async Task<List<ModeloClima>> ConsultarPronosticoAsync()
      {
         var peticion = new RestRequest("forecast", Method.Get);
         peticion.AddParameter("q", "Maldonado,UY");
         peticion.AddParameter("appid", _apiKey);
         peticion.AddParameter("units", "metric");
         peticion.AddParameter("lang", "es");

         var respuesta = await _restCliente.ExecuteAsync<ClimaForecastDTO>(peticion);

         if (!respuesta.IsSuccessful || respuesta.Data == null)
            throw new Exception($"Error al obtener clima: {(int)respuesta.StatusCode} - {respuesta.ErrorMessage}");

         var climaDTO = respuesta.Data;

         List<ModeloClima> pronosticos = new();

         foreach (var item in climaDTO.ListaPronosticos)
         {
            ModeloClima modeloClima = new ModeloClima()
            {
               Estado = item.Clima[0].Estado,
               Descripcion = item.Clima[0].Descripcion,
               Temperatura = item.Principal.Temperatura,
               TemperaturaMaxima = item.Principal.TemperaturaMaxima,
               TemperaturaMinima = item.Principal.TemperaturaMinima,
               PorcentajeHumedad = item.Principal.PorcentajeHumedad,
               VelocidadViento = item.Viento.VelocidadViento,
               Fecha = item.Fecha,
               IconoUrl = ObtenerIconoUrl(item.Clima[0].Estado)
            };
            pronosticos.Add(modeloClima);
         }

         return pronosticos;
      }
        private string ObtenerIconoUrl(string estado)
        {
            return estado switch
            {   
                "Clouds" => "cloudicono.png",
                "Clear" => "clearicono.png",
                "Rain" => "rainicono.png",
                _ => ""
            };
        }
   }
}
