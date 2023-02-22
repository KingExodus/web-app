namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EmployeePayrollDto : BaseSaveEmployeeDto
    {
        public int Id { get; set; }
        public decimal SalaryNetPay { get; set; }
    }
}
