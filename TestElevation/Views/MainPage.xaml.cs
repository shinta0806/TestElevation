using Microsoft.UI.Xaml.Controls;

using TestElevation.ViewModels;

namespace TestElevation.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
