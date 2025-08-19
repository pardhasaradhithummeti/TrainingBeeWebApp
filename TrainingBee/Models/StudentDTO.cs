namespace TrainingBee.Models
{
    public class StudentDTO
    {
        public int RollNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; } 
    }
}
