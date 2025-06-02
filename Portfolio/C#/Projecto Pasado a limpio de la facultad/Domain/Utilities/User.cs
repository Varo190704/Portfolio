using Domain.Enum;
using Domain.Validators;

namespace Domain.Utilities;

public class User
{
    private string _name;
    private string _surname;
    private string _email;
    private string _password;
    private DateOnly _birthDate;
    
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

    public string Surname
    {
        get => _surname;
        set
        {
            if (Validator.stringnotnull(value))
            {
                _surname = value;
            }
            else
            {
                throw new ArgumentException("Surname cannot be Null");
            }
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (Validator.stringnotnull(value))
            {
                if (Validator.validEmail(value))
                {
                    _email = value;
                }
                else
                {
                    throw new ArgumentException("Invalid Email");
                }
            }
            else
            {
                throw new ArgumentException("Email cannot be Null");
            }
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (Validator.stringnotnull(value))
            {
                if (Validator.validPassword(value))
                {
                    _password = value;
                }
                else
                {
                    throw new ArgumentException("Invalid Password");
                }
            }
            else
            {
                throw new ArgumentException("Password cannot be Null");
            }
        }
    }

    public DateOnly BirthDate
    {
        get => _birthDate;
        set
        {
            if (Validator.datevalidUser(value))
            {
                _birthDate = value;
            }
            else
            {
                throw new ArgumentException("Invalid BirthDate, user must be born before 01/01/2007");
            }
        }
    }
    public bool AdminSistem { get; set; }
    public List<UserType> Types = new  List<UserType>(); /*lo cambie*/
    public List<Task> tasks = new List<Task>();
    public List<Project> projects = new List<Project>();
    public bool login { get; set; }
    
    public User(){}

    public User(string name, string surname, string email, string password, DateOnly birthDate)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        BirthDate = birthDate;  
        AdminSistem = false;
    }
    
}
