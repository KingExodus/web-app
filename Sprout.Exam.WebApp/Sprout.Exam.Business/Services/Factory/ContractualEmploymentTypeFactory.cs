using Sprout.Exam.Business.Domain.Factory;
using System;

namespace Sprout.Exam.Business.Services.Factory
{
    public class ContractualEmploymentTypeFactory : IEmploymentTypeFactory
    {
        public decimal ComputeSalary(decimal days)
        {
            decimal salaryPerDay = 500;
            decimal workedDays = days;
            decimal netPay = salaryPerDay * workedDays;

            return Math.Round(netPay, 2);
        }
    }
}
