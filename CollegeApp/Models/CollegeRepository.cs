namespace CollegeApp.Models;

public static class CollegeRepository
{
    public static List<Student> Students { get; set; } = new List<Student>
    {
        new Student
        {
            Id = 1,
            StudentName = "Joh",
            Email = "john@example.com",
            Address = "London"
        },
        new Student
        {
            Id = 2,
            StudentName = "Maary",
            Email = "john@example.com",
            Address = "London"
        },

        new Student
        {
            Id = 3,
            StudentName = "John",
            Email = "dfe",
            Address = "London"
        },
        new Student
        {
            Id = 4,
            StudentName = "mary",
            Email = "dfe",
            Address = "London"
        },
    };
}