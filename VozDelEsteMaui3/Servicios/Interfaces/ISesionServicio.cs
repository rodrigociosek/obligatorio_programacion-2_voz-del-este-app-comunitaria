
using VozDelEsteMaui3.Modelos;

namespace VozDelEsteMaui3.Servicios.Interfaces
{
   public interface ISesionServicio
   {
      bool EstaAutenticado { get; }
      Usuario UsuarioIngresado { get; }
      PreferenciasUsuario PreferenciasUsuario { get; }
      void GuardarPreferencias();
      Task<bool> LoginAsync(string alias, string clave);
      void CerrarSesion();
   }
}
