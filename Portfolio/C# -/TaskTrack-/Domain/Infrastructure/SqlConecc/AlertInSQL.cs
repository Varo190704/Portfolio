using Domain.Utilities;
using Microsoft.Data.SqlClient;

namespace Domain.Infrastructure.SqlConecc;

public class AlertInSQL
{
    private readonly string connectionString = "Server=localhost,1433;Database=TaskTrackPro;User Id=sa;Password=Passw1rd;TrustServerCertificate=True;";

    public List<Alert> GetAll()
    {
        List<Alert> projects = new List<Alert>();

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Alerts";
        using SqlCommand cmd = new SqlCommand(query, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(reader.GetOrdinal("id"));
            Alert p = getById(id);
            projects.Add(p);
        }

        return projects;
    }
    
    public Alert getById(int Id)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "SELECT * FROM Alerts WHERE Id = @Id";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", Id);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        if (reader.Read())
        {
            int id = reader.GetInt32(reader.GetOrdinal("id"));
            string ema = reader["Email"] as string;
            string description = reader["Message"] as string;

            return new Alert(description, id, ema);
        }
        return null;
    }
    
    public void Add(Alert alert)
    {
        if (alert == null)
        {
            throw new ArgumentException("Invalid Alert");
        }
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        INSERT INTO Alerts (Id, Email, Message)
        VALUES (@Id, @Email, @Message)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", alert.Id);
        cmd.Parameters.AddWithValue("@Email", alert.Email);
        cmd.Parameters.AddWithValue("@Message", alert.Message);
        cmd.ExecuteNonQuery();
    }
    
    public virtual void Update(Alert alert)
    {
        if (Equals(alert, null))
        {
            throw new ArgumentException("Invalid Alert");
        }

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"
        UPDATE Alerts
        SET 
            Message = @Message
        WHERE Id = @Id";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Message", alert.Message);
        cmd.Parameters.AddWithValue("@Id", alert.Id);
        cmd.ExecuteNonQuery();
    }
    
    public void Remove(Alert alert)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = "DELETE FROM Alerts WHERE Id = @Id";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", alert.Id);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected == 0)
        {
            throw new ArgumentException("Alert not found");
        }
    }
}