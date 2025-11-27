using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp2026.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly ProjectRepository _projectRepository;
    private readonly TaskRepository _taskRepository;
    private readonly CategoryRepository _categoryRepository;

    [ObservableProperty]
    private ObservableCollection<Project> projects = [];

    [ObservableProperty]
    private ObservableCollection<ProjectTask> tasks = [];

    [ObservableProperty]
    private int totalProjects = 0;

    [ObservableProperty]
    private int activeTasks = 0;

    [ObservableProperty]
    private int completedTasks = 0;

    [ObservableProperty]
    private bool isLoading = false;

    public DashboardViewModel(ProjectRepository projectRepository, TaskRepository taskRepository, CategoryRepository categoryRepository)
    {
        _projectRepository = projectRepository;
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
    }

    [RelayCommand]
    public async Task LoadData()
    {
        try
        {
            this.IsLoading = true;
            Debug.WriteLine("DashboardViewModel: Starting LoadData");

            var projectList = await _projectRepository.ListAsync();
            Debug.WriteLine($"DashboardViewModel: Found {projectList.Count} projects");
            
            var taskList = await _taskRepository.ListAsync();
            Debug.WriteLine($"DashboardViewModel: Found {taskList.Count} tasks");

            this.Projects.Clear();
            foreach (var project in projectList)
            {
                this.Projects.Add(project);
                Debug.WriteLine($"DashboardViewModel: Added project {project.Name}");
            }

            this.Tasks.Clear();
            foreach (var task in taskList.Take(5))
            {
                this.Tasks.Add(task);
            }

            this.TotalProjects = projectList.Count;
            this.ActiveTasks = taskList.Count(t => !t.IsCompleted);
            this.CompletedTasks = taskList.Count(t => t.IsCompleted);
            
            Debug.WriteLine($"DashboardViewModel: Stats - Total: {this.TotalProjects}, Active: {this.ActiveTasks}, Completed: {this.CompletedTasks}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading dashboard data: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        finally
        {
            this.IsLoading = false;
        }
    }
}
