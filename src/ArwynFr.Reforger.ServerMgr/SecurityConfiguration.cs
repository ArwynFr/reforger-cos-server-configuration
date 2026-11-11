using System.Security.Claims;

using AspNet.Security.OAuth.Discord;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ArwynFr.Reforger.ServerMgr;

internal static class SecurityConfiguration
{
    public const string ReadWritePolicyName = "ReadWrite";
    private const string DiscordAuthenticationConfiguration = "Authentication:Schemes:Discord";
    private const string AllowlistConfigurationPath = "Authorization:Allowlist";

    extension(WebApplicationBuilder webApplicationBuilder)
    {
        public void AddSecurity()
        {
            webApplicationBuilder.Services.AddAuthentication(AddAuthentication).AddCookie().AddDiscord();
            webApplicationBuilder.Services.AddOptions<DiscordAuthenticationOptions>(DiscordAuthenticationDefaults.AuthenticationScheme).BindConfiguration(DiscordAuthenticationConfiguration);
            webApplicationBuilder.Services.AddAuthorization(webApplicationBuilder.AddAuthorization);
            webApplicationBuilder.Services.AddCascadingAuthenticationState();
        }

        private string[] Whitelist => webApplicationBuilder.Configuration.GetSection(AllowlistConfigurationPath).Get<string[]>() ?? [];

        internal void AddAuthorization(AuthorizationOptions options)
        {
            options.AddPolicy(ReadWritePolicyName, webApplicationBuilder.ReadWritePolicy);
        }

        internal void ReadWritePolicy(AuthorizationPolicyBuilder authorizationPolicyBuilder)
        => authorizationPolicyBuilder.RequireClaim(ClaimTypes.NameIdentifier, webApplicationBuilder.Whitelist);
    }


    internal static void AddAuthentication(AuthenticationOptions options)
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
    }
}