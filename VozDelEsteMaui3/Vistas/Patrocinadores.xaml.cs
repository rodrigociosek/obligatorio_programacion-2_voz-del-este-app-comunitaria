using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas;

public partial class Patrocinadores : ContentPage
{
	public Patrocinadores(PatrocinadorViewModel patrocinadorViewModel)
	{
		InitializeComponent();
		BindingContext = patrocinadorViewModel;
	}

   protected override async void OnAppearing()
   {
      base.OnAppearing();
		if (BindingContext is PatrocinadorViewModel patrocinadorViewModel)
			await patrocinadorViewModel.CargarPatrocinadoresAsync();
   }
}