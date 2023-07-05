using System.ComponentModel.DataAnnotations;
using CollegeApp.Validators;


namespace CollegeApp.Models;

public class StudentDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Student name is required")]
    [StringLength(30, MinimumLength = 2,
        ErrorMessage = "Student name should be minimum 2 characters and a maximum of 50 characters")]

    public string? StudentName { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    public int Age { get; set; }

    [Range(12, 60, ErrorMessage = "Age should be between 12 and 60")]
    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; }

    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Password and confirm password should match")]

    public string? ConfirmPassword { get; set; }

    [DataCheck] public DateTime AdmissionDate { get; set; }
}