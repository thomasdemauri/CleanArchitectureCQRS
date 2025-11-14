using CleanArchitecture.Application.Commands.Company;
using CleanArchitecture.Application.ViewModels.Company;
using CleanArchitecture.Infrastructure.Queries;
using CleanArchitecture.Infrastructure.Caching;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitectureCQRS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CompanyQueries _companyQueries;
        private readonly ICachingService _cache;

        public CompaniesController(IMediator mediator, CompanyQueries companyQueries, ICachingService cache)
        {
            _mediator = mediator;
            _companyQueries = companyQueries;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
        {
            var id = await _mediator.Send(command);

            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyViewModel>>> GetCompanies()
        {
            var companies = await _companyQueries.GetAll();

            return Ok(companies);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CompanyDetailedViewModel>> GetById(Guid id)
        {
            var companyFromCache = await _cache.GetAsync<CompanyDetailedViewModel>($"company:{id}");

            if (companyFromCache is not null)
            {
                return Ok(new { Data = companyFromCache, FromCache = true });
            }

            var company = await _companyQueries.GetById(id);

            if (company is null)
            {
                return NotFound();
            }

            await _cache.SetAsync($"company:{id}", company);
            return Ok(new { Data = company, FromCache = false });
        }
    }
}
