using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Modelos.DTOs;
using VozDelEsteMaui3.Servicios.Interfaces;
using VozDelEsteMaui3.Vistas.Modales;

namespace VozDelEsteMaui3.ViewModels
{
    public partial class PeliculaViewModel : ObservableObject
    {
        private readonly IPeliculaServicio _peliculaServicio;
        
        [ObservableProperty]
        private ObservableCollection<ModeloPelicula> peliculas;

        [ObservableProperty]
        private ObservableCollection<GeneroSeleccion> generos;

        [ObservableProperty]
        private string palabraclave;

        public ICommand VerNoticiaCommand { get; }
        public ICommand BuscarCommand { get; }
        public ICommand VerPeliculaCommand { get; }
         


        public PeliculaViewModel(IPeliculaServicio peliculaServicio)
        {
            _peliculaServicio = peliculaServicio;

            generos = new ObservableCollection<GeneroSeleccion>(
            Enum.GetValues(typeof(TheMovieDbGenero))
                .Cast<TheMovieDbGenero>()
                .Select(g => new GeneroSeleccion { Genero = g, EstaSeleccionado = false }));

            BuscarCommand = new Command(async () => await CargarDatos(Palabraclave,Generos));
            VerPeliculaCommand = new Command<ModeloPelicula>(async (pelicula) => await IrModalDetallePelicula(pelicula));
            
        }


        private async Task IrModalDetallePelicula(ModeloPelicula pelicula)
        {
            var modal = new DetallePeliculaModal()
            {
                BindingContext = pelicula,
            };
            await Shell.Current.Navigation.PushModalAsync(modal);
        }

        private async Task CargarDatos(string palabraclave, ObservableCollection<GeneroSeleccion> generos)
        {
            if (string.IsNullOrEmpty(palabraclave) && !generos.Any(e => e.EstaSeleccionado == true))
            {
                await Shell.Current.DisplayAlert("Aviso", "Debe seleccionar una categoria o ingresar un nombre", "Ok");
                return;
            }

            if (!string.IsNullOrEmpty(palabraclave))
            {
                var lista = await _peliculaServicio.ConsultarPeliculasPorNombreAsync(palabraclave);

                if (generos.Any(e => e.EstaSeleccionado == true))
                {
                    var generosSeleccionadosInt = generos
                    .Where(g => g.EstaSeleccionado)
                    .Select(g => (int)g.Genero)
                    .ToList();

                    var listaFiltrada = lista
                        .Where(p => p.Generos.Any(id => generosSeleccionadosInt.Contains(id)))
                        .ToList();

                    var listaConvertida = new ObservableCollection<ModeloPelicula>(listaFiltrada);
                    Peliculas = listaConvertida;
                    return;
                }
                
                var listaPeliculas = new ObservableCollection<ModeloPelicula>(lista);
                Peliculas = listaPeliculas;
            }
            else
            {
                var generosSeleccionados = generos
                .Where(g => g.EstaSeleccionado)
                .Select(g => g.Genero)
                .ToList();

                var lista = await _peliculaServicio.ConsultarPeliculasPorGeneroAsync(generosSeleccionados);
                var listaConvertida = new ObservableCollection<ModeloPelicula>(lista);

                Peliculas= listaConvertida;
            }
        }
    }
}
