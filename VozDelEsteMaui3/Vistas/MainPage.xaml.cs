using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas
{
   public partial class MainPage : ContentPage
   {
      public MainPage(MainPageViewModel mainPageViewModel)
      {
         InitializeComponent();
         BindingContext = mainPageViewModel;
      }

   }
}
