using ArwynFr.Reforger.ServerMgr.Components;
using ArwynFr.Reforger.ServerMgr.Configuration;
using ArwynFr.Reforger.ServerMgr.Infrastructure;

Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSystemd();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
SecurityConfiguration.AddAuthentication(builder);
SecurityConfiguration.AddAuthorization(builder);
ReforgerOptions.Register(builder);

var app = builder.Build();

UseProductionMiddlewares(app);

static void UseProductionMiddlewares(WebApplication app)
{
    if (app.Environment.IsDevelopment()) { return; }
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

await app.RunAsync();
