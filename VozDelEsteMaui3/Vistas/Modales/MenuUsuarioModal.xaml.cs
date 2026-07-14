using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas.Modales;

public partial class MenuUsuarioModal : ContentPage
{
	public MenuUsuarioModal()
	{
		InitializeComponent();
	}

    private void OnSwitchToggled(object sender, ToggledEventArgs e)
    {
		if (Application.Current.UserAppTheme == AppTheme.Dark) Application.Current.UserAppTheme = AppTheme.Light;

		else Application.Current.UserAppTheme = AppTheme.Dark;
    }
}