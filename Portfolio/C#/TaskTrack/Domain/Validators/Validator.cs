using System.Net.Mail;

namespace Domain.Validators;

public static class Validator
{
    public static bool validEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            return false;
        }

        string[] parts = email.Split('@');
        if (parts.Length != 2)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(parts[0]))
        {
            return false;
        }
        
        string[] domainParts = parts[1].Split('.');

        if (domainParts.Length < 2)
        {
            return false;
        }

        if (string.IsNullOrEmpty(domainParts[0]))
        {
            return false;
        }
        
        return domainParts[1].Length >= 2;
    }

    public static bool validPassword(string password)
    {
        if (password.Length < 8)
        {
            return false;
        }
        
        bool upperCase = false;
        bool lowerCase = false;
        bool numer = false;
        bool singular = false;
        
        foreach (char c in password)
        {
            if (c >= 'A' && c <= 'Z')
            {
                upperCase = true;
            }
            else if (c >= 'a' && c <= 'z')
            {
                lowerCase = true;
            }
            else if (c >= '0' && c <= '9')
            {
                numer = true;
            }
            else
            {
                singular = true;
            }
        }
        return upperCase && lowerCase && numer && singular;
    }

    public static bool stringnotnull(string String)
    {
        bool valid = true;
        
        if(string.IsNullOrEmpty(String))
        {
            valid = false;
        }

        return valid;
    }
    
    public static bool datevalidUser(DateOnly date)
    {
        bool valid = true;

        if (date == null)
        {
            valid = false;
        }
        
        if (date.Year > 2007)
        {
            valid = false;
        }
        
        if (date.DayOfYear < 1 || date.DayOfYear > 366)
        {
            valid = false;
        }

        if (date.Month < 1 || date.Month > 12)
        {
            valid = false;
        }

        return valid;

    }

    public static bool invalidDescrip(string description)
    {
        bool valid = true;
        
        if (description.Length > 400)
        {
            valid = false;
        }
        
        return valid;
    }
}