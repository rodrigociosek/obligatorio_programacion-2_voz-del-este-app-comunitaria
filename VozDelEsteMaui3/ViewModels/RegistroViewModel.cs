using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.ViewModels
{
    public partial class RegistroViewModel : ObservableObject
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        [ObservableProperty]
        private string alias;

        [ObservableProperty]
        private string clave;

        [ObservableProperty]
        private string nombreCompleto;

        [ObservableProperty]
        private string direccion;

        [ObservableProperty]
        private string telefono;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string fotoUrl;

        [ObservableProperty]
        private bool esAdmin;

        public ICommand SacarFotoCommand { get; }

        public RegistroViewModel(IUsuarioRepositorio usuarioRepositorio)
        {
            this._usuarioRepositorio = usuarioRepositorio;
            SacarFotoCommand = new Command(async () => await TomarFotoAsync());
        }

        private async Task TomarFotoAsync()
        {
            try
            {
                // Verificamos si la cámara está disponible
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    // Abrimos la cámara y esperamos la foto
                    FileResult foto = await MediaPicker.Default.CapturePhotoAsync();

                    if (foto != null)
                    {
                        // Guardamos la foto en AppDataDirectory
                        string rutaDestino = Path.Combine(FileSystem.AppDataDirectory, foto.FileName);
                        using var stream = await foto.OpenReadAsync();
                        using var nuevoArchivo = File.OpenWrite(rutaDestino);
                        await stream.CopyToAsync(nuevoArchivo);
                        FotoUrl = rutaDestino;
                        await Application.Current.MainPage.DisplayAlert("Exito", "Foto sacada correctamente", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error al tomar la foto", $"Error: {ex.Message}", "OK");
            }
        }


        [RelayCommand]
        private async Task Registrar() // Genera "RegistrarCommand"
        {
            var nuevoUsuario = new Usuario
            {
                Alias = Alias,
                Clave = Clave,
                NombreCompleto = NombreCompleto,
                Direccion = Direccion,
                Telefono = Telefono,
                Email = Email,
                FotoUrl = FotoUrl,
                EsAdmin = EsAdmin
            };

            // Aquí podrías llamar a tu repositorio para guardar
            var exito = await _usuarioRepositorio.AgregarAsync(nuevoUsuario);
            if (exito) await Application.Current.MainPage.DisplayAlert("Exito", "Usuario creado correctamente", "OK");
            else await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error", "OK");
        }
    }

}
