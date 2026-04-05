using System.Security.Claims;

using AspNet.Security.OAuth.Discord;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ArwynFr.Reforger.ServerMgr.Infrastructure;

internal static class SecurityConfiguration
{
    public static void AddAuthentication(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(AddAuthentication)
            .AddCookie(options => builder.Configuration.Bind("Cookie", options))
            .AddDiscord(options => builder.Configuration.Bind("Discord", options));
    }

    public static void AddAuthorization(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(AddAuthorizationBuilder(builder));
    }

    internal static void AddAuthentication(AuthenticationOptions options)
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
    }

    internal static Action<AuthorizationOptions> AddAuthorizationBuilder(WebApplicationBuilder builder) => options =>
    {
        var whitelist = builder.Configuration.GetSection("AuthZ").Get<string[]>() ?? [];
        options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireClaim(ClaimTypes.NameIdentifier, whitelist).Build();
    };
}
