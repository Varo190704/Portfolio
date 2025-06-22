using Domain.Utilities;
using TaskTrackPro.UI.Components;
using Domain.Aplication.Interfaces;
using Domain.Aplication.Services;
using Domain.Infrastructure.SqlConecc;
using Task = Domain.Utilities.Task;
using Domain.Infrastructure.SqlConecc;
    
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AlertInSQL>();
builder.Services.AddSingleton<UserInSQL>();
builder.Services.AddSingleton<TaskInSQL>();
builder.Services.AddSingleton<ResourceInSQL>();
builder.Services.AddSingleton<ProjectInSQL>();

builder.Services.AddScoped<IProjectManager, ProjectManager>();
builder.Services.AddScoped<IUserManager,    UserManager>();
builder.Services.AddScoped<ITaskManager,    TaskManager>();
builder.Services.AddScoped<IResourceManager,ResourceManager>();
builder.Services.AddScoped<IAlertManager,   AlertManager>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();