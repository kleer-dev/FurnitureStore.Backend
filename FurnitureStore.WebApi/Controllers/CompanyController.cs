using AutoMapper;
using FurnitureStore.Application.CommandsQueries.Company.Commands.Create;
using FurnitureStore.Application.CommandsQueries.Company.Commands.Delete;
using FurnitureStore.WebApi.Dto.Company;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.WebApi.Controllers;

[ApiController]
[Route("api/company")]
public class CompanyController : BaseController
{
    private readonly IMapper _mapper;

    public CompanyController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateCompanyDto dto)
    {
        var command = _mapper.Map<CreateCompanyCommand>(dto);
        var companyId = await Mediator.Send(command);

        return Created("api/company", companyId);
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteCompanyCommand { Id = id };
        await Mediator.Send(command);

        return NoContent();
    }
}
