using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure.ArmaReforgerServer;

public record Game(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("passwordAdmin")] string PasswordAdmin,
    [property: JsonPropertyName("crossPlatform")] bool CrossPlatform,
    [property: JsonPropertyName("admins")] IReadOnlyList<string> Admins,
    [property: JsonPropertyName("scenarioId")] string ScenarioId,
    [property: JsonPropertyName("maxPlayers")] int MaxPlayers,
    [property: JsonPropertyName("visible")] bool Visible,
    [property: JsonPropertyName("gameProperties")] GameProperties GameProperties,
    [property: JsonPropertyName("mods")] IReadOnlyList<Mod> Mods
);

