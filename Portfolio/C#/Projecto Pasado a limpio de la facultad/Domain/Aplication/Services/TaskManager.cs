using Domain.Aplication.Interfaces;
using Domain.Utilities;
using Domain.Validators;
using IList = Domain.Infrastructure.Persistence;
using Task = Domain.Utilities.Task;

namespace Domain.Aplication.Services;

public class TaskManager: ITaskManager
{
    private readonly IList.IList<Task> TaskRepo;

    public TaskManager(IList.IList<Task> taskRepo)
    {
        TaskRepo = taskRepo;
    }
    
    public IEnumerable<Task> getAllTasks()
    {
        return TaskRepo.GetAll();
    }
    public Task getTasksByName(String name)
    {
        return TaskRepo.getByName(name);
    }

    public Task getTaskByName(String name)
    {
        return TaskRepo.getByName(name);
    }
    
    public void createTask(Task task)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }
        if (TaskRepo.getByName(task.Name) != null)
        {
            throw new ArgumentException("Task already exists");
        }
        TaskRepo.Add(task);
    }

    public void deleteTask(Task task)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }

        if (TaskRepo.getByName(task.Name) == null)
        {
            throw new ArgumentException("Task does not exist");
        }
        TaskRepo.Remove(task);
    }

    public void updateTask(Task task)
    {
        if (TaskRepo.getByName(task.Name) == null)
        {
            throw new ArgumentException("Task does not exist");
        }
        TaskRepo.Update(task);
    }

    public void AddDependency(Task task, Task dependency)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }
        task.AddDepen(dependency);
        TaskRepo.Update(task);
    }

    public void RemoveDependency(Task task, Task dependency)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }
        
        task.RemoveDependency(dependency);
        TaskRepo.Update(task);
    }
    
    
    public void AssResource(Task task, Resource resource)
    {
        task.AddResource(resource);
        TaskRepo.Update(task);
    }
    
    public void DeleteResource(Task task, Resource resource)
    {
        task.RemoveResource(resource);
        TaskRepo.Update(task);
    }
    
    public void RecalculateCriticalPath()
    {
        var list = TaskRepo.GetAll().ToList();
        CriticalPathCalculator.Calculate(list);
        
        foreach (Task task in list)
        {
            TaskRepo.Update(task);
        }
    }
    
    public void AlwaysUpdateTasks(List<Resource> updatedResources, List<Task> Tasks)
    {
        foreach (var task in Tasks)
        {
            task.Resources.RemoveAll(task => !updatedResources.Any(t => t.Name == task.Name));
            foreach (var res in task.Resources)
            {
                var updatedResource = updatedResources.FirstOrDefault(t => t.Name == task.Name);
                if (updatedResource != null)
                {
                    res.Description = updatedResource.Description;
                    res.TaskType = updatedResource.TaskType;
                }
            }
            TaskRepo.Update(task);
        }
    }
}