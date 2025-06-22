using Domain.Enum;
using Domain.Utilities;
using Microsoft.Data.SqlClient;
using Task = Domain.Utilities.Task;

namespace Domain.Infrastructure.SqlConecc;

public class ProjectInSQL
{
    private readonly string connectionString = "Server=localhost,1433;Database=TaskTrackPro;User Id=sa;Password=Passw1rd;TrustServerCertificate=True;";

    public List<Project> GetAll()
    {
        List<Project> projects = new List<Project>();

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Projects";
        using SqlCommand cmd = new SqlCommand(query, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string name = reader.GetString(reader.GetOrdinal("Name"));
            Project p = getByName(name);
            projects.Add(p);
        }

        return projects;
    }
    
    public Project getByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Projects WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", name);
        using SqlDataReader reader = cmd.ExecuteReader();
        Project project = null;
        if (reader.Read())
        {
            string description = reader.GetString(reader.GetOrdinal("Description"));
            string adminEma = reader["AdminEmail"] as string;
            string liderEma = reader["LiderEmail"] as string;
            DateOnly startDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate")));
            DateOnly endDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate")));
            int duration = reader.GetInt32(reader.GetOrdinal("Duration"));
            project = new Project(name, description, startDate, endDate, duration, adminEma, liderEma);
            project.Members = getUsers(name);
            project.Tasks = getTasks(name);
        }
        return project;
    }
    
    public List<Task> getTasks(string name)
    {
        List<Task> dependencies = new();

        using SqlConnection connection = new(connectionString);
        connection.Open();

        string query = "SELECT TaskName FROM TasksProjects WHERE ProjectName = @ProjectName";
        using SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", name);

        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string depName = reader.GetString(0);
            Task depTask = getTByName(depName);
            if (depTask != null)
            {
                dependencies.Add(depTask);
            }
        }

        return dependencies;
    }
    
    public List<User> getUsers(string name)
    {
        List<User> dependencies = new();

        using SqlConnection connection = new(connectionString);
        connection.Open();

        string query = "SELECT Email FROM UserProjects WHERE ProjectName = @ProjectName";
        using SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", name);

        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string depName = reader.GetString(0);
            User depTask = getUByName(depName);
            if (depTask != null)
            {
                dependencies.Add(depTask);
            }
        }

        return dependencies;
    }
    
    public User getUByName(string name)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Users WHERE Email = @Email";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", name);
        using SqlDataReader reader = cmd.ExecuteReader();
        User u = null;
        if (reader.Read())
        {
            string nameu = reader.GetString(reader.GetOrdinal("Name"));
            string email = reader.GetString(reader.GetOrdinal("Email"));
            string surname = reader.GetString(reader.GetOrdinal("Surname"));
            string password = reader.GetString(reader.GetOrdinal("Password"));
            DateOnly BirthDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("BirthDate")));
            bool adminSis = reader.GetBoolean(reader.GetOrdinal("AdminSistem"));
            bool Login = reader.GetBoolean(reader.GetOrdinal("Login"));
            u = new User(nameu, surname, email, password, BirthDate, adminSis, Login);
            u.Types = getUType(email);
        }
        return u;
    }
    
    
    public List<UserType> getUType(string email)
    {
        List<UserType> dependencies = new();

        using SqlConnection connection = new(connectionString);
        connection.Open();

        string query = "SELECT Type FROM UserTypes WHERE Email = @Email";
        using SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@Email", email);

        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string depName = reader.GetString(0);
            UserType depTask = UserType.SystemAdmin;
            if (Equals(depName, "ProjectAdmin"))
            {
                depTask = UserType.ProjectAdmin;
            }

            if (Equals(depName, "ProjectLider"))
            {
                depTask = UserType.ProjectLider;
            }

            if (Equals(depName, "ProjectMember"))
            {
                depTask = UserType.ProjectMember;
            }
            dependencies.Add(depTask);
        }

        return dependencies;
    }

    public Task getTByName(string name)
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
            Task depTask = getTByName(depName);
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
            return new Resource(name,TaskTypeR, description, InCommon);
        }
        return null;
    }
    
    public void Add(Project project)
    {
        if (project == null)
        {
            throw new ArgumentException("Invalid Project");
        }
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        INSERT INTO Projects (Name, Description, StartDate, EndDate, Duration, AdminEmail, LiderEmail)
        VALUES (@Name, @Description, @StartDate, @EndDate, @Duration, @AdminEmail, @LiderEmail)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@Description", project.Description);
        cmd.Parameters.AddWithValue("@StartDate", project.StartDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@EndDate", project.EndDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@Duration", project.Duration);
        cmd.Parameters.AddWithValue("@AdminEmail", project.Admin);
        cmd.Parameters.AddWithValue("@LiderEmail", project.Lider);
        cmd.ExecuteNonQuery();
    }
   
    public void assTask(Project project, Task task)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
            IF NOT EXISTS (
            SELECT 1 
            FROM TasksProjects 
            WHERE TaskName    = @TaskName
            AND ProjectName = @ProjectName
            )
            BEGIN
            INSERT INTO TasksProjects (TaskName, ProjectName)
            VALUES (@TaskName, @ProjectName);
            END";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.Parameters.AddWithValue("@TaskName", task.Name);
        cmd.ExecuteNonQuery();
    }
    
    public void assTaskMember(Project project, Task task, User user)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        INSERT INTO UserTasks (Email, TaskName, ProjectName)
        VALUES (@Email, @TaskName, @ProjectName)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.Parameters.AddWithValue("@TaskName", task.Name);
        cmd.ExecuteNonQuery();
    }
    
    public void assMember(Project project, User user)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        INSERT INTO UserProjects (ProjectName, Email)
        VALUES (@ProjectName, @Email)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.ExecuteNonQuery();
    }
    
    public void assLider(Project project, User user)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        UPDATE Projects
        SET 
            LiderEmail = @LiderEmail
        WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@LiderEmail", user.Email);
        cmd.ExecuteNonQuery();
    }
    
    public void assAdmin(Project project, User user)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        UPDATE Projects
        SET 
            AdminEmail = @LiderEmail
        WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@AdminEmail", user.Email);
        cmd.ExecuteNonQuery();
    }
    
    public void RemoveTP(Project project, Task task)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM TasksProjects WHERE ProjectName = @ProjectName AND TaskName = @TaskName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.Parameters.AddWithValue("@TaskName", task.Name);
        cmd.ExecuteNonQuery();
    }
    
    public void RemoveUP(Project project, User task)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM UserProjects WHERE ProjectName = @ProjectName AND Email = @Email";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.Parameters.AddWithValue("@Email", task.Email);
        cmd.ExecuteNonQuery();
    }
    
    public virtual void Update(Project project)
    {
        if (Equals(project, null))
        {
            throw new ArgumentException("Invalid Project");
        }

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        UPDATE Projects
        SET 
            Description = @Description,
            StartDate = @StartDate,
            EndDate = @EndDate,
            Duration = @Duration,
            AdminEmail = @AdminEmail,
            LiderEmail = @LiderEmail
        WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@Description", project.Description);
        cmd.Parameters.AddWithValue("@StartDate", project.StartDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@EndDate", project.EndDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@Duration", project.Duration);
        cmd.Parameters.AddWithValue("@AdminEmail", project.Admin);
        cmd.Parameters.AddWithValue("@LiderEmail", project.Lider);
        int affected = cmd.ExecuteNonQuery();
        
        if (affected == 0)
        {
            throw new Exception("Project not found or not updated.");
        }
    }
    
    public void Remove(Project project)
    {
        RemoveT(project);
        RemoveComp(project);
        RemoveU(project);
        RemoveUsages(project);
        RemoveUTP(project);
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM Projects WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveT(Project project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM TasksProjects WHERE ProjectName = @ProjectName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveU(Project project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM UserProjects WHERE ProjectName = @ProjectName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveComp(Project project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM ResourceUsagesComp WHERE ProjectName = @ProjectName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveUTP(Project project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM UserTasks WHERE ProjectName = @ProjectName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveUsages(Project project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM ResourceUsages WHERE ProjectName = @ProjectName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ProjectName", project.Name);
        cmd.ExecuteNonQuery();
    }    
}