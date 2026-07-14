
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Vistas;
using VozDelEsteMaui3.Vistas.Modales;

namespace VozDelEsteMaui3.ViewModels
{
    public partial class PatrocinadorViewModel : ObservableObject
    {
        private readonly IPatrocinadorRepositorio _patrocinadorRepositorio;

        [ObservableProperty]
        private ObservableCollection<Patrocinador> patrocinadores;

        public ICommand AgregarPatrocinadorCommand { get; }
        public ICommand VerUbicacionCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand GuardarCambiosCommand { get; }
        public ICommand CancelarCommand { get; }


        public PatrocinadorViewModel(IPatrocinadorRepositorio patrocinadorRepositorio)
        {
            _patrocinadorRepositorio = patrocinadorRepositorio;

            AgregarPatrocinadorCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AgregarPatrocinador)));
            CancelarCommand = new Command(async () => await CerrarModal());
            EditarCommand = new Command<int>(async (id) => await IrPaginaEditarPatrocinador(id));
            EliminarCommand = new Command<Patrocinador>(async (patrocinador) => await EliminarPatrocinador(patrocinador));
            VerUbicacionCommand = new Command<Patrocinador>(async (patrocinador) => await IrModalUbicacion(patrocinador));
        }

        private async Task IrModalUbicacion(Patrocinador patrocinador)
        {
            var modal = new VerUbicacionPatrocinador(_patrocinadorRepositorio)
            {
                BindingContext = this
            };
            modal.Patrocinador = patrocinador;
            await Application.Current.MainPage.Navigation.PushModalAsync(modal);
        }

        private async Task EliminarPatrocinador(Patrocinador patrocinador)
        {
            bool resultado = await Shell.Current.DisplayAlert("Confirmación","¿Estás seguro de que querés continuar?","Sí","No");

            if (resultado)
            {
                if (patrocinador != null)
                {
                    await _patrocinadorRepositorio.EliminarAsync(patrocinador);
                    Patrocinadores.Remove(patrocinador);
                    await Shell.Current.DisplayAlert("Exito", $"Se ha eliminado {patrocinador.Nombre} con exito","Ok");
                    return;
                }
                await Shell.Current.DisplayAlert("Error", $"No se encontro el patrocinador","Ok");
            }
        }

        public async Task IrPaginaEditarPatrocinador(int id)
        {
            await Shell.Current.GoToAsync($"{nameof(EditarPatrocinador)}?Id={id}");
        }

        public async Task CargarPatrocinadoresAsync()
        {
            var lista = await _patrocinadorRepositorio.ObtenerTodoAsync();
            Patrocinadores = new ObservableCollection<Patrocinador>(lista);
        }
        private async Task CerrarModal()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
