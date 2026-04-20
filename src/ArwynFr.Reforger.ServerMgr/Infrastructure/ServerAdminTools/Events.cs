using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure.ServerAdminTools;

public record Events(
    [property: JsonPropertyName("serveradmintools_game_started")] int? ServeradmintoolsGameStarted,
    [property: JsonPropertyName("serveradmintools_player_joined")] int? ServeradmintoolsPlayerJoined,
    [property: JsonPropertyName("serveradmintools_vote_ended")] int? ServeradmintoolsVoteEnded,
    [property: JsonPropertyName("serveradmintools_player_killed")] int? ServeradmintoolsPlayerKilled,
    [property: JsonPropertyName("serveradmintools_vote_started")] int? ServeradmintoolsVoteStarted
);
