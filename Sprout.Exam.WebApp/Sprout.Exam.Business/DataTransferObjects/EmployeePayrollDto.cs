namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EmployeePayrollDto : BaseSaveEmployeeDto
    {
        public int Id { get; set; }
        public int SalaryNetPay { get; set; }
    }
}
