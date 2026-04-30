using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure.ServerAdminTools;

public record Config(
    [property: JsonPropertyName("admins")] IDictionary<Guid, string> Admins,
    [property: JsonPropertyName("bans")] IDictionary<Guid, string> Bans,
    [property: JsonPropertyName("repeatedChatMessages")] IReadOnlyList<object> RepeatedChatMessages,
    [property: JsonPropertyName("scheduledChatMessages")] IReadOnlyList<object> ScheduledChatMessages,
    [property: JsonPropertyName("serverMessage")] IReadOnlyList<string> ServerMessage,
    [property: JsonPropertyName("chatMessagesUtcTime")] bool ChatMessagesUtcTime,
    [property: JsonPropertyName("repeatedChatMessagesCycle")] bool RepeatedChatMessagesCycle,
    [property: JsonPropertyName("statsFileUpdateIntervalSeconds")] int StatsFileUpdateIntervalSeconds,
    [property: JsonPropertyName("banReloadIntervalMinutes")] int BanReloadIntervalMinutes,
    [property: JsonPropertyName("statsFileName")] string StatsFileName,
    [property: JsonPropertyName("statsSaveConnectedPlayers")] bool StatsSaveConnectedPlayers,
    [property: JsonPropertyName("eventsApiToken")] string EventsApiToken,
    [property: JsonPropertyName("eventsApiAddress")] string EventsApiAddress,
    [property: JsonPropertyName("eventsApiRatelimitSeconds")] int EventsApiRatelimitSeconds,
    [property: JsonPropertyName("serverMessageHeaderImage")] string ServerMessageHeaderImage,
    [property: JsonPropertyName("serverMessageDiscordLink")] string ServerMessageDiscordLink,
    [property: JsonPropertyName("serverMessageOpen")] bool ServerMessageOpen,
    [property: JsonPropertyName("eventsApiEventsEnabled")] IReadOnlyList<string> EventsApiEventsEnabled
)
{
    public static Task<Config?> Read(FileInfo fileInfo, CancellationToken cancellationToken) => JsonFileWriter.Read<Config>(fileInfo, cancellationToken);
    public Task Write(FileInfo fileInfo, CancellationToken cancellationToken) => JsonFileWriter.Write(this, fileInfo, cancellationToken);
}
