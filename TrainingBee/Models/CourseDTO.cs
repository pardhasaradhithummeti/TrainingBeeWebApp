using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainingBee.Models
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage ="Provide Course Name")]
        [DisplayName("Name of the Course")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Provide Course Fees")]
        [Range(10000,75000, ErrorMessage ="Course fee should be in the range of 10000-75000")]
        [DisplayName("Course fees")]
        public int CourseFees { get; set; }
        [Required(ErrorMessage = "Provide Course Duration")]
        [Range(1, 500, ErrorMessage ="Course Duration should be in the range of 1-500 day")]
        public int CourseDuration { get; set; }
    }
}
