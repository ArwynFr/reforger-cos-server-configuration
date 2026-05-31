using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure.ArmaReforgerServer;

public record A2s(
    [property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("port")] int Port
);
