namespace DeathStarLibrary;

public static class DirExtension
{
    public static string? ProjectBase()
    {
        var currDir = Directory.GetCurrentDirectory();
        var libraryDir = "DeathStarLibrary";
        var projectBase = Directory.GetParent(currDir)?.FullName;
        var baseDir = Path.Combine(projectBase!, libraryDir);
        return baseDir;
    }
}
