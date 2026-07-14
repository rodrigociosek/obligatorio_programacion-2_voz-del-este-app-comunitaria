using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.ViewModels
{
    public partial class AgregarPatrocinadorViewModel : ObservableObject
    {
        private readonly IPatrocinadorRepositorio _patrocinadorRepositorio;

        [ObservableProperty]
        private Patrocinador patrocinador;

        public ICommand CrearPatrocinadorCommand { get; }

        public AgregarPatrocinadorViewModel(IPatrocinadorRepositorio patrocinadorRepositorio)
        {
            _patrocinadorRepositorio = patrocinadorRepositorio;

            Patrocinador = new Patrocinador();
            CrearPatrocinadorCommand = new Command(async () => await CrearPatrocinador());
        }
        public void GuardarUbicacionPatrocinador(double lat, double lon)
        {
            Patrocinador.Latitud = lat;
            Patrocinador.Longitud = lon;
        }
        
        public async Task CrearPatrocinador()
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
            await _patrocinadorRepositorio.AgregarAsync(Patrocinador);
            await Shell.Current.DisplayAlert("Exito", "Patrocinador creado correctamente", "OK");
            await Shell.Current.GoToAsync("..");
        }

    }
}

