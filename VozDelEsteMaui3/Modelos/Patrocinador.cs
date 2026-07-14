
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace VozDelEsteMaui3.Modelos
{
   [Table("Patrocinadores")]
   public partial class Patrocinador : ObservableObject
   {
      [PrimaryKey, AutoIncrement]
      public int Id { get; set; }

      [ObservableProperty]
      [property: NotNull]
      private string nombre;

      [ObservableProperty]
      private string logoUrl = "";

      [ObservableProperty]
      private double latitud;

      [ObservableProperty]
      private double longitud;
   }
}
