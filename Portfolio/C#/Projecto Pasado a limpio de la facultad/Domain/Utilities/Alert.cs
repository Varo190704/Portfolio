namespace Domain.Utilities;

public class Alert
{
    private string _message { get; set; }
    private int _id { get; set; }
    private string _email { get; set; }
    public string Message
    {
        get => _message;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Message cannot be Null");
            }
            else
            {
                _message = value;
            }
        } 
        
    }
    public int Id
    {
        get => _id;
        set
        {
            if (value >= 1)
            {
                _id = value;
            }
            else
            {
                throw new ArgumentException("Id must be greater than 0");
            }
        }
        
    }

    public string Email
    {
        get => _email;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email cannot be Null");
            }
            _email = value;
        }
    }
    
    public Alert(){}

    public Alert(string message, int id, string email)
    {
        Message = message;
        Id = id;
        Email = email;
    }
    
    
}