using Avalonia.Controls;
using Avalonia.DeskApp.ViewModels;

namespace Avalonia.DeskApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new MainViewModel();
    }
}