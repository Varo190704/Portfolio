using Domain.Enum;

namespace Domain.Utilities;

public class Task
{
    private string _name;
    private int _duration;
    public string Name
    {
        get => _name;
        set
        {
            if (Validators.Validator.stringnotnull(value))
            {
                _name = value;
            }
            else
            {
                throw new ArgumentException("Name cannot be Null");
            }
        }
        
    }

    public int Duration
    {
        get => _duration;
        set
        {
            if (value >= 1)
            {
                _duration = value;
            }
            else
            {
                throw new ArgumentException("Duration must be greater than 0");
            }
        }
        
    }
    public string Description { get; set;}
    public DateOnly StartDate { get; set;}
    public DateOnly EndDate { get; set;}
    public DateOnly LateStartDate { get; set;}
    public DateOnly LateEndDate { get; set;}
    public int Slak { get; set;}
    public TaskProgress Status { get; set;}
    public List<Task> Dependencies = new List<Task>();
    public List<Resource> Resources = new List<Resource>();
    
    public Task(){}

    public Task(string name, string description, DateOnly startDate, int duration)
    {
        Name = name;
        Description = description;
        Duration = duration;
        StartDate = startDate;
        EndDate = startDate.AddDays(duration);
        LateStartDate = startDate;
        LateEndDate = EndDate;
        Slak = 0;
        Status = TaskProgress.Pending;    
    }

    public Task(string name, string description, DateOnly startDate, int duration, TaskProgress status)
    {
        Name = name;
        Description = description;
        Duration = duration;
        StartDate = startDate;
        EndDate = startDate.AddDays(duration);
        LateStartDate = startDate;
        LateEndDate = EndDate;
        Slak = 0;
        Status = status;    
    }
    
    public Task(Task task)
    {
        Name = task.Name;
        Description = task.Description;
        Duration = task.Duration;
        StartDate = task.StartDate;
        EndDate = task.EndDate;
        LateStartDate = task.LateStartDate;
        LateEndDate = task.LateEndDate;
        Slak = task.Slak;
        Status = task.Status;
        Dependencies = new List<Task>();
        /*i've to do the same with resource (a clone constructor)*/
        Resources = task.Resources.Select(r => new Resource(r)).ToList();

    }
    
    public void AddDepen(Task dependency)
    {
        if (dependency == null)
        {
            throw new ArgumentException("Dependency cannot be null");
        }

        if (Dependencies.Contains(dependency))
        {
            throw new ArgumentException("Dependency already added");
        }

        Dependencies.Add(dependency);
    }
    
    // nuevo

    public void RemoveDependency(Task dependency)
    {
        if (dependency == null)
        {
            throw new ArgumentException("Dependency cannot be null");
        }

        if (!Dependencies.Contains(dependency))
        {
            throw new ArgumentException("Dependency already added");
        }
        
        Dependencies.Remove(dependency);
    }

    public void AddResource(Resource resource)
    {
        if (resource == null)
        {
            throw new ArgumentException("Resource cannot be null");
        }

        if (Resources.Contains(resource))
        {
            throw new ArgumentException("Resource already added");
        }
        Resources.Add(resource);
    }

    public void RemoveResource(Resource resource)
    {
        if (resource == null)
        {
            throw new ArgumentException("Resource cannot be null");
        }
        if (Resources.Contains(resource))
        {
            Resources.Remove(resource);
        }
        Resources.Remove(resource);
    }
}