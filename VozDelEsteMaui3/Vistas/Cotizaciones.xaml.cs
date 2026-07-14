using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas;

public partial class Cotizaciones : ContentPage
{
	public Cotizaciones(CotizacionViewModel cotizacionViewModel)
	{
		InitializeComponent();
		BindingContext = cotizacionViewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is CotizacionViewModel cotizacionViewModel)
			await cotizacionViewModel.CargarDatosAsync();
   }
}