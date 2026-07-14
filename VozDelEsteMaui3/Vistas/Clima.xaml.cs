using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas;

public partial class Clima : ContentPage
{
   public Clima(ClimaViewModel climaViewModel)
   {
      InitializeComponent();
      BindingContext = climaViewModel;
   }

   protected override async void OnAppearing()
   {
      base.OnAppearing();
      if (BindingContext is ClimaViewModel climaViewModel)
         await climaViewModel.CargarDatosAsync();
   }
}