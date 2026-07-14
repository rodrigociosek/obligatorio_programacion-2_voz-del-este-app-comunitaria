
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Vistas;
using VozDelEsteMaui3.Vistas.Modales;

namespace VozDelEsteMaui3.ViewModels
{
    public partial class EditarPatrocinadorViewModel : ObservableObject
    {
        private readonly IPatrocinadorRepositorio _patrocinadorRepositorio;

        [ObservableProperty]
        private Patrocinador patrocinador;

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string mapaHtml;


        public ICommand ActualizarPatrocinadorCommand { get; }
        public ICommand CancelarCommand { get; }


        public EditarPatrocinadorViewModel(IPatrocinadorRepositorio patrocinadorRepositorio)
        {
            _patrocinadorRepositorio = patrocinadorRepositorio;

            ActualizarPatrocinadorCommand = new Command(async () => await ActualizarPatrocinador());
        }

        public async Task CargarPatrocinador()
        {
            try
            {
                Patrocinador = await _patrocinadorRepositorio.ObtenerPorIdAsync(Id);
                if (Patrocinador == null) throw new Exception();

                MapaHtml = new LeafletCreadorMapa().BuildMapHtml(
                    LeafletMapMode.Editar,
                    popupTexto: Patrocinador?.Nombre ?? "Ubicación"
                );
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                await Shell.Current.GoToAsync(nameof(Patrocinadores));
                return;
            }
        }
        public void ActualizarUbicacion(double lat, double lng)
        {
            if (Patrocinador != null)
            {
                Patrocinador.Latitud = lat;
                Patrocinador.Longitud = lng;
            }
        }

        private async Task ActualizarPatrocinador()
        {
            if (string.IsNullOrWhiteSpace(Patrocinador.Nombre))
            {
                await Shell.Current.DisplayAlert("Error", "Falta el nombre del patrocinador", "OK");
                return;
            }
            if (Patrocinador.Latitud == 0 || Patrocinador.Longitud == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Falta la ubicación del patrocinador", "OK");
                return;
            }
            await _patrocinadorRepositorio.ActualizarAsync(Patrocinador);
            await Shell.Current.DisplayAlert("Exito", "Patrocinador actualizado correctamente", "OK");
            await Shell.Current.GoToAsync("..");
        }

    }
}
