using System.Diagnostics;
using System.Text.Json;

using ArwynFr.Reforger.ServerMgr.Configuration;
using ArwynFr.Reforger.ServerMgr.Infrastructure.ServerAdminTools;

using Microsoft.Extensions.Options;

namespace ArwynFr.Reforger.ServerMgr.Domain;

internal class ArmaReforgerProcess(IServiceProvider serviceProvider, string name)
{
    private IOptions<ReforgerOptions> Options => serviceProvider.GetRequiredService<IOptions<ReforgerOptions>>();
    private string PidFilename => Path.Join(Options.Value.BasePath, name, "server.pid");
    private string BinFilename => Path.Join(Options.Value.BasePath, name, "ArmaReforgerServer");
    private string ConfigFilename => Path.Join(Options.Value.BasePath, name, "config.json");
    private string ProfilePath => Path.Join(Options.Value.BasePath, name);
    private string StatsFilename => Path.Join(ProfilePath, "profile", "ServerAdminTools_Stats.json");
    private string PidContents => new FileInfo(PidFilename) is { Exists: true } ? File.ReadAllText(PidFilename) : string.Empty;
    private int? ProcessId => int.TryParse(PidContents, out var result) ? result : null;
    private Process? Process => Process.GetProcesses().FirstOrDefault(_ => _.Id == ProcessId);
    public bool Running => Process is { HasExited: false };

    public async Task EnsureStarted(CancellationToken cancellationToken)
    {
        if (Running) { return; }
        ProcessStartInfo processStartInfo = new(BinFilename, ["-config", ConfigFilename, "-profile", ProfilePath, "-maxFPS", "60", "-loadsessionsave", "-nothrow"]);
        var process = Process.Start(processStartInfo) ?? throw new InvalidOperationException();
        FileInfo fileInfo = new(PidFilename);
        Directory.CreateDirectory(fileInfo.Directory!.FullName);
        await File.WriteAllTextAsync(PidFilename, process.Id.ToString(), cancellationToken);
    }

    public async Task Stop(CancellationToken cancellationToken)
    {
        if (Process is not { HasExited: false }) { return; }
        Process.Kill();
    }

    public Task Wait(CancellationToken cancellationToken) => Process switch
    {
        { HasExited: false } => Process.WaitForExitAsync(cancellationToken),
        _ => Task.CompletedTask
    };

    public async Task<string[]> GetPlayers(CancellationToken cancellationToken)
    {
        FileInfo fileInfo = new(StatsFilename);
        if (!fileInfo.Exists) { return []; }
        using var stream = new FileStream(StatsFilename, FileMode.Open);
        var result = await JsonSerializer.DeserializeAsync<Stats>(stream, cancellationToken: cancellationToken);
        return result?.ConnectedPlayers.Select(_ => _.Value).OrderBy(_ => _).ToArray() ?? [];
    }
}