using Avalonia.Controls;
using Avalonia.DeskApp.ViewModels;

namespace Avalonia.DeskApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = App.Current.Services.GetService (typeof(MainViewModel));
    }
}