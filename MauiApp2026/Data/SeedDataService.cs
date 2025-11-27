using System.Text.Json;
using MauiApp2026.Models;
using Microsoft.Extensions.Logging;

namespace MauiApp2026.Data
{
    public class SeedDataService
    {
        private readonly ProjectRepository _projectRepository;
        private readonly TaskRepository _taskRepository;
        private readonly TagRepository _tagRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly string _seedDataFilePath = "SeedData.json";
        private readonly ILogger<SeedDataService> _logger;

        public SeedDataService(ProjectRepository projectRepository, TaskRepository taskRepository, TagRepository tagRepository, CategoryRepository categoryRepository, ILogger<SeedDataService> logger)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task LoadSeedDataAsync()
        {
            await ClearTablesAsync();

            ProjectsJson? payload = null;
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, _seedDataFilePath);
                _logger.LogInformation($"Looking for seed data at: {filePath}");
                
                if (File.Exists(filePath))
                {
                    _logger.LogInformation("Seed data file found, deserializing...");
                    await using var stream = File.OpenRead(filePath);
                    payload = JsonSerializer.Deserialize(stream, JsonContext.Default.ProjectsJson);
                    _logger.LogInformation($"Deserialized {payload?.Projects.Count ?? 0} projects");
                }
                else
                {
                    _logger.LogWarning($"Seed data file not found at: {filePath}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing seed data");
            }

            try
            {
                if (payload is not null && payload.Projects.Count > 0)
                {
                    foreach (var project in payload.Projects)
                    {
                        if (project is null)
                        {
                            continue;
                        }

                        if (project.Category is not null)
                        {
                            await _categoryRepository.SaveItemAsync(project.Category);
                            project.CategoryID = project.Category.ID;
                        }

                        await _projectRepository.SaveItemAsync(project);
                        _logger.LogInformation($"Saved project: {project.Name}");

                        if (project?.Tasks is not null)
                        {
                            foreach (var task in project.Tasks)
                            {
                                task.ProjectID = project.ID;
                                await _taskRepository.SaveItemAsync(task);
                            }
                        }

                        if (project?.Tags is not null)
                        {
                            foreach (var tag in project.Tags)
                            {
                                await _tagRepository.SaveItemAsync(tag, project.ID);
                            }
                        }
                    }
                    _logger.LogInformation("Seed data loaded successfully");
                }
                else
                {
                    _logger.LogWarning("No projects found in seed data");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error saving seed data");
                throw;
            }
        }

        private async Task ClearTablesAsync()
        {
            try
            {
                await Task.WhenAll(
                    _projectRepository.DropTableAsync(),
                    _taskRepository.DropTableAsync(),
                    _tagRepository.DropTableAsync(),
                    _categoryRepository.DropTableAsync());
                _logger.LogInformation("Tables cleared");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error clearing tables");
            }
        }
    }
}