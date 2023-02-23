using Sprout.Exam.Business.Domain.Factory;
using System;

namespace Sprout.Exam.Business.Services.Factory
{
    public class RegularEmploymentTypeFactory : IEmploymentTypeFactory
    {
        public decimal ComputeSalary(decimal days)
        {
            decimal basicSalary = 20000;
            decimal salaryPerDay = basicSalary / 22;
            decimal tax = basicSalary * (decimal)0.12;
            decimal absences = salaryPerDay * days;
            decimal netPay = basicSalary - absences - tax;

            return Math.Round(netPay, 2);
        }
    }
}
