using CommunityToolkit.Mvvm.ComponentModel;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.Servicios.Interfaces;

namespace VozDelEsteMaui3.Servicios
{
   public partial class SesionServicio : ObservableObject, ISesionServicio
   {
      private readonly IUsuarioRepositorio _usuarioRepositorio;

      [ObservableProperty]
      private Usuario? usuarioActual;

      public bool EstaAutenticado => UsuarioActual != null;

      public Usuario? UsuarioIngresado => UsuarioActual;

      public PreferenciasUsuario PreferenciasUsuario { get; private set; }

      public SesionServicio(IUsuarioRepositorio usuarioRepositorio)
      {
         _usuarioRepositorio = usuarioRepositorio;
         PreferenciasUsuario = CargarPreferencias();
      }

      private PreferenciasUsuario CargarPreferencias()
      {
         return new PreferenciasUsuario
         {
            MostrarClima = Preferences.Get("mostrarClima", true),
            MostrarCotizaciones = Preferences.Get("mostrarCotizaciones", true),
            MostrarNoticias = Preferences.Get("mostrarNoticias", true),
            MostrarPeliculas = Preferences.Get("mostrarPeliculas", true),
            MostrarPatrocinadores = Preferences.Get("mostrarPatrocinadores", true),
         };
      }

      public void GuardarPreferencias()
      {
         Preferences.Set("mostrarClima", PreferenciasUsuario.MostrarClima);
         Preferences.Set("mostrarCotizaciones", PreferenciasUsuario.MostrarCotizaciones);
         Preferences.Set("mostrarNoticias", PreferenciasUsuario.MostrarNoticias);
         Preferences.Set("mostrarPeliculas", PreferenciasUsuario.MostrarPeliculas);
         Preferences.Set("mostrarPatrocinadores", PreferenciasUsuario.MostrarPatrocinadores);
      }

      public async Task<bool> LoginAsync(string alias, string clave)
      {
         var usuario = await _usuarioRepositorio.ObtenerUsuarioPorAlias(alias);
         if (usuario != null && usuario.Clave == clave)
         {
            UsuarioActual = usuario;
            return true;
         }
         return false;
      }

      public void CerrarSesion()
      {
         UsuarioActual = null;
      }

   }
}
