namespace ArwynFr.Reforger.ServerMgr.Domain;

internal class ServerManager(IServiceProvider serviceProvider, string name) : BackgroundService
{
    public static void Register(WebApplicationBuilder builder)
    {
        RegisterInstance(builder, "cos-1");
        RegisterInstance(builder, "cos-2");
    }

    private static void RegisterInstance(WebApplicationBuilder builder, string name)
    {
        builder.Services.AddKeyedSingleton<ServerManager>(name, (services, key) => new(services, name));
        builder.Services.AddHostedService(services => services.GetRequiredKeyedService<ServerManager>(name));
    }

    public bool Enabled { get; private set; } = false;

    public bool Running => new ArmaReforgerProcess(serviceProvider, name).Running;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (Enabled && stoppingToken is { IsCancellationRequested: false })
        {
            ArmaReforgerProcess armaReforgerProcess = new(serviceProvider, name);
            await armaReforgerProcess.EnsureStarted(stoppingToken);
            await armaReforgerProcess.Wait(stoppingToken);
        }
    }

    public async Task Enable(CancellationToken cancellationToken)
    {
        try
        {
            Enabled = true;
            ArmaReforgerProcess armaReforgerProcess = new(serviceProvider, name);
            await armaReforgerProcess.EnsureStarted(cancellationToken);
            await StartAsync(cancellationToken);
        }
        catch (Exception)
        {
            Enabled = false;
            throw;
        }
    }

    public async Task Disable(CancellationToken cancellationToken)
    {
        Enabled = false;
        ArmaReforgerProcess armaReforgerProcess = new(serviceProvider, name);
        await armaReforgerProcess.Stop(cancellationToken);
        await armaReforgerProcess.Wait(cancellationToken);
    }
}