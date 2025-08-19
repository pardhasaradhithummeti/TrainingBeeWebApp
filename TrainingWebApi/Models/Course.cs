namespace TrainingWebApi.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CourseFees { get; set; }
        public int CourseDuration { get; set; }

        //Navigation property
        //here, one course will have many students
        public virtual List<Student> StudentEnrolled { get; set; }
    }
}
