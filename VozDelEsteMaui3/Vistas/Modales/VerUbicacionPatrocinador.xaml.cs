using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas.Modales;

public partial class VerUbicacionPatrocinador : ContentPage
{
    private readonly IPatrocinadorRepositorio _patrocinadorRepositorio;

    public Patrocinador Patrocinador { get; set; }
    public VerUbicacionPatrocinador(IPatrocinadorRepositorio patrocinadorRepositorio)
    {
        _patrocinadorRepositorio = patrocinadorRepositorio;
        InitializeComponent();

        LeafletWebView.Navigating += (s, e) =>
        {
            if (e.Url.StartsWith("leaflet://location"))
            {
                e.Cancel = true;
                var query = System.Web.HttpUtility.ParseQueryString(new Uri(e.Url).Query);
                double lat = double.Parse(query["lat"], CultureInfo.InvariantCulture);
                double lng = double.Parse(query["lng"], CultureInfo.InvariantCulture);
            }
        };

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var html = new LeafletCreadorMapa().BuildMapHtml(
        LeafletMapMode.Visualizar,
        lat: Patrocinador.Latitud,
        lng: Patrocinador.Longitud,
        popupTexto: Patrocinador.Nombre ?? "Ubicación"
        );

        LeafletWebView.Source = new HtmlWebViewSource { Html = html };
    }

}