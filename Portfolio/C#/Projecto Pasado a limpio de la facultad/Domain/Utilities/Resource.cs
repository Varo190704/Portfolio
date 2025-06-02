using Domain.Enum;

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
    
    public Resource(){}

    public Resource(string name, TaskType taskType, string description)
    {
        Name = name;
        TaskType = taskType;
        Description = description;
    }

    public Resource(Resource resource)
    {
        Name = resource.Name;
        TaskType = resource.TaskType;
        Description = resource.Description;
    }
}