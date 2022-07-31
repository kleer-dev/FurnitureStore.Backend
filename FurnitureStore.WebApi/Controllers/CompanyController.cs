using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Company.Commands.Create;
using FurnitureStore.Application.CommandsQueries.Company.Commands.Delete;
using FurnitureStore.Application.CommandsQueries.Company.Commands.Update;
using FurnitureStore.Application.CommandsQueries.Company.Queries.Get;
using FurnitureStore.Application.CommandsQueries.Company.Queries.GetList;
using FurnitureStore.WebApi.Dto.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/companies")]
public class CompanyController : BaseController
{
    private readonly IMapper _mapper;

    public CompanyController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet("get/{id:long}")]
    public async Task<ActionResult<CompanyVm>> Get(long id)
    {
        var query = new GetCompanyQuery() { Id = id };
        var company = await Mediator.Send(query);

        return Ok(company);
    }

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll()
    {
        var query = new GetCompanyListQuery();
        var vm = await Mediator.Send(query);

        return Ok(vm.Companies);
    }

    [HttpPost("add")]
    public async Task<ActionResult<long>> Create([FromBody] CreateCompanyDto dto)
    {
        var command = _mapper.Map<CreateCompanyCommand>(dto);
        var companyId = await Mediator.Send(command);

        return Created("api/company", companyId);
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteCompanyCommand { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("update/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateCompanyDto dto)
    {
        var command = _mapper.Map<UpdateCompanyCommand>(dto);
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }
}
