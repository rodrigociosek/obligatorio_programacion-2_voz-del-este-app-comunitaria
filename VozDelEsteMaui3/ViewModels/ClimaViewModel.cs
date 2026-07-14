
using CommunityToolkit.Mvvm.ComponentModel;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Orquestadores;

namespace VozDelEsteMaui3.ViewModels
{
    public partial class ClimaViewModel : ObservableObject
    {
        private readonly OrquestadorClima _orquestadorClima;

        [ObservableProperty]
        private ModeloClima climaActual;

        [ObservableProperty]
        private List<ModeloClima> pronostico;

        [ObservableProperty]
        private string fondoUrl;

        public ClimaViewModel(OrquestadorClima orquestadorClima)
        {
            _orquestadorClima = orquestadorClima;
        }

        public async Task CargarDatosAsync()
        {
            ClimaActual = await _orquestadorClima.ObtenerClimaActualAsync();
            Pronostico = await _orquestadorClima.ObtenerPronosticoAsync();
            CambiarFondoClima();
        }

        private void CambiarFondoClima()
        {
            FondoUrl = ClimaActual.Estado switch
            {
                "Clouds" => "clouds.jpg",
                "Clear" => "clear.jpg",
                "Rain" => "rain.jpg",
                _ => ""
            };
        }

    }
}
