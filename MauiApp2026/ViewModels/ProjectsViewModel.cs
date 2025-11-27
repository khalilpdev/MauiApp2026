using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp2026.ViewModels;

public partial class ProjectsViewModel : ObservableObject
{
    private readonly ProjectRepository _projectRepository;

    [ObservableProperty]
    private ObservableCollection<Project> projects = [];

    [ObservableProperty]
    private bool isLoading = false;

    public ProjectsViewModel(ProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [RelayCommand]
    public async Task LoadData()
    {
        try
        {
            this.IsLoading = true;
            Debug.WriteLine("ProjectsViewModel: Starting LoadData");

            var projectList = await _projectRepository.ListAsync();
            Debug.WriteLine($"ProjectsViewModel: Found {projectList.Count} projects");

            this.Projects.Clear();
            foreach (var project in projectList)
            {
                this.Projects.Add(project);
                Debug.WriteLine($"ProjectsViewModel: Added project {project.Name}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading projects: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        finally
        {
            this.IsLoading = false;
        }
    }
}
