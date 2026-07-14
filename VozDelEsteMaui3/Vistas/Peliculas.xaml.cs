using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas;

public partial class Peliculas : ContentPage
{
	public Peliculas(PeliculaViewModel peliculaViewModel)
	{
		InitializeComponent();
		BindingContext = peliculaViewModel;
	}
}