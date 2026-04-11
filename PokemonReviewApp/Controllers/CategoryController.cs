using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryController : Controller
  {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
      _categoryRepository = categoryRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
      var categories = _categoryRepository.GetCategories();
      var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);
      return Ok(mappedCategories);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
      if(!_categoryRepository.CategoryExists(id))
      {
        return NotFound();
      }
      var category = _categoryRepository.GetCategory(id);
      var mappedCategory = _mapper.Map<CategoryDto>(category);
      return Ok(mappedCategory);
    }

    [HttpGet("GetPokemonByCategory/{categoryId}")]
    public IActionResult GetPokemonsByCategory(int categoryId)
    {
      var pokemon = _categoryRepository.GetPokemonsByCategory(categoryId);
      var mappedPokemons = _mapper.Map<List<PokemonDto>>(pokemon);
      return Ok(mappedPokemons);
    }
  }
}