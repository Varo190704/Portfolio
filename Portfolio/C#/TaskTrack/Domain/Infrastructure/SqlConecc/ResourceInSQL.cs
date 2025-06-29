using Domain.Enum;
using Domain.Utilities;
using Domain.Validators;
using Microsoft.Data.SqlClient;

namespace Domain.Infrastructure.SqlConecc;

public class ResourceInSQL
{
    private readonly string connectionString = "Server=localhost,1433;Database=TaskTrackPro;User Id=sa;Password=Passw1rd;TrustServerCertificate=True;";

    public List<Resource> GetAll()
    {
        List<Resource> projects = new List<Resource>();

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Resources";
        using SqlCommand cmd = new SqlCommand(query, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string name = reader.GetString(reader.GetOrdinal("Name"));
            Resource p = getByName(name);
            projects.Add(p);
        }

        return projects;
    }
    
    public Resource getByName(string name)
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
        Resource r = null;
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
            r = new Resource(name,TaskTypeR, description, InCommon);
            r.resourceUsed = getUsed(name);
            r.ResourceUsedComp = getUsedComp(name);
        }
        return r;
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
    
    public void Add(Resource project)
    {
        if (project == null)
        {
            throw new ArgumentException("Invalid Resource");
        }
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        INSERT INTO Resources (Name, TaskType, Description, InCommon)
        VALUES (@Name, @TaskType, @Description, @InCommon)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@Description", project.Description);
        cmd.Parameters.AddWithValue("@InCommon", project.inCommon);
        cmd.Parameters.AddWithValue("@TaskType", project.TaskType.ToString());
        cmd.ExecuteNonQuery();
    }
    
    public void AddUsed(Resource resource, string task, string project, DateOnly startDate, DateOnly endDate)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        INSERT INTO ResourceUsages (ResourceName, TaskName, ProjectName, StartDate, EndDate)
        VALUES (@ResourceName, @TaskName, @ProjectName, @StartDate, @EndDate)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", resource.Name);
        cmd.Parameters.AddWithValue("@TaskName", task);
        cmd.Parameters.AddWithValue("@ProjectName", project);
        cmd.Parameters.AddWithValue("@StartDate", startDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@EndDate", endDate.ToDateTime(TimeOnly.MinValue));
        cmd.ExecuteNonQuery();
    }
    
    public void AddComp(Resource resource, string task, string project, DateOnly startDate, DateOnly endDate)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        INSERT INTO ResourceUsagesComp (ResourceName, TaskName, ProjectName, StartDate, EndDate)
        VALUES (@ResourceName, @TaskName, @ProjectName, @StartDate, @EndDate)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", resource.Name);
        cmd.Parameters.AddWithValue("@TaskName", task);
        cmd.Parameters.AddWithValue("@ProjectName", project);
        cmd.Parameters.AddWithValue("@StartDate", startDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@EndDate", endDate.ToDateTime(TimeOnly.MinValue));
        cmd.ExecuteNonQuery();
    }
    
    
    public void assigned(string resourceName, string taskName, string selProjectName, DateOnly assign2StartDate, DateOnly assign2EndDate)
    {
        AddComp(getByName(resourceName), taskName, selProjectName, assign2StartDate, assign2EndDate);
        AddUsed(getByName(resourceName), taskName, selProjectName, assign2StartDate, assign2EndDate);
    }
    
    public virtual void Update(Resource project)
    {
        if (Equals(project, null))
        {
            throw new ArgumentException("Invalid Resource");
        }

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        UPDATE Resources
        SET 
            Description = @Description,
            TaskType = @TaskType,
            InCommon = @InCommon
        WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@Description", project.Description);
        cmd.Parameters.AddWithValue("@InCommon", project.inCommon);
        cmd.Parameters.AddWithValue("@TaskType", project.TaskType.ToString());
        cmd.ExecuteNonQuery();
    }
    
    public void Remove(Resource project)
    {
        RemoveComp(project);
        RemoveUsages(project);
        RemoveTas(project);
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM Resources WHERE Name = @Name";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.ExecuteNonQuery();
    }

    private void RemoveComp(Resource project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM ResourceUsagesComp WHERE ResourceName = @ResourceName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveUsages(Resource project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM ResourceUsages WHERE ResourceName = @ResourceName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", project.Name);
        cmd.ExecuteNonQuery();
    }
    
    public void RemoveUsagesR(string resourceName, string projectName, string taskName)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        DELETE FROM ResourceUsages
         WHERE ResourceName = @ResourceName
           AND TaskName     = @TaskName
           AND ProjectName  = @ProjectName;
           ";
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", resourceName);
        cmd.Parameters.AddWithValue("@TaskName",     taskName);
        cmd.Parameters.AddWithValue("@ProjectName",  projectName);
        cmd.ExecuteNonQuery();
    }
    
    private void RemoveTas(Resource project)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM TaskResources WHERE ResourceName = @ResourceName";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", project.Name);
        cmd.ExecuteNonQuery();
    }

    public bool isUsed(Resource resource, DateOnly startDate, DateOnly endDate)
    {
        using SqlConnection  connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"SELECT COUNT(1) 
                FROM ResourceUsages 
                WHERE ResourceName = @ResourceName 
                AND ( 
                (@startDate BETWEEN StartDate AND EndDate) 
                OR (@endDate   BETWEEN StartDate AND EndDate) 
                OR (StartDate   BETWEEN @startDate AND @endDate)
                );";
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", resource.Name);
        cmd.Parameters.AddWithValue("@startDate", startDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@endDate",   endDate.ToDateTime(TimeOnly.MinValue));
        int overlaps = Convert.ToInt32(cmd.ExecuteScalar());
        return overlaps > 0;
    }

    public ResourceUsed getUsedR(Resource resource, DateOnly startDate, DateOnly endDate)
    {
        using SqlConnection  connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
            SELECT TOP 1 *
            FROM ResourceUsages
            WHERE ResourceName = @ResourceName
            AND (
            (@startDate BETWEEN StartDate AND EndDate)
            OR (@endDate   BETWEEN StartDate AND EndDate)
            OR (StartDate   BETWEEN @startDate AND @endDate)
            );";
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ResourceName", resource.Name);
        cmd.Parameters.AddWithValue("@startDate",     startDate.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@endDate",       endDate.  ToDateTime(TimeOnly.MinValue));
        using var reader = cmd.ExecuteReader();
        ResourceUsed r = null;
        if (reader.Read())
        {
            r = new ResourceUsed(reader.GetString(reader.GetOrdinal("ResourceName")),
                reader.GetString(reader.GetOrdinal("TaskName")), reader.GetString(reader.GetOrdinal("ProjectName")),
                DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate"))),
                DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate"))));
        }
        return r;
    }

    public ResourceUsed getFUsed(Resource resource)
    {
        Resource r = getByName(resource.Name);
        return r.ResourceUsedComp.FirstOrDefault();
    }
}