using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure.ArmaReforgerServer;

public record Config(
    [property: JsonPropertyName("bindAddress")] string BindAddress,
    [property: JsonPropertyName("bindPort")] int BindPort,
    [property: JsonPropertyName("publicAddress")] string PublicAddress,
    [property: JsonPropertyName("publicPort")] int PublicPort,
    [property: JsonPropertyName("a2s")] A2s A2s,
    [property: JsonPropertyName("rcon")] Rcon Rcon,
    [property: JsonPropertyName("game")] Game Game
);

