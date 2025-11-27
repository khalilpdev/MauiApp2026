using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp2026;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string greeting = "Welcome to Avalonia UI!";

    public MainWindowViewModel()
    {
    }
}
