using Avalonia.Data.Converters;
using System.Globalization;
using MauiApp2026.Views;

namespace MauiApp2026.Views;

public class PageConverter : IMultiValueConverter
{
    public static PageConverter Instance { get; } = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count > 0 && values[0] is string pageName)
        {
            object? view = pageName switch
            {
                "Dashboard" => new DashboardView(),
                "Projects" => new ProjectsView(),
                "ManageMeta" => new ManageMetaView(),
                _ => new DashboardView(),
            };
            return view;
        }

        return new DashboardView();
    }
}
