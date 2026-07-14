namespace VozDelEsteMaui3.Vistas.Modales;

public partial class DetalleNoticiaModal : ContentPage
{
	public DetalleNoticiaModal()
	{
		InitializeComponent();
	}

    private async void Cerrar_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.Navigation.PopModalAsync(true);
    }

    private void OnIrAlLink(object sender, EventArgs e)
    {

    }
}