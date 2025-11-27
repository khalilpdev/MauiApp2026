using MauiApp2026.Models;
using MauiApp2026.PageModels;

namespace MauiApp2026.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}