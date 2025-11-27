namespace MauiApp2026.Data
{
    public static class Constants
    {
        public const string DatabaseFilename = "AppSQLite.db3";

        public static string DatabasePath =>
            $"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MauiApp2026", DatabaseFilename)}";
    }
}