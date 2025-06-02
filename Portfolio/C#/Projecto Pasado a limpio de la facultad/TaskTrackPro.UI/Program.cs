using Domain.Infrastructure.Persistence;
using Domain.Utilities;
using TaskTrackPro.UI.Components;
using Domain.Aplication.Interfaces;
using Domain.Aplication.Services;
using Task = Domain.Utilities.Task;
using IList = Domain.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AlertRepositoryInMemory>();

builder.Services.AddSingleton<IList.IList<Alert>,    AlertRepositoryInMemory>();
builder.Services.AddSingleton<IList.IList<User>,     UserRepositoryInMemory>();
builder.Services.AddSingleton<IList.IList<Task>,     RepositoryInMemory<Task>>();
builder.Services.AddSingleton<IList.IList<Resource>, RepositoryInMemory<Resource>>();
builder.Services.AddSingleton<IList.IList<Project>,  RepositoryInMemory<Project>>();

builder.Services.AddScoped<IProjectManager, ProjectManager>();
builder.Services.AddScoped<IUserManager,    UserManager>();
builder.Services.AddScoped<ITaskManager,    TaskManager>();
builder.Services.AddScoped<IResourceManager,ResourceManager>();
builder.Services.AddScoped<IAlertManager,   AlertManager>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();