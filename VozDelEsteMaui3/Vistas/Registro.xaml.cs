using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas;

public partial class Registro : ContentPage
{
	public Registro(RegistroViewModel registroViewModel)
	{
		InitializeComponent();
		BindingContext = registroViewModel;
	}
}