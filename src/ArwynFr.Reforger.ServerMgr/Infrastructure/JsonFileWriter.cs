using System.Text.Json;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure;

internal class JsonFileWriter
{
    public static async Task<T?> Read<T>(FileInfo fileInfo, CancellationToken cancellationToken)
    {
        if (fileInfo is { Exists: false }) { return default(T); }
        using FileStream fileStream = new(fileInfo.FullName, FileMode.Open);
        return await JsonSerializer.DeserializeAsync<T>(fileStream, cancellationToken: cancellationToken);
    }

    public static async Task Write<T>(T value, FileInfo fileInfo, CancellationToken cancellationToken)
    {
        if (fileInfo.Directory is { Exists: false } parent) { parent.Create(); }
        using FileStream fileStream = new(fileInfo.FullName, FileMode.Create);
        await JsonSerializer.SerializeAsync(fileStream, value, cancellationToken: cancellationToken);
    }
}