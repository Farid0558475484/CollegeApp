using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Validators;

public class DataCheckAttribute : ValidationAttribute

{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)


    {
        var date = (DateTime)value!;
        if (date < DateTime.Now)
        {
            return new ValidationResult("Admission date cannot be greater than today's date");
        }

        return ValidationResult.Success;
    }
}