using System.Collections;
using Domain.Enum;
using Domain.Utilities;
using Domain.Validators;
using Microsoft.Data.SqlClient;
using Task = Domain.Utilities.Task;

namespace Domain.Infrastructure.SqlConecc;

public class UserInSQL
{
    private readonly string connectionString = "Server=localhost,1433;Database=TaskTrackPro;User Id=sa;Password=Passw1rd;TrustServerCertificate=True;";

    public List<User> GetAll()
    {
        List<User> projects = new List<User>();

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Users";
        using SqlCommand cmd = new SqlCommand(query, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string email = reader.GetString(reader.GetOrdinal("Email"));
            User p = getByName(email);
            projects.Add(p);
        }

        return projects;
    }
    
    public User getByName(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Users WHERE Email = @Email";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", email);
        using SqlDataReader reader = cmd.ExecuteReader();
        User u = null;
        if (reader.Read())
        {
            string name = reader.GetString(reader.GetOrdinal("Name"));
            string surname = reader.GetString(reader.GetOrdinal("Surname"));
            string password = reader.GetString(reader.GetOrdinal("Password"));
            DateOnly BirthDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("BirthDate")));
            bool adminSis = reader.GetBoolean(reader.GetOrdinal("AdminSistem"));
            bool Login = reader.GetBoolean(reader.GetOrdinal("Login"));
            u = new User(name, surname, email, password, BirthDate, adminSis, Login);
            u.tasks = getTasks(email);
            u.projects = getProjects(email);
            u.Types = getType(email);
        }
        return u;
    }
    
    private List<UserType> getType(string email)
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

    private List<Project> getProjects(string email)
    {
        List<Project> dependencies = new();

        using SqlConnection connection = new(connectionString);
        connection.Open();

        string query = "SELECT ProjectName FROM UserProjects WHERE Email = @Email";
        using SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@Email", email);

        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string depName = reader.GetString(0);
            Project depTask = getPByName(depName);
            if (depTask != null)
            {
                dependencies.Add(depTask);
            }
        }

        return dependencies;
    }
    
    private Project getPByName(string name)
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
        
        if (reader.Read())
        {
            string description = reader.GetString(reader.GetOrdinal("Description"));
            string adminEma = reader["AdminEmail"] as string;
            string liderEma = reader["LiderEmail"] as string;
            DateOnly startDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate")));
            DateOnly endDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate")));
            int duration = reader.GetInt32(reader.GetOrdinal("Duration"));

            return new Project(name, description, startDate, endDate, duration, adminEma, liderEma);
        }
        return null;
    }
    
    private List<Task> getTasks(string email)
    {
        List<Task> dependencies = new();

        using SqlConnection connection = new(connectionString);
        connection.Open();

        string query = "SELECT TaskName FROM UserTasks WHERE Email = @Email";
        using SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@Email", email);

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
    
    private Task getTByName(string name)
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
    
    private Resource getRByName(string name)
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
    
    public void Add(User project)
    {
        if (project == null)
        {
            throw new ArgumentException("Invalid User");
        }
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        INSERT INTO Users (Email, Name, Surname, Password, Birthdate, AdminSistem, Login)
        VALUES (@Email, @Name, @Surname, @Password, @Birthdate, @AdminSistem, @Login)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@Email", project.Email);
        cmd.Parameters.AddWithValue("@AdminSistem", project.AdminSistem);
        cmd.Parameters.AddWithValue("@Birthdate", project.BirthDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@Surname", project.Surname);
        cmd.Parameters.AddWithValue("@Password", project.Password);
        cmd.Parameters.AddWithValue("@Login", project.login);
        cmd.ExecuteNonQuery();
    }

    public virtual void Update(User project)
    {
        if (Equals(project, null))
        {
            throw new ArgumentException("Invalid User");
        }

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        UPDATE Users
        SET 
            Name = @Name,
            Surname = @Surname,
            Password = @Password,
            Birthdate = @BirthDate,
            AdminSistem = @AdminSistem,
            Login = @Login
        WHERE Email = @Email";
        using SqlCommand cmd = new SqlCommand(query, connection);
        
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@Email", project.Email);
        cmd.Parameters.AddWithValue("@AdminSistem", project.AdminSistem);
        cmd.Parameters.AddWithValue("@Birthdate", project.BirthDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@Surname", project.Surname);
        cmd.Parameters.AddWithValue("@Password", project.Password);
        cmd.Parameters.AddWithValue("@Login", project.login);
        int affected = cmd.ExecuteNonQuery();
        
        if (affected == 0)
        {
            throw new Exception("User not found or not updated.");
        }
    }
    
    public void Remove(User project)
    {
        RemoveTa(project);
        RemovePro(project);
        RemoveTy(project);
        RemoveAlo(project);
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM Users WHERE Email = @Email";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", project.Email);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveTy(User project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM UserTypes WHERE Email = @Email";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", project.Email);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveTa(User project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM UserTasks WHERE Email = @Email";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", project.Email);
        cmd.ExecuteNonQuery();
    }
    
    private void RemovePro(User project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM UserProjects WHERE Email = @Email";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", project.Email);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveAlo(User project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM Alerts WHERE Email = @Email";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Email", project.Email);
        cmd.ExecuteNonQuery();
    }
}