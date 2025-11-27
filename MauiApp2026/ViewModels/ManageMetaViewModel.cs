using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp2026.ViewModels;

public partial class ManageMetaViewModel : ObservableObject
{
    private readonly CategoryRepository _categoryRepository;
    private readonly TagRepository _tagRepository;

    [ObservableProperty]
    private ObservableCollection<Category> categories = [];

    [ObservableProperty]
    private ObservableCollection<Tag> tags = [];

    [ObservableProperty]
    private bool isLoading = false;

    public ManageMetaViewModel(CategoryRepository categoryRepository, TagRepository tagRepository)
    {
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
    }

    [RelayCommand]
    public async Task LoadData()
    {
        try
        {
            this.IsLoading = true;
            Debug.WriteLine("ManageMetaViewModel: Starting LoadData");

            var categoryList = await _categoryRepository.ListAsync();
            Debug.WriteLine($"ManageMetaViewModel: Found {categoryList.Count} categories");
            
            var tagList = await _tagRepository.ListAsync();
            Debug.WriteLine($"ManageMetaViewModel: Found {tagList.Count} tags");

            this.Categories.Clear();
            foreach (var category in categoryList)
            {
                this.Categories.Add(category);
                Debug.WriteLine($"ManageMetaViewModel: Added category {category.Title}");
            }

            this.Tags.Clear();
            foreach (var tag in tagList)
            {
                this.Tags.Add(tag);
                Debug.WriteLine($"ManageMetaViewModel: Added tag {tag.Title}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading meta data: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        finally
        {
            this.IsLoading = false;
        }
    }
}
