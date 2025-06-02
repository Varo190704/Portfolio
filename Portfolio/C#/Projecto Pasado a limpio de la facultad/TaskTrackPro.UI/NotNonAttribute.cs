using System.ComponentModel.DataAnnotations;

namespace TaskTrackPro.UI;

public class NotNonAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        var str = value as string;
        if (str == "non")
        {
            return new ValidationResult(ErrorMessage ??
                                        "No es válida tu selección");
        }
        return ValidationResult.Success;
    }
}