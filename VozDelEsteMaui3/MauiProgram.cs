using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using VozDelEsteMaui3.Data;
using VozDelEsteMaui3.Data.Interfaces;
using VozDelEsteMaui3.Data.Repositorios;
using VozDelEsteMaui3.Orquestadores;
using VozDelEsteMaui3.Servicios;
using VozDelEsteMaui3.Servicios.Interfaces;
using VozDelEsteMaui3.ViewModels;
using VozDelEsteMaui3.Vistas;

namespace VozDelEsteMaui3
{
   public static class MauiProgram
   {
      public static MauiApp CreateMauiApp()
      {
         var builder = MauiApp.CreateBuilder();
         builder
             .UseMauiApp<App>()
             .UseMauiCommunityToolkit()


             //.UseMauiMaps()
             .ConfigureFonts(fonts =>
             {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
             });
         builder.Logging.AddDebug();

         // ViewModels
         builder.Services.AddTransient<LoginViewModel>();
         builder.Services.AddTransient<RegistroViewModel>();
         builder.Services.AddTransient<MainPageViewModel>();
         builder.Services.AddTransient<ClimaViewModel>();
         builder.Services.AddTransient<CotizacionViewModel>();
         builder.Services.AddTransient<PatrocinadorViewModel>();
         builder.Services.AddTransient<AgregarPatrocinadorViewModel>();
         builder.Services.AddTransient<EditarPatrocinadorViewModel>();
         builder.Services.AddTransient<NoticiaViewModel>();
         builder.Services.AddTransient<PeliculaViewModel>();

         // Vistas
         builder.Services.AddTransient<Login>();
         builder.Services.AddTransient<MainPage>();
         builder.Services.AddTransient<Registro>();
         builder.Services.AddTransient<Clima>();
         builder.Services.AddTransient<Cotizaciones>();
         builder.Services.AddTransient<Noticias>();
         builder.Services.AddTransient<Peliculas>();
         builder.Services.AddTransient<Patrocinadores>();
         builder.Services.AddTransient<AgregarPatrocinador>();
         builder.Services.AddTransient<EditarPatrocinador>();

         // Data
         builder.Services.AddTransient<SQLiteDbContext>();
         builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
         builder.Services.AddTransient<IClimaRepositorio, ClimaRepositorio>();
         builder.Services.AddTransient<ICotizacionRepositorio, CotizacionRepositorio>();
         builder.Services.AddTransient<IPatrocinadorRepositorio, PatrocinadorRepositorio>();

         // Servicios
         builder.Services.AddSingleton<ISesionServicio,SesionServicio>();
         builder.Services.AddSingleton<IClimaServicio,ClimaServicio>();
         builder.Services.AddSingleton<ICotizacionServicio,CotizacionServicio>();
         builder.Services.AddSingleton<INoticiaServicio,NoticiaServicio>();
         builder.Services.AddSingleton<IPeliculaServicio,PeliculaServicio>();

         // Orquestadores
         builder.Services.AddTransient<OrquestadorClima>();
         builder.Services.AddTransient<OrquestadorCotizacion>();
         return builder.Build();
      }
   }
}
