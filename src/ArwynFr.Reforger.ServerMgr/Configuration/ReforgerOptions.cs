using System.ComponentModel.DataAnnotations;

namespace ArwynFr.Reforger.ServerMgr.Configuration;

internal record class ReforgerOptions
{
    private const string ConfigurationSectionName = "Reforger";

    [Required]
    public string? BasePath { get; set; }

    public static void Register(WebApplicationBuilder builder)
    => builder.Services.AddOptions<ReforgerOptions>().BindConfiguration(ConfigurationSectionName);
}
