
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace VozDelEsteMaui3.Modelos
{
   [Table("Cotizaciones")]
   public partial class ModeloCotizacion : ObservableObject
   {
      [PrimaryKey, AutoIncrement]
      public int Id { get; set; }

      [ObservableProperty]
      private DateTime fecha;

      [ObservableProperty]
      private float uyuEur;

      [ObservableProperty]
      private float uyuUsd;

      [ObservableProperty]
      private float uyuArs;

      [ObservableProperty]
      private float uyuBrl;

   }
}
