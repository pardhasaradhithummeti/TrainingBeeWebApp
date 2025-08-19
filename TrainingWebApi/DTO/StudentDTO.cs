using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainingWebApi.DTO
{
    public class StudentDTO
    {
        public int RollNo { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DisplayName("Course Joined")]
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
    }
}
