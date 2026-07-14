
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;

namespace VozDelEsteMaui3.Modelos
{
   [Table("Climas")]
   public partial class ModeloClima : ObservableObject
   {
      [PrimaryKey, AutoIncrement]
      public int Id { get; set; }
      [ObservableProperty]
      private string estado = "";

      [ObservableProperty]
      private string descripcion = "";

      [ObservableProperty]
      private double temperatura;

      [ObservableProperty]
      private double temperaturaMinima;

      [ObservableProperty]
      private double temperaturaMaxima;

      [ObservableProperty]
      private long porcentajeHumedad;

      [ObservableProperty]
      private string iconoUrl = "";

      [ObservableProperty]
      private double velocidadViento;

      [ObservableProperty]
      private DateTime fecha;


   }
}
