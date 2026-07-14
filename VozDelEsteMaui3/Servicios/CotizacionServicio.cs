
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Modelos.DTOs;
using VozDelEsteMaui3.Servicios.Interfaces;

namespace VozDelEsteMaui3.Servicios
{
   public class CotizacionServicio : ICotizacionServicio
   {
      private readonly RestClient _restCliente;
      private readonly string _apiKey = "6e2ca33bebb0b8e217802684fb067bee";

      public CotizacionServicio()
      {
         var options = new RestClientOptions("http://api.currencylayer.com/");
         _restCliente = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
      }

      public async Task<ModeloCotizacion> ConsultarCotizacionActualAsync()
      {
         var peticion = new RestRequest("live", Method.Get);
         peticion.AddParameter("access_key", _apiKey);
         peticion.AddParameter("currencies", "EUR,USD,ARS,BRL");
         peticion.AddParameter("source", "UYU");
         peticion.AddParameter("format", "1");

         var respuesta = await _restCliente.ExecuteAsync<CurrencyLayerDTO>(peticion);

         if (!respuesta.IsSuccessful || respuesta.Data == null)
            throw new Exception($"Error al obtener cotizacion: {(int)respuesta.StatusCode} - {respuesta.ErrorMessage}");

         var cotizacionDTO = respuesta.Data;

         return new ModeloCotizacion
         {
            UyuArs = 1 / cotizacionDTO.Cotizaciones.UyuArs,
            UyuUsd = 1 / cotizacionDTO.Cotizaciones.UyuUsd,
            UyuBrl = 1 / cotizacionDTO.Cotizaciones.UyuBrl,
            UyuEur = 1 / cotizacionDTO.Cotizaciones.UyuEur,
            Fecha = DateTime.Now
         };
      }
   }
}
