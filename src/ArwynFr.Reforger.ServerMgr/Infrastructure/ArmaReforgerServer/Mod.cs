using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure.ArmaReforgerServer;

public record Mod(
    [property: JsonPropertyName("modId")] string ModId,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("version")] string Version
);

