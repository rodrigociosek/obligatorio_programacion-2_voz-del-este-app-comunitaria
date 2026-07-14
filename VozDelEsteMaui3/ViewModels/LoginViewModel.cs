using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Windows.Input;
using VozDelEsteMaui3.Servicios.Interfaces;
using VozDelEsteMaui3.Vistas;

namespace VozDelEsteMaui3.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly ISesionServicio _sesionServicio;
        private readonly IServiceProvider _servicios;

        public string Usuario { get; set; }
        public string Clave { get; set; }

        [ObservableProperty]
        private bool esMobil; 
        public ICommand LoginCommand { get; }
        public ICommand RegistroCommand { get; }
        public ICommand IngresarConHuellaCommand { get; }
        

        public LoginViewModel(ISesionServicio sesion, IServiceProvider services)
        {
            _sesionServicio = sesion;
            _servicios = services;

            LoginCommand = new Command(async () => await LoginUsuario());

            RegistroCommand = new Command(async () => await RegistroUsuario());

            IngresarConHuellaCommand = new Command(async () => await AutenticarConHuellaAsync());

            EsMobil = DeviceInfo.Platform != DevicePlatform.WinUI;
        }
        public async Task AutenticarConHuellaAsync()
        {
            var resultado = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration(
                "Autenticación requerida", "Usa tu huella digital"));

            if (resultado.Authenticated)
            {
                var success = await _sesionServicio.LoginAsync("franco", "1234");
                if (success)
                {
                    var ventana = Application.Current?.Windows.FirstOrDefault();
                    if (ventana != null)
                    {
                        ventana.Page = new AppShell();
                    }
                }
            }
        }

        public async Task LoginUsuario()
        {
            var success = await _sesionServicio.LoginAsync(Usuario, Clave);
            if (success)
            {
                var ventana = Application.Current?.Windows.FirstOrDefault();
                if (ventana != null)
                {
                    ventana.Page = new AppShell();
                }
            }
            else
            {
                var paginaActual = Application.Current?.Windows.FirstOrDefault()?.Page;
                if (paginaActual != null)
                {
                    await paginaActual.DisplayAlert("Error", "Usuario o clave incorrecta", "Ok");
                }
            }
        }

        public async Task RegistroUsuario()
        {
            var registroPage = _servicios.GetRequiredService<Registro>();
            var paginaActual = Application.Current?.Windows.FirstOrDefault()?.Page as NavigationPage;

            if (paginaActual != null)
            {
                await paginaActual.PushAsync(registroPage);
            }
        }
    }
}