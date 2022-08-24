using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.DTOs.Categories;
using OnionArchitecture.Application.Features.Commands.CategoryCommands;
using OnionArchitecture.Application.Features.Queries.CategoryQueries;
using OnionArchitecture.Application.Filters.Category;
using OnionArchitecture.Application.Interfaces;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;


namespace OnionArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMediator _mediator;

        public CategoriesController(IUnitOfWork unit, IMapper mapper,IHttpClientFactory clientFactory,IMediator mediator)
        {
            _unit = unit;
            _mapper = mapper;
            _clientFactory = clientFactory;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration =5)]
        [EnsureIdActionFilter]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            //if (id == 0) return BadRequest(new
            //{
            //    code = "Id",
            //    description = "Inputted id is invalid, you can't enter 0."
            //});
            //HttpClient client = _clientFactory.CreateClient("ghibliApi");
            //var ghibli = await client.GetStringAsync("/films/58611129-2dbc-4a81-a72f-77ddfc1b1b49");
            var query = new CategoryGetByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result.Response == null) return NotFound();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new CategoryGetAllQuery();
            var result = await _mediator.Send(query);
            if (result.Response is null || !result.Response.Any()) return NotFound();
           
            return Ok(result.Response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CategoryPostCommand command)
        {
            //CategoryPostCommand command = new(dto);
            var result = await _mediator.Send(command);
            if (result.Response is 0) return NotFound();
            return CreatedAtAction("Get", routeValues: new { id =result.Response}, new { category=command,createdAt=DateTime.Now.ToLocalTime() });
            //return StatusCode(StatusCodes.Status201Created, new
            //{
            //    category = result,
            //    created = DateTime.Now,
            //    modified = DateTime.Now
            //});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]CategoryPostDto dto)
        {
            Category existed = await _unit.CategoryRepository.GetByIdAsync(id, null);
            if (existed is null) return NotFound();
            _unit.CategoryRepository.Update(existed);
            existed.Name = dto.Name;
            existed.ModifiedAt = DateTime.Now;
            await _unit.SaveChangesAsync();
            return Ok(new
            {
                category = dto,
                modified = DateTime.Now
            });
        }

        [HttpDelete("{id}")]
        [EnsureIdActionFilter]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            Category existed = await _unit.CategoryRepository.GetByIdAsync(id, null);
            if (existed is null) return NotFound();

            await _unit.CategoryRepository.DeleteAsync(existed);
            return Ok(id);
        }
    }
}
