using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas;

public partial class Noticias : ContentPage
{
	public Noticias(NoticiaViewModel noticiaViewModel)
	{
		InitializeComponent();
		BindingContext = noticiaViewModel;
	}
}