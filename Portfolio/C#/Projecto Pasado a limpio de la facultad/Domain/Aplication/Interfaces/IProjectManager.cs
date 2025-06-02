using Domain.Utilities;
using Task = Domain.Utilities.Task;

namespace Domain.Aplication.Interfaces;

public interface IProjectManager
{
    IEnumerable<Project> getAllProjects();
    Project getProjectsByName(String name);
    void createProject(Project project);
    void deleteProject(Project project);
    void updateProject(Project project);
    void assignMember(Project project, User member);
    void assAlert(Project project, Alert alert);
    void assTaskAdmin(Project project, User admin, Task task);
    void updateTaskAdmin(Project project, User admin, Task task);
    void deleteTask(Project project,  Task task);
    void updateStatus(Project project, User admin, Task task, bool[] status);
    void assResource(Project project, User member, Task task, Resource resource);
    void deleteResource(Project project, User member, Task task, Resource resource);
    List<Task> getCriticalPath(Project project);
    void assAdmin(User admin, Project project);
    void assTaskToMember(Project project, User admin, User member, Task task);
    void deleteTaskToMember(Project project, User admin, User member, Task task);
    void assTask(Project project, Task task);
    void AlwaysUpdateTasks(List<Task> updatedTasks, List<Project> Projects);
    void AlwaysUpdateUsers(List<User> updatedUsers, List<Project> Projects);
}