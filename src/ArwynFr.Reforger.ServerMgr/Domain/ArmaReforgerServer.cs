using System.Diagnostics;

using ArwynFr.Reforger.ServerMgr.Configuration;

using Microsoft.Extensions.Options;

namespace ArwynFr.Reforger.ServerMgr.Domain;

internal class ArmaReforgerServer(IServiceProvider serviceProvider, string name)
{
    public bool IsRunning => Process is { HasExited: false };
    private string PidFilename => Path.Join(_options.Value.BasePath, name, "serveur.pid");
    private string BinFilename => Path.Join(_options.Value.BasePath, name, "ArmaReforgerServer");
    private string ConfFilename => Path.Join(_options.Value.BasePath, name, "config.json");
    private string ProfilePath => Path.Join(_options.Value.BasePath, name);
    private string[] BinArguments => ["-config", ConfFilename, "-profile", ProfilePath, "-maxFPS", "60", "-loadsessionsave", "-nothrow", "-rplEncodeAsLongJobs", "-addonsRepair"];
    private string[] SteamCmdArguments => ["+force_install_dir", ProfilePath, "+login", "anonymous", "+app_update", "1874900", "validate", "+quit"];
    private int? Pid => File.Exists(PidFilename) ? int.Parse(File.ReadAllText(PidFilename)) : null;
    private Process? Process => Pid is int ? Process.GetProcessById(Pid.Value) : null;
    private readonly ILogger _logger = serviceProvider.GetRequiredService<ILogger<ArmaReforgerServer>>();
    private readonly IOptions<ReforgerOptions> _options = serviceProvider.GetRequiredService<IOptions<ReforgerOptions>>();


    public async Task Start(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Spawning process");
        ProcessStartInfo processStartInfo = new(BinFilename, BinArguments)
        {
            RedirectStandardError = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };

        var process = Process.Start(processStartInfo) ?? throw new InvalidOperationException("Server process failed to start");
        await File.WriteAllTextAsync(PidFilename, process.Id.ToString(), cancellationToken);
    }

    public async Task Upgrade(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Running steamcmd");
        ProcessStartInfo processStartInfo = new("steamcmd.sh", SteamCmdArguments)
        {
            RedirectStandardError = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };

        var process = Process.Start(processStartInfo) ?? throw new InvalidOperationException("Steamcmd process failed to start");
        await process.WaitForExitAsync(cancellationToken);
    }

    public async Task Stop(CancellationToken cancellationToken)
    {
        if (Process is null) { return; }
        Process.Kill();
        await Process.WaitForExitAsync(cancellationToken);
    }
}
