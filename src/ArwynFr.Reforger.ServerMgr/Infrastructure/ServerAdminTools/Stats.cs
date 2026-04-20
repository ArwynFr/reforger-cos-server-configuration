using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure.ServerAdminTools;

public record Stats(
    [property: JsonPropertyName("registered_systems")] int? RegisteredSystems,
    [property: JsonPropertyName("registered_vehicles")] int? RegisteredVehicles,
    [property: JsonPropertyName("registered_groups")] int? RegisteredGroups,
    [property: JsonPropertyName("fps")] int? Fps,
    [property: JsonPropertyName("uptime_seconds")] int? UptimeSeconds,
    [property: JsonPropertyName("updated")] int? Updated,
    [property: JsonPropertyName("ai_characters")] int? AiCharacters,
    [property: JsonPropertyName("registered_tasks")] int? RegisteredTasks,
    [property: JsonPropertyName("registered_entities")] int? RegisteredEntities,
    [property: JsonPropertyName("players")] int? Players,
    [property: JsonPropertyName("connected_players")] IDictionary<string, string> ConnectedPlayers,
    [property: JsonPropertyName("events")] Events Events
);