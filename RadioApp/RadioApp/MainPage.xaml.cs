using RadioApp.ViewModels;

namespace RadioApp;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();

        BindingContext = new RadioViewModel();
    }
}

