namespace VozDelEsteMaui3.Vistas.Modales;

public partial class DetallePeliculaModal : ContentPage
{

    public DetallePeliculaModal()
	{
		InitializeComponent();

    }

    private async void Cerrar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopModalAsync(true);
    }
}