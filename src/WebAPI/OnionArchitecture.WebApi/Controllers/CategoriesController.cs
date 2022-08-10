using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.DTOs.Categories;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;


namespace OnionArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
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

            Category category = await _repository.GetByIdAsync(id,null);
            if (category is null) return NotFound();

            CategoryItemDto dto = _mapper.Map<CategoryItemDto>(category);
            return Ok(dto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            List<Category> categories = await _repository.GetAllAsync(null);

            List<CategoryItemDto> dtos = _mapper.Map<List<CategoryItemDto>>(categories);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryPostDto dto)
        {
            Category category = _mapper.Map<Category>(dto);
            category.CreatedAt = DateTime.Now;
            category.ModifiedAt = DateTime.Now;

            await _repository.AddAsync(category);

            return StatusCode(StatusCodes.Status201Created, new
            {
                category = dto,
                created = DateTime.Now,
                modified = DateTime.Now
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,CategoryPostDto dto)
        {
            Category existed = await _repository.GetByIdAsync(id, null);
            if (existed is null) return NotFound();
            _repository.Update(existed);
            existed.Name = dto.Name;
            existed.ModifiedAt = DateTime.Now;
            await _repository.SaveChangesAsync();
            return Ok(new
            {
                category=dto,
                modified=DateTime.Now
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

            Category existed = await _repository.GetByIdAsync(id, null);
            if (existed is null) return NotFound();
                
            await _repository.DeleteAsync(existed);
            return Ok(id);
        }
    }
}
