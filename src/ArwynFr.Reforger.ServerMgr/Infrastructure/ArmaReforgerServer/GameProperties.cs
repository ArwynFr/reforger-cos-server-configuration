using System.Text.Json.Serialization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure.ArmaReforgerServer;

public record GameProperties(
    [property: JsonPropertyName("serverMaxViewDistance")] int ServerMaxViewDistance,
    [property: JsonPropertyName("serverMinGrassDistance")] int ServerMinGrassDistance,
    [property: JsonPropertyName("networkViewDistance")] int NetworkViewDistance,
    [property: JsonPropertyName("disableThirdPerson")] bool DisableThirdPerson,
    [property: JsonPropertyName("fastValidation")] bool FastValidation,
    [property: JsonPropertyName("battlEye")] bool BattlEye,
    [property: JsonPropertyName("VONDisableUI")] bool VONDisableUI,
    [property: JsonPropertyName("VONDisableDirectSpeechUI")] bool VONDisableDirectSpeechUI,
    [property: JsonPropertyName("VONCanTransmitCrossFaction")] bool VONCanTransmitCrossFaction,
    [property: JsonPropertyName("missionHeader")] dynamic MissionHeader
);

