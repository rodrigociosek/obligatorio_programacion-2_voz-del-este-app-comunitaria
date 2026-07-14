
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Servicios.Interfaces;
using VozDelEsteMaui3.Vistas.Modales;

namespace VozDelEsteMaui3.ViewModels
{
    public partial class NoticiaViewModel : ObservableObject
    {
        private readonly INoticiaServicio _noticiaServicio;

        [ObservableProperty]
        private ObservableCollection<ModeloNoticia> noticias;

        [ObservableProperty]
        private string palabraclave;

        public ICommand VerNoticiaCommand { get; }
        public ICommand BuscarCommand { get; }

        public NoticiaViewModel(INoticiaServicio noticiaServicio)
        {
            _noticiaServicio = noticiaServicio;

            BuscarCommand = new Command(async () => await CargarDatos(Palabraclave));

            VerNoticiaCommand = new Command<ModeloNoticia>(async (noticia) => await AbrirDetalleNoticia(noticia));
        }

        private async Task AbrirDetalleNoticia(ModeloNoticia noticia)
        {
            try
            {
                var modal = new DetalleNoticiaModal()
                {
                    BindingContext = noticia
                };
                await Shell.Current.Navigation.PushModalAsync(modal);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error: {ex}", "Ok");
            }

        }

        public async Task CargarDatos(string palabraclave)
        {
            try
            {
                var lista = await _noticiaServicio.ConsultarNoticiasAsync(palabraclave);
                Noticias = new ObservableCollection<ModeloNoticia>(lista);
                await Shell.Current.DisplayAlert("Exito", $"Noticias cargadas correctamente", "Ok");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error: {ex}", "Ok");
            }
            
        }
    }
}
