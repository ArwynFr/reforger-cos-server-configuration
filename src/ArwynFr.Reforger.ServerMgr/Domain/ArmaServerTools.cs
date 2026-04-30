using ArwynFr.Reforger.ServerMgr.Configuration;
using ArwynFr.Reforger.ServerMgr.Infrastructure.ServerAdminTools;

using Microsoft.Extensions.Options;

namespace ArwynFr.Reforger.ServerMgr.Domain;

internal class ArmaServerTools(IServiceProvider serviceProvider, string name)
{
    private readonly IOptions<ReforgerOptions> _options = serviceProvider.GetRequiredService<IOptions<ReforgerOptions>>();
    private string StatsFilename => Path.Join(_options.Value.BasePath, name, "profile", "ServerAdminTools_Stats.json");
    private string ConfigFilename => Path.Join(_options.Value.BasePath, name, "profile", "ServerAdminTools_Config.json");
    private FileInfo StatsFileInfo => new(StatsFilename);
    private FileInfo ConfigFileInfo => new(ConfigFilename);

    public async Task<Stats?> GetStatistics(CancellationToken cancellationToken) => await Stats.Read(StatsFileInfo, cancellationToken);
    public async Task<Config?> GetConfiguration(CancellationToken cancellationToken) => await Config.Read(ConfigFileInfo, cancellationToken);
    public async Task SetConfiguration(Config config, CancellationToken cancellationToken) => await config.Write(ConfigFileInfo, cancellationToken);
}
