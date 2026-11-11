using System.Runtime.CompilerServices;

namespace ArwynFr.Reforger.ServerMgr;

public sealed class FileTailService
{
    private string _filename = string.Empty;
    private long _lastPosition;

    public void Initialize(string filename)
    {
        _filename = filename;
        _lastPosition = 0;
    }

    public async IAsyncEnumerable<string> ReadNewContentAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (_filename == "") { yield break; }
        await using var stream = new FileStream(
            _filename,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite,
            bufferSize: 4096,
            options: FileOptions.Asynchronous);
        using var reader = new StreamReader(stream);
        stream.Position = _lastPosition;
        while (await reader.ReadLineAsync(cancellationToken) is string line)
        {
            yield return line;
        }

        _lastPosition = stream.Position;
    }
}
