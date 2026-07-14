using Microsoft.Maui.Controls;
using System.Globalization;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas;

public partial class AgregarPatrocinador : ContentPage
{
    private readonly AgregarPatrocinadorViewModel _agregarPatrocinadorViewModel;

    public AgregarPatrocinador(AgregarPatrocinadorViewModel agregarPatrocinadorViewModel)
    {
        _agregarPatrocinadorViewModel = agregarPatrocinadorViewModel;

        InitializeComponent();
        BindingContext = _agregarPatrocinadorViewModel;
        var html = new LeafletCreadorMapa().BuildMapHtml(
            LeafletMapMode.Seleccionar,
            popupTexto: "Ubicación actual"
        );
        LeafletWebView.Source = new HtmlWebViewSource { Html = html };

        LeafletWebView.Navigating += (s, e) =>
        {
            if (e.Url.StartsWith("leaflet://location"))
            {
                e.Cancel = true;
                var query = System.Web.HttpUtility.ParseQueryString(new Uri(e.Url).Query);
                var querylat = query["lat"];
                var querylng = query["lng"];
                double lat = double.Parse(querylat, CultureInfo.InvariantCulture);
                double lng = double.Parse(querylng, CultureInfo.InvariantCulture);

                _agregarPatrocinadorViewModel.GuardarUbicacionPatrocinador(lat, lng);
            }
        };

    }


}