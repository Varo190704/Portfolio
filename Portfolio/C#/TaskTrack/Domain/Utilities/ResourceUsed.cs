namespace Domain.Validators;

public class ResourceUsed
{
    public string Resource { get; set; }
    public string Task{get;set;}
    public string Project{get;set;}
    public DateOnly Start{get;set;}
    public DateOnly End{get;set;}

    public ResourceUsed(string resource, string task, string project, DateOnly start, DateOnly end)
    {
        Resource = resource;
        Task = task;
        Project = project;
        Start = start;
        End = end;
    }
    
}