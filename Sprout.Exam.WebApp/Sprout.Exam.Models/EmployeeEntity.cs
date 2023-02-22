namespace Sprout.Exam.Models
{
    public class EmployeeEntity : BaseEntity
    {
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Tin { get; set; }
        public int TypeId { get; set; }
    }
}
