using Domain.Enum;
using Domain.Validators;

namespace Domain.Utilities;

public class Project
{
    private string _name;
    private string _description;
    private DateOnly _startDate;
    private DateOnly _endDate;
    private User _admin;

    public string Name
    {
        get => _name;
        set
        {
            if (Validator.stringnotnull(value))
            {
                _name = value;
            }
            else
            {
                throw new ArgumentException("Name cannot be Null");
            }
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (Validator.invalidDescrip(value))
            {
                _description = value;
            }
            else
            {
                throw new ArgumentException("Description cannot be Null");
            }
        }

    }

    public DateOnly StartDate
    {
        get => _startDate;
        set
        {
            if (Equals(value, null))
            {
                throw new ArgumentException("Invalid StartDate");
            }
            _startDate = value;
        }
    }

    public DateOnly EndDate
    {
        get => _endDate;
        set
        {
            if (Equals(value, null))
            {
                throw new ArgumentException("Invalid EndDate");
            }
            if (value < StartDate)
                throw new ArgumentException("EndDate cannot be before StartDate");

            _endDate = value;
        }

    }

    public User Admin
    {
        get => _admin;
        set
        {
            if (!Equals(value, null))
            {
                _admin = value;
            }
            else
            {
                _admin = null;
            }
        }

    }

    public int Duration { get; set; }
    public List<Task> Tasks = new List<Task>();
    public List<User> Members = new List<User>();
    public List<Alert> Alerts = new List<Alert>();

    public Project()
    {
    }

    public Project(string name, string description, DateOnly startDate)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        /*nuevo*/
        EndDate = startDate.AddDays(1);
    }

    public Project(string name, string description, User admin, DateOnly startDate)
    {
        Name = name;
        Description = description;
        Admin = admin;
        Members.Add(admin);
        admin.projects.Add(this);
        admin.Types.Add(UserType.ProjectAdmin);
        StartDate = startDate;
        EndDate = startDate.AddDays(1);
    }

    public void assMember(User user)
    {
        if (!Equals(user, null))
        {
            Members.Add(user);
            user.projects.Add(this);
            user.Types.Add(UserType.ProjectMember);
        }
        else
        {
            throw new ArgumentException("Invalid Member");
        }
    }

    public void assAlert(Alert alert)
    {
        if (!Equals(alert, null))
        {
            Alerts.Add(alert);
        }
        else
        {
            throw new ArgumentException("Invalid Member");
        }
    }
//
    public void assTaskAdmin(User user, Task task)
    {
        if (Equals(user, null))
        {
            throw new ArgumentException("Admin cannot be null");
        }

        if (!Equals(user, Admin))
        {
            throw new ArgumentException("Only Admin can add tasks to a project");
        }

        assTask(task);
    }

    public void assTask(Task task)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }
            
        if (Tasks.Contains(task))
        {
            throw new ArgumentException("Task already added");
        }
        List<Task> notListed = task.Dependencies.Where(d => !Tasks.Any(t => t.Name == d.Name)).ToList();
        foreach (Task dep in notListed)
        {
            if (!Tasks.Any(t => t.Name == dep.Name))
            {
                assTask(dep);
            }
        }

        Task pTask = new Task(task);
        pTask.Dependencies = task.Dependencies.Select(d => Tasks.First(t => t.Name == d.Name)).ToList();
        pTask.Resources = task.Resources.Select(r => new Resource(r.Name, r.TaskType, r.Description)).ToList();
        Tasks.Add(pTask);
        refreshTimes();
        
    }

    public void updateTaskAdmin(User user, Task task)
    {
        if (Equals(user, null))
        {
            throw new ArgumentException("Admin cannot be null");
        }

        if (!Equals(user, Admin))
        {
            throw new ArgumentException("Only Admin can update tasks to a project");
        }

        updateTask(task);
    }

    private void updateTask(Task task)
    {
        if (!Equals(task, null))
        {
            bool boolean = false;
            foreach (Task t in Tasks)
            {
                if (t.Name == task.Name)
                {
                    t.Duration = task.Duration;
                    t.Description = task.Description;
                    t.StartDate = task.StartDate;
                    t.EndDate = task.EndDate;
                    t.LateStartDate = task.LateStartDate;
                    t.LateEndDate = task.LateEndDate;
                    t.Slak = task.Slak;
                    t.Status = task.Status;
                    t.Dependencies = task.Dependencies;
                    t.Resources = task.Resources;
                    boolean = true;
                }
            }

            refreshTimes();
            if (!boolean)
            {
                throw new ArgumentException("Task is not in the project");
            }
        }
        else
        {
            throw new ArgumentException("Invalid Task");
        }
    }

    public void deleteTaskAdmin(User user, Task task)
    {
        if (Equals(user, null))
        {
            throw new ArgumentException("Admin cannot be null");
        }

        if (!Equals(user, Admin))
        {
            throw new ArgumentException("Only Admin can delete tasks to a project");
        }

        deleteTask(task);
    }

    public void deleteTask(Task task)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }

        if (!Tasks.Contains(task))
        {
            throw new ArgumentException("Task is not in the project");
        }

        foreach (Task t in Tasks)
        {
            if (t.Dependencies.Contains(task))
            {
                t.Dependencies.Remove(task);
            }
        }

        Tasks.Remove(task);
        refreshTimes();
    }

    public void updateStatusAdmin(bool[] status, User user, Task task)
    {
        if (Equals(user, null))
        {
            throw new ArgumentException("Admin cannot be null");
        }

        updateStatus(task, status);
    }

    private void updateStatus(Task task, bool[] status)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }

        if (!Tasks.Contains(task))
        {
            throw new ArgumentException("Task is not in the project");
        }

        if (status.Length != 3)
        {
            throw new ArgumentException("Invalid Status Array");
        }

        foreach (Task t in Tasks)
        {
            if (Equals(t, task))
            {
                if (status[0])
                {
                    t.Status = TaskProgress.Pending;
                }

                if (status[1])
                {
                    t.Status = TaskProgress.InProgress;
                }

                if (status[2])
                {
                    t.Status = TaskProgress.Completed;
                }
            }
        }
    }

    public void assResourceAdmin(User user, Task task, Resource resource)
    {
        if (Equals(user, null))
        {
            throw new ArgumentException("Admin cannot be null");
        }

        if (!Equals(user, Admin))
        {
            throw new ArgumentException("Only Admin can delete tasks to a project");
        }
        assResource(task, resource);
    }

    private void assResource(Task task, Resource resource)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }

        if (Equals(resource, null))
        {
            throw new ArgumentException("Invalid Resource");
        }

        if (!Tasks.Contains(task))
        {
            throw new ArgumentException("Task is not in the project");
        }

        if (task.Resources.Contains(resource))
        {
            throw new ArgumentException("Resource already added");
        }
        
        foreach (Task t in Tasks)
        {
            if (Equals(t, task))
            {
                task.Resources.Add(resource);
            }
        }

    }

    public void deleteResourceAdmin(User user, Task task, Resource resource)
    {
        if (Equals(user, null))
        {
            throw new ArgumentException("Admin cannot be null");
        }

        if (!Equals(user, Admin))
        {
            throw new ArgumentException("Only Admin can delete tasks to a project");
        }
        deleteResource(task, resource);
    }

    private void deleteResource(Task task, Resource resource)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }
        
        if (Equals(resource, null))
        {
            throw new ArgumentException("Invalid Resource");
        }

        if (!Tasks.Contains(task))
        {
            throw new ArgumentException("Task is not in the project");
        }

        if (!task.Resources.Contains(resource))
        {
            throw new ArgumentException("Resource is not in the task");
        }
        
        foreach (Task t in Tasks)
        {
            if (Equals(t, task))
            {
                task.Resources.Remove(resource);
            }
        }
    }

    public List<Task> getCriticalPath()
    {
        refreshTimes();
        return Tasks.Where(t=>t.Slak == 0).ToList();
    }
    
    public void assAdmin(User user)
    {
        if (!Equals(user, null))
        {
            Admin = user;
            Members.Add(user);
            user.projects.Add(this);
            user.Types.Add(UserType.ProjectAdmin);
        }
        else
        {
            throw new ArgumentException("Invalid Admin");
        }
    }

    public void assTaskToMemberAdmin(User admin, User user, Task task)
    {
        if (Equals(admin, null))
        {
            throw new ArgumentException("Admin cannot be null");
        }
        
        if (Equals(user, null))
        {
            throw new ArgumentException("User cannot be null");
        }

        if (!Equals(admin, Admin))
        {
            throw new ArgumentException("Only Admin can add tasks to a project");
        }
        
        assTaskToMember(user, task);
    }

    private void assTaskToMember(User user, Task task)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }
        if (!Members.Contains(user))
        {
            throw new ArgumentException("User is not a member of the project");
        }
        if (user.tasks.Contains(task))
        {
            throw new ArgumentException("Task already assigned to user");
        }
        if (Tasks.Contains(task))
        {
            user.tasks.Add(task);
        }
        else
        {
            Tasks.Add(task);
            user.tasks.Add(task);
        }
        refreshTimes();    
    }

    public void deleteTaskToMemberAdmin(User admin, User user, Task task)
    {
        if (Equals(admin, null))
        {
            throw new ArgumentException("Admin cannot be null");
        }

        if (Equals(user, null))
        {
            throw new ArgumentException("User cannot be null");
        }

        if (!Equals(admin, Admin))
        {
            throw new ArgumentException("Only Admin can delete tasks to a project");
        }

        deleteTaskOnMember(user, task);
    }

    private void deleteTaskOnMember(User user,Task task)
    {
        if (Equals(task, null))
        {
            throw new ArgumentException("Invalid Task");
        }

        if (!Tasks.Contains(task))
        {
            throw new ArgumentException("Task is not in the project");
        }

        if (!user.tasks.Contains(task))
        {
            throw new ArgumentException("Task is not assigned to user");
        }
        
        foreach (Task t in Tasks)
        {
            if (t.Dependencies.Contains(task))
            {
                t.Dependencies.Remove(task);
            }
        }

        user.tasks.Remove(task);
        refreshTimes();
    }
    
    //i put this "CriticalPathCalculator.Calculate(Tasks);" here bc the first
    public void refreshTimes()
    {
        if (Tasks.Count != 0)
        {
            CriticalPathCalculator.Calculate(Tasks);
            GetProjectStartDate();
            GetProjectEndDate();
            DurationProject();    
        }
    }
    
    //no idea how to make this work on tests
    private void GetProjectStartDate()
    {
        StartDate = Tasks.Min(t => t.StartDate);
    }
    
    //same
    private void GetProjectEndDate()
    {
        EndDate = Tasks.Max(t => t.EndDate);
    }
    
    private void DurationProject()
    {
        Duration = EndDate.DayNumber - StartDate.DayNumber;
    }
}