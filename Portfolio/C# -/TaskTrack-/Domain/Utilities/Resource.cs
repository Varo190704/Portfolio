using Domain.Enum;
using Domain.Validators;

namespace Domain.Utilities;

public class Resource
{
    
    private string _name;
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
    public TaskType TaskType { get; set;}
    public string Description { get; set;}
    public string inCommon {get; set;}
    public List<ResourceUsed> resourceUsed { get; set;} = new List<ResourceUsed>();
    public List<ResourceUsed> ResourceUsedComp {get; set;} = new List<ResourceUsed>();
    
    public Resource(){}
   
    public Resource(string name, TaskType taskType, string description, string common)
    {
        Name = name;
        TaskType = taskType;
        Description = description;
        inCommon = common;
    }

    public void assigned(string task, string project,DateOnly startDate, DateOnly endDate)
    {
        ResourceUsed newAss = new ResourceUsed(Name, task, project, startDate, endDate);
        resourceUsed.Add(newAss);
        ResourceUsedComp.Add(newAss);
    }

    public ResourceUsed GetTaskAssigned(string task)
    {
        return resourceUsed.FirstOrDefault(x => x.Task == task);
    }

    public bool IsResourceUsed(DateOnly startDate, DateOnly endDate)
    {
        return resourceUsed.Any(r => (startDate >= r.Start && startDate <= r.End) || (endDate >= r.Start && endDate <= r.End) || (startDate <= r.Start && endDate >= r.End));
    }

    public ResourceUsed GetResourceUsed(DateOnly startDate, DateOnly endDate)
    {
        return resourceUsed.First(r => startDate >= r.Start && startDate <= r.End);
    }
}