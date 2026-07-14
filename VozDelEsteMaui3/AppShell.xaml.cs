using VozDelEsteMaui3.Vistas;

namespace VozDelEsteMaui3
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Clima), typeof(Clima));
            Routing.RegisterRoute(nameof(Cotizaciones), typeof(Cotizaciones));
            Routing.RegisterRoute(nameof(Noticias), typeof(Noticias));
            Routing.RegisterRoute(nameof(Peliculas), typeof(Peliculas));
            Routing.RegisterRoute(nameof(Patrocinadores), typeof(Patrocinadores));
            Routing.RegisterRoute(nameof(AgregarPatrocinador), typeof(AgregarPatrocinador));
            Routing.RegisterRoute(nameof(EditarPatrocinador), typeof(EditarPatrocinador));
        }
    }
}
