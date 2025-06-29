using Domain.Aplication.Interfaces;
using Domain.Infrastructure.SqlConecc;
using Domain.Utilities;
using Task = Domain.Utilities.Task;

namespace Domain.Aplication.Services;

public class ProjectManager: IProjectManager
{
    private readonly ProjectInSQL ProjectRepo;

    public ProjectManager(ProjectInSQL projectRepo)
    {
        ProjectRepo = projectRepo;
    }

    public IEnumerable<Project> getAllProjects()
    {
        return ProjectRepo.GetAll();
    }

    public Project getProjectsByName(String name)
    {
        return ProjectRepo.getByName(name);
    }

    public void createProject(Project project)
    {
        if (Equals(project, null))
        {
            throw new ArgumentException("Invalid Project");
        }

        if (ProjectRepo.getByName(project.Name) != null)
        {
            throw new ArgumentException("Project already exists");
        }
        ProjectRepo.Add(project);
    }
    
    public void deleteProject(Project project)
    {
        if (ProjectRepo.getByName(project.Name) == null)
        {
            throw new ArgumentException("Project does not exist");
        }
        ProjectRepo.Remove(project);
    }
    
    public void updateProject(Project project)
    {
        ProjectRepo.Update(project);
    }

    public void assignMember(Project project, User member)
    {
        project.assMember(member);
        ProjectRepo.assMember(project, member);
    }

    public void assLider(Project project, User alert)
    {
        ProjectRepo.assLider(project, alert);
    }
    
    public void assAdmin(Project project, User alert)
    {
        ProjectRepo.assAdmin(project, alert);
    }
    
    public void assAlert(Project project, Alert alert)
    {
        project.assAlert(alert);
        ProjectRepo.Update(project);
    }

    public void assTaskAdmin(Project project, User admin, Task task)
    {
        project.assTaskAdmin(admin, task);
        ProjectRepo.Update(project);
    }

    public void assTask(Project project, Task task)
    {
        project.assTask(task);
        ProjectRepo.assTask(project, task);
        ProjectRepo.Update(project);
    }

    public void updateTaskAdmin(Project project, User admin, Task task)
    {
        project.updateTaskAdmin(admin, task);
        ProjectRepo.Update(project);
    }

    public void deleteTask(Project project, Task task)
    {
        ProjectRepo.RemoveTP(project, task);
    }
    
    public void deleteUser(Project project, User task)
    {
        ProjectRepo.RemoveUP(project, task);
    }

    public void updateStatus(Project project, User admin, Task task, bool[] status)
    {
        project.updateStatusAdmin(status, admin, task);
        ProjectRepo.Update(project);
    }

    public void assResource(Project project, User member, Task task, Resource resource)
    {
        project.assResourceAdmin(member, task, resource);
        ProjectRepo.Update(project);
    }
    
    public void deleteResource(Project project, User member, Task task, Resource resource)
    {
        project.deleteResourceAdmin(member, task, resource);
        ProjectRepo.Update(project);
    }
    
    public List<Task> getCriticalPath(Project project)
    {
        return project.getCriticalPath();
    }

    public void assAdmin(User admin, Project project)
    {
        project.assAdmin(admin);
        ProjectRepo.Update(project);
    }

    public void assTaskToMember(Project project, User admin, User member, Task task)
    {
        ProjectRepo.assTaskMember(project, task, member);
    }

    public void deleteTaskToMember(Project project, User admin, User member, Task task)
    {
        project.deleteTaskToMemberAdmin(admin, member, task);
        ProjectRepo.Update(project);
    }
    
    public void AlwaysUpdateTasks(List<Task> updatedTasks, List<Project> Projects)
    {
        foreach (var project in Projects)
        {
            project.Tasks.RemoveAll(task => !updatedTasks.Any(t => t.Name == task.Name));
            ProjectRepo.Update(project);
        }
    }
    
    public void AlwaysUpdateUsers(List<User> updatedUsers, List<Project> Projects)
    {
        foreach (var project in Projects)
        {
            project.Members.RemoveAll(task => !updatedUsers.Any(t => t.Email == task.Email));
            foreach (var user in project.Members)
            {
                var updatedTask = updatedUsers.FirstOrDefault(t => t.Email == user.Email);
                if (updatedTask != null)
                {
                    user.Name = updatedTask.Name;
                    user.Surname = updatedTask.Surname;
                    user.AdminSistem = updatedTask.AdminSistem;
                    user.login = updatedTask.login;
                    user.BirthDate = updatedTask.BirthDate;
                    user.Password = updatedTask.Password;
                    user.Types = updatedTask.Types;
                    user.projects = updatedTask.projects;
                }
            }
            ProjectRepo.Update(project);
        }
    }
}
