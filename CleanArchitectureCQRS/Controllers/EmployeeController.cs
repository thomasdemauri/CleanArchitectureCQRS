using CleanArchitecture.Application.Commands.Employee;
using CleanArchitecture.Application.Queries;
using CleanArchitecture.Application.Requests.Employee;
using CleanArchitecture.Application.ViewModels.Employee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureCQRS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeQueries _employeeQueries;
        private readonly IMediator _mediator;

        public EmployeeController(IEmployeeQueries employeeQueries, IMediator mediator)
        {
            _employeeQueries = employeeQueries;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var id = await _mediator.Send(request);

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeDetailedViewModel>> GetById(Guid id)
        {
            var employee = await _employeeQueries.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        [Route("{employeeId:guid}/contracts")]
        public async Task<IActionResult> AddContract(Guid employeeId, [FromBody] AddEmployeeContractRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var command = new CreateEmployeeContractCommand(
                request.AdmissionDate, request.FirstProbationPeriodDays,
                request.SecondProbationPeriodDays, request.Salary, request.ManagerId, employeeId);

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = employeeId }, employeeId); // mudar aqui depois
        }
    }
}
