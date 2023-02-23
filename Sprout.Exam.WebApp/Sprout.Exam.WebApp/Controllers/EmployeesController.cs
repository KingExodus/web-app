using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Domain;
using System.Threading;
using Sprout.Exam.Business.Domain.Query;
using AutoMapper;
using System.Collections.Generic;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAddEmployeeCommand _addEmployeeCommand;
        private readonly IUpdateEmployeeCommand _updateEmployeeCommand;
        private readonly IEmployeeQuery _employeeQuery;
        private readonly IEmployeeByIdQuery _employeeByIdQuery;
        private readonly IRemoveEmployeeCommand _removeEmployeeCommand;
        private readonly ICalculateSalaryCommand _calculateSalaryCommand;

        public EmployeesController(IMapper mapper,
            IAddEmployeeCommand addEmployeeCommand,
            IUpdateEmployeeCommand updateEmployeeCommand,
            IEmployeeQuery employeeQuery,
            IEmployeeByIdQuery employeeByIdQuery,
            IRemoveEmployeeCommand removeEmployeeCommand,
            ICalculateSalaryCommand calculateSalaryCommand)
        {
            _mapper = mapper;
            _addEmployeeCommand = addEmployeeCommand;
            _updateEmployeeCommand = updateEmployeeCommand;
            _employeeQuery = employeeQuery;
            _employeeByIdQuery = employeeByIdQuery;
            _removeEmployeeCommand = removeEmployeeCommand;
            _calculateSalaryCommand = calculateSalaryCommand;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _employeeQuery.ExecuteAsync(User, cancellationToken);
            
            return Ok(_mapper.Map<List<EmployeeDto>>(result));
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _employeeByIdQuery.ExecuteAsync(id, User, cancellationToken);
            if (result == null)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<EmployeeDto>(result));
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EditEmployeeDto input, CancellationToken cancellationToken)
        {
            if (input == null)
            {
                return BadRequest();
            }
            input.Id = id;

            var result = await _updateEmployeeCommand.ExecuteAsync(input, User, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateEmployeeDto input, CancellationToken cancellationToken)
        {
            if (input == null)
            {
                return BadRequest();
            }

            var result = await _addEmployeeCommand.ExecuteAsync(input, User, cancellationToken);
            if (result == null)
            {
                return BadRequest("Employee is already exist");
            }

            return Created($"/api/employees/{result.Id}", result.Id);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _removeEmployeeCommand.ExecuteAsync(id, User, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Id);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate([FromBody]EmployeeSalaryDto input, CancellationToken cancellationToken)
        {
            if (input == null)
            {
                return BadRequest();
            }

            var result = await _calculateSalaryCommand.ExecuteAsync(input, User, cancellationToken);

            return Ok(result.SalaryNetPay);
        }

    }
}
