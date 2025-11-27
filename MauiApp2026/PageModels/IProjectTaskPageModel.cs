using CommunityToolkit.Mvvm.Input;
using MauiApp2026.Models;

namespace MauiApp2026.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}