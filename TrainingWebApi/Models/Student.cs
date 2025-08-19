using System.ComponentModel.DataAnnotations;

namespace TrainingWebApi.Models
{
    public class Student
    {
        [Key]
        public int RollNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
        //Foreign Key
        public int CourseId { get; set; }

        //Navigation Property : used to retrieve related details
        //this following property will retreive the Course details which the student joined for
        //here, we are implementing one student to one course relation
        public virtual Course CourseJoined { get; set; }
    }
}
