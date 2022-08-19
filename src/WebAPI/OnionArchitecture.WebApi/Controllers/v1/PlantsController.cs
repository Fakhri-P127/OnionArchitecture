using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.DTOs.Plants;
using OnionArchitecture.Application.Filters;
using OnionArchitecture.Application.Filters.Plant;
using OnionArchitecture.Application.Interfaces;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Application.Utilities;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Persistance;

namespace OnionArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]

    public class PlantsController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public PlantsController(IUnitOfWork unit, IMapper mapper)
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
                description = "Id is invalid, you can't enter 0."
            });

            Plant existed = await _unit.PlantRepository.GetByIdAsync(id, null, "PlantCategories");
            if (existed is null) return NotFound();

            PlantItemDto dto = _mapper.Map<PlantItemDto>(existed);
            return Ok(existed);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Plant> plants = await _unit.PlantRepository
                .GetAllAsync(null, "PlantCategories");

            List<PlantItemDto> dtos = _mapper.Map<List<PlantItemDto>>(plants);
            return Ok(plants.Select(plant => new { Plant = plant }));
        }
        [HttpPost]
        [PlantFilters]
        public async Task<IActionResult> Create(PlantPostDto dto)
        {
            dto = await UtilityClass.RemoveDuplicateCategories(dto, _unit.CategoryRepository);
            if (dto.PlantCategories.Count == 0 || dto.PlantCategories is null) return BadRequest(new
            {
                code = "No Category",
                description = "The categories you entered doesn't exist"
            });
            Plant plant = _mapper.Map<Plant>(dto);
            plant.CreatedAt = DateTime.Now;
            plant.ModifiedAt = DateTime.Now;
            plant.PlantCategories.ForEach(x =>
            {
                x.CreatedAt = DateTime.Now;
                x.ModifiedAt = DateTime.Now;
            });

            await _unit.PlantRepository.AddAsync(plant);

            return StatusCode(StatusCodes.Status201Created, new
            {
                plant = dto,
                created = plant.CreatedAt,
                modified = plant.ModifiedAt
            });
        }

        [HttpPost]
        [Route("/api/v2/[controller]")]
        [PlantFilters]
        public async Task<IActionResult> CreateV2(PlantPostDto dto)
        {
            dto = await UtilityClass.RemoveDuplicateCategories(dto, _unit.CategoryRepository);
            if (dto.PlantCategories.Count == 0 || dto.PlantCategories is null) return BadRequest(new
            {
                code = "No Category",
                description = "The categories you entered doesn't exist"
            });
            Plant plant = _mapper.Map<Plant>(dto);
            plant.CreatedAt = DateTime.Now;
            plant.ModifiedAt = DateTime.Now;
            plant.PlantCategories.ForEach(x =>
            {
                x.CreatedAt = DateTime.Now;
                x.ModifiedAt = DateTime.Now;
            });

            await _unit.PlantRepository.AddAsync(plant);

            return StatusCode(StatusCodes.Status201Created, new
            {
                plant = dto,
                created = plant.CreatedAt,
                modified = plant.ModifiedAt
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, PlantPostDto dto)
        {
            Plant existed = await _unit.PlantRepository.GetByIdAsync(id, null, "PlantCategories", "PlantCategories.Category");
            if (existed is null) return NotFound();

            dto = await UtilityClass.RemoveDuplicateCategories(dto, _unit.CategoryRepository);
            if (dto.PlantCategories.Count == 0 || dto.PlantCategories is null) return BadRequest(new
            {
                code = "No Category",
                description = "The categories you entered doesn't exist"
            });
            _unit.PlantRepository.Update(existed);
            existed.Name = dto.Name;
            existed.Description = dto.Description;
            existed.Price = dto.Price;
            existed.SKU = dto.SKU;
            existed.ModifiedAt = DateTime.Now;

            existed.PlantCategories = _mapper.Map<List<PlantCategory>>(dto.PlantCategories);
            existed.PlantCategories.ForEach(x => x.ModifiedAt = DateTime.Now);

            await _unit.SaveChangesAsync();
            return Ok(new
            {
                plant = dto,
                created = existed.CreatedAt,
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
            Plant existed = await _unit.PlantRepository.GetByIdAsync(id, null);
            if (existed is null) return NotFound();

            await _unit.PlantRepository.DeleteAsync(existed);
            return Ok(id);
        }



    }
}
