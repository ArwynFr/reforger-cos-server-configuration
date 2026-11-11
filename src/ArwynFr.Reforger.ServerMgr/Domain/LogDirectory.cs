namespace ArwynFr.Reforger.ServerMgr.Domain;

public sealed record LogDirectory(DirectoryInfo Parent, LogDirectoryName LogDirectoryName)
{
    public static LogDirectory? Factory(string? value) =>
        Path.GetDirectoryName(value) is string parent &&
        Path.GetFileName(value) is string name &&
        LogDirectoryName.Factory(name) is LogDirectoryName logDirectoryName
        ? new(new(parent), logDirectoryName) : null;
}
