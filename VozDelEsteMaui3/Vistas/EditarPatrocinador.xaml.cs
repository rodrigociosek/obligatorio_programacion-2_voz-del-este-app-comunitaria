using System.Globalization;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Modelos;
using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas;

[QueryProperty(nameof(PatrocinadorId), "Id")]
public partial class EditarPatrocinador : ContentPage
{
    private readonly IPatrocinadorRepositorio _patrocinadorRepositorio;
    public int PatrocinadorId { get; set; }

    public EditarPatrocinador(IPatrocinadorRepositorio patrocinadorRepositorio, EditarPatrocinadorViewModel editarPatrocinadorViewModel)
    {
        _patrocinadorRepositorio = patrocinadorRepositorio;
        InitializeComponent();
        BindingContext = editarPatrocinadorViewModel;

        LeafletWebView.Navigating += (s, e) =>
        {
            if (e.Url.StartsWith("leaflet://location"))
            {
                e.Cancel = true;
                var query = System.Web.HttpUtility.ParseQueryString(new Uri(e.Url).Query);
                double lat = double.Parse(query["lat"], CultureInfo.InvariantCulture);
                double lng = double.Parse(query["lng"], CultureInfo.InvariantCulture);

                editarPatrocinadorViewModel.ActualizarUbicacion(lat, lng);
            }
        };


    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var vm = (EditarPatrocinadorViewModel)BindingContext;
        vm.Id = PatrocinadorId;
        await vm.CargarPatrocinador();

        var html = new LeafletCreadorMapa().BuildMapHtml(
        LeafletMapMode.Editar,
        lat: vm.Patrocinador.Latitud,
        lng: vm.Patrocinador.Longitud,
        popupTexto: vm.Patrocinador.Nombre ?? "Ubicación"
    );

        LeafletWebView.Source = new HtmlWebViewSource { Html = html };

    }

}