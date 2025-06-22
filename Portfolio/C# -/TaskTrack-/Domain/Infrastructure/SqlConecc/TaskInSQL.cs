using Domain.Enum;
using Domain.Utilities;
using Domain.Validators;
using Microsoft.Data.SqlClient;
using Task = Domain.Utilities.Task;

namespace Domain.Infrastructure.SqlConecc;

public class TaskInSQL
{
    private readonly string connectionString = "Server=localhost,1433;Database=TaskTrackPro;User Id=sa;Password=Passw1rd;TrustServerCertificate=True;";

    public List<Task> GetAll()
    {
        List<Task> projects = new List<Task>();

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Tasks";
        using SqlCommand cmd = new SqlCommand(query, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string name = reader.GetString(reader.GetOrdinal("Name"));
            Task p = getByName(name);
            projects.Add(p);
        }

        return projects;
    }
    
    public Task getByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Tasks WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", name);
        using SqlDataReader reader = cmd.ExecuteReader();
        Task task = null;
        if (reader.Read())
        {
            int duration = reader.GetInt32(reader.GetOrdinal("Duration"));
            int Slak = reader.GetInt32(reader.GetOrdinal("Slak"));
            string description = reader.GetString(reader.GetOrdinal("Description"));
            DateOnly startDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate")));
            DateOnly endDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate")));
            DateOnly LateStartDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("LateStartDate")));
            DateOnly LateEndDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("LateEndDate")));
            string status = reader.GetString(reader.GetOrdinal("Status"));
            TaskProgress statusR = TaskProgress.Pending;
            if (status == "Completed")
            {
                statusR = Enum.TaskProgress.Completed;
            }
            if (status == "InProgress")
            {
                statusR = Enum.TaskProgress.InProgress;
            }
            task = new Task(name, description, duration, Slak,  startDate, endDate, statusR, LateStartDate, LateEndDate);
        }

        if (task != null)
        {
            task.Dependencies = getDependencies(task.Name);
            task.Resources = getResources(task.Name);
        }
        
        return task;
    }

    private List<Task> getDependencies(string taskName)
    {
        List<Task> dependencies = new List<Task>();
        using SqlConnection connection = new(connectionString);
        connection.Open();
        
        string query = "SELECT DependsOnTaskName FROM TaskDependencies WHERE TaskName = @TaskName";
        using SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@TaskName", taskName);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string depName = reader.GetString(0);
            Task depTask = getByName(depName);
            if (depTask != null)
            {
                dependencies.Add(depTask);
            }
        }

        return dependencies;
    }

    private List<Resource> getResources(string taskName)
    {
        List<Resource> dependencies = new List<Resource>();
        using SqlConnection connection = new(connectionString);
        connection.Open();
        
        string query = "SELECT ResourceName FROM TaskResources WHERE TaskName = @TaskName";
        using SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@TaskName", taskName);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string depName = reader.GetString(0);
            Resource depTask = getRByName(depName);
            if (depTask != null)
            {
                dependencies.Add(depTask);
            }
        }

        return dependencies;
    }
    
    public Resource getRByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Resources WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", name);
        using SqlDataReader reader = cmd.ExecuteReader();
        Resource t = null;
        if (reader.Read())
        {
            string description = reader.GetString(reader.GetOrdinal("Description"));
            string InCommon = reader.GetString(reader.GetOrdinal("InCommon"));
            string TaskType = reader.GetString(reader.GetOrdinal("TaskType"));
            TaskType TaskTypeR = Enum.TaskType.Human;
            if (TaskType == "Infrastructure")
            {
                TaskTypeR = Enum.TaskType.Infrastructure;
            }
            if (TaskType == "Software")
            {
                TaskTypeR = Enum.TaskType.Software;
            }
            t = new Resource(name,TaskTypeR, description, InCommon);
            t.resourceUsed = getUsed(name);
            t.ResourceUsedComp = getUsedComp(name);
        }
        return t;
    }
    
    private List<ResourceUsed> getUsedComp(string name)
    {
        List<ResourceUsed> projects = new List<ResourceUsed>();
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM ResourceUsagesComp WHERE ResourceName = @ResourceName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", name);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string ResourceName = reader.GetString(reader.GetOrdinal("ResourceName"));
            string TaskName = reader.GetString(reader.GetOrdinal("TaskName"));
            string ProjectName = reader.GetString(reader.GetOrdinal("ProjectName"));
            DateOnly StartDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate")));
            DateOnly EndDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate")));
            ResourceUsed resource = new ResourceUsed(ResourceName, TaskName, ProjectName, StartDate, EndDate);
            projects.Add(resource);
        }
        return projects;
    }
    
    private List<ResourceUsed> getUsed(string name)
    {
        List<ResourceUsed> projects = new List<ResourceUsed>();
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM ResourceUsages WHERE ResourceName = @ResourceName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", name);
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string ResourceName = reader.GetString(reader.GetOrdinal("ResourceName"));
            string TaskName = reader.GetString(reader.GetOrdinal("TaskName"));
            string ProjectName = reader.GetString(reader.GetOrdinal("ProjectName"));
            DateOnly StartDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate")));
            DateOnly EndDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate")));
            ResourceUsed resource = new ResourceUsed(ResourceName, TaskName, ProjectName, StartDate, EndDate);
            projects.Add(resource);
        }
        return projects;
    }
    
    public void Add(Task project)
    {
        if (project == null)
        {
            throw new ArgumentException("Invalid Task");
        }
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        INSERT INTO Tasks (Name, Duration, Description, StartDate, EndDate, LateStartDate, LateEndDate, Slak, Status)
        VALUES (@Name, @Duration, @Description, @StartDate, @EndDate, @LateStartDate, @LateEndDate, @Slak, @Status)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@Duration", project.Duration);
        cmd.Parameters.AddWithValue("@Description", project.Description);
        cmd.Parameters.AddWithValue("@StartDate", project.StartDate);
        cmd.Parameters.AddWithValue("@EndDate", project.EndDate);
        cmd.Parameters.AddWithValue("@LateStartDate", project.LateStartDate);
        cmd.Parameters.AddWithValue("@LateEndDate", project.LateEndDate);
        cmd.Parameters.AddWithValue("@Slak", project.Slak);
        cmd.Parameters.AddWithValue("@Status", project.Status);
        cmd.ExecuteNonQuery();
    }
    
    public virtual void Update(Task project)
    {
        if (Equals(project, null))
        {
            throw new ArgumentException("Invalid Task");
        }

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        UPDATE Tasks
        SET 
            Duration = @Duration,
            Description = @Description,
            StartDate = @StartDate,
            EndDate = @EndDate,
            LateStartDate = @LateStartDate,
            LateEndDate = @LateEndDate,
            Slak = @Slak,
            Status = @Status
        WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@Duration", project.Duration);
        cmd.Parameters.AddWithValue("@Description", project.Description);
        cmd.Parameters.AddWithValue("@StartDate", project.StartDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@EndDate", project.EndDate  .ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@LateStartDate", project.LateStartDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@LateEndDate", project.LateEndDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@Slak", project.Slak);
        cmd.Parameters.AddWithValue("@Status", project.Status.ToString());
        int affected = cmd.ExecuteNonQuery();
        
        if (affected == 0)
        {
            throw new Exception("Task not found or not updated.");
        }
    }
    
    public void Remove(Task project)
    {
        RemovePro(project);
        RemoveAsDep(project);
        RemoveAsTask(project);
        RemoveRes(project);
        RemoveU(project);
        RemoveComp(project);
        RemoveUsages(project);
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM Tasks WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.ExecuteNonQuery();
    }
    private void RemoveAsTask(Task project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM TaskDependencies WHERE TaskName = @TaskName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@TaskName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveAsDep(Task project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM TaskDependencies WHERE DependsOnTaskName = @DependsOnTaskName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@DependsOnTaskName", project.Name);
        cmd.ExecuteNonQuery();
    }

    
    private void RemoveRes(Task project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM TaskResources WHERE TaskName = @TaskName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@TaskName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemovePro(Task project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM TasksProjects WHERE TaskName = @TaskName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@TaskName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveU(Task project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM UserTasks WHERE TaskName = @TaskName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@TaskName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveComp(Task project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM ResourceUsagesComp WHERE TaskName = @TaskName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@TaskName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveUsages(Task project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM ResourceUsages WHERE TaskName = @TaskName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@TaskName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    public void InsertDep(Task task, Task dep)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
            IF NOT EXISTS (
            SELECT 1
            FROM TaskDependencies
            WHERE TaskName          = @TaskName
            AND DependsOnTaskName = @DependsOnTaskName
            )
            BEGIN
            INSERT INTO TaskDependencies (TaskName, DependsOnTaskName)
            VALUES (@TaskName, @DependsOnTaskName)
            END";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@TaskName", task.Name);
        cmd.Parameters.AddWithValue("@DependsOnTaskName", dep.Name);
        cmd.ExecuteNonQuery();
    }

}