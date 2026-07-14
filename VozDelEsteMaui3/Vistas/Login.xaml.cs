
using VozDelEsteMaui3.ViewModels;

namespace VozDelEsteMaui3.Vistas;

public partial class Login : ContentPage
{
   public Login(LoginViewModel loginViewModel)
   {
      InitializeComponent();

      BindingContext = loginViewModel;
   }
}