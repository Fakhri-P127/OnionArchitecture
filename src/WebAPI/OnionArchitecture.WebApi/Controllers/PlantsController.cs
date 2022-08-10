using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.DTOs.Plants;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly IPlantRepository _repository;
        private readonly IMapper _mapper;

        public PlantsController(IPlantRepository repository,IMapper mapper)
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
                description = "Id is invalid, you can't enter 0."
            });

            Plant existed = await _repository.GetByIdAsync(id,"PlantCategories"
                ,"PlantCategories.Category");
            if (existed is null) return NotFound();

            PlantItemDto dto = _mapper.Map<PlantItemDto>(existed);
            return Ok(dto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Plant> plants = await _repository.GetAllAsync(null);

            List<PlantItemDto> dtos = _mapper.Map<List<PlantItemDto>>(plants);
            return Ok(dtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PlantPostDto dto)
        {
            Plant plant = _mapper.Map<Plant>(dto);
            plant.CreatedAt = DateTime.Now;
            plant.ModifiedAt = DateTime.Now;
            await _repository.AddAsync(plant);

            return StatusCode(StatusCodes.Status201Created, new
            {
                plant= dto,
                created= DateTime.Now,
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
            Plant existed = await _repository.GetByIdAsync(id);
            if (existed is null) return NotFound();

            await _repository.DeleteAsync(existed);
            return Ok(id);
        }
    }
}
