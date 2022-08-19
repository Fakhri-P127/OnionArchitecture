using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.DTOs.Categories;
using OnionArchitecture.Application.Interfaces;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;


namespace OnionArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0) return BadRequest(new
            {
                code = "Id",
                description = "Inputted id is invalid, you can't enter 0."
            });

            Category category = await _unit.CategoryRepository.GetByIdAsync(id, null);
            if (category is null) return NotFound();

            CategoryItemDto dto = _mapper.Map<CategoryItemDto>(category);
            return Ok(dto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            List<Category> categories = await _unit.CategoryRepository.GetAllAsync(null);

            List<CategoryItemDto> dtos = _mapper.Map<List<CategoryItemDto>>(categories);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryPostDto dto)
        {
            Category category = _mapper.Map<Category>(dto);
            category.CreatedAt = DateTime.Now;
            category.ModifiedAt = DateTime.Now;

            await _unit.CategoryRepository.AddAsync(category);

            return StatusCode(StatusCodes.Status201Created, new
            {
                category = dto,
                created = DateTime.Now,
                modified = DateTime.Now
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryPostDto dto)
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
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest(new
            {
                code = "Id",
                description = "Invalid input, please input something other than zero"
            });

            Category existed = await _unit.CategoryRepository.GetByIdAsync(id, null);
            if (existed is null) return NotFound();

            await _unit.CategoryRepository.DeleteAsync(existed);
            return Ok(id);
        }
    }
}
