using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace MauiApp2026;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly SeedDataService _seedDataService;
    private readonly DashboardViewModel _dashboardViewModel;
    private readonly ProjectsViewModel _projectsViewModel;
    private readonly ManageMetaViewModel _manageMetaViewModel;

    [ObservableProperty]
    private string currentPage = "Dashboard";

    [ObservableProperty]
    private int selectedMenuIndex = 0;

    [ObservableProperty]
    private object? currentViewModel;

    [ObservableProperty]
    private bool isInitializing = true;

    public MainWindowViewModel(
        SeedDataService seedDataService,
        DashboardViewModel dashboardViewModel,
        ProjectsViewModel projectsViewModel,
        ManageMetaViewModel manageMetaViewModel)
    {
        _seedDataService = seedDataService;
        _dashboardViewModel = dashboardViewModel;
        _projectsViewModel = projectsViewModel;
        _manageMetaViewModel = manageMetaViewModel;

        this.CurrentViewModel = _dashboardViewModel;

        // Initialize on construction
        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        try
        {
            this.IsInitializing = true;
            Debug.WriteLine("Starting to load seed data...");

            await _seedDataService.LoadSeedDataAsync();

            Debug.WriteLine("Seed data loaded, loading dashboard...");
            await _dashboardViewModel.LoadDataCommand.ExecuteAsync(null);

            Debug.WriteLine("Dashboard loaded successfully");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error during initialization: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        finally
        {
            this.IsInitializing = false;
        }
    }

    [RelayCommand]
    public async Task NavigateToDashboard()
    {
        this.CurrentPage = "Dashboard";
        this.SelectedMenuIndex = 0;
        this.CurrentViewModel = _dashboardViewModel;
        await _dashboardViewModel.LoadDataCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    public async Task NavigateToProjects()
    {
        this.CurrentPage = "Projects";
        this.SelectedMenuIndex = 1;
        this.CurrentViewModel = _projectsViewModel;
        await _projectsViewModel.LoadDataCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    public async Task NavigateToManageMeta()
    {
        this.CurrentPage = "ManageMeta";
        this.SelectedMenuIndex = 2;
        this.CurrentViewModel = _manageMetaViewModel;
        await _manageMetaViewModel.LoadDataCommand.ExecuteAsync(null);
    }
}
