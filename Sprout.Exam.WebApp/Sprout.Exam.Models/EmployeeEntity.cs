using System;

namespace Sprout.Exam.Models
{
    public class EmployeeEntity : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string TIN { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
