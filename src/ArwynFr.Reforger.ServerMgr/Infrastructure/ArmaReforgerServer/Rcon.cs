using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure.ArmaReforgerServer;

public record Rcon(
    [property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("port")] int Port,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("permission")] string Permission,
    [property: JsonPropertyName("blacklist")] IReadOnlyList<string> Blacklist,
    [property: JsonPropertyName("whitelist")] IReadOnlyList<string> Whitelist
);

