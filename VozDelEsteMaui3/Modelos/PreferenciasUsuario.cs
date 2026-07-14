using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VozDelEsteMaui3.Modelos
{
   public partial class PreferenciasUsuario : ObservableObject
   {
      [ObservableProperty]
      private bool mostrarClima;

      [ObservableProperty]
      private bool mostrarCotizaciones;

      [ObservableProperty]
      private bool mostrarNoticias;

      [ObservableProperty]
      private bool mostrarPeliculas;

      [ObservableProperty]
      private bool mostrarPatrocinadores;
   }
}