using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, ICategoryRepository categoryRepository , IOwnerRepository ownerRepository,IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokenmons = _mapper.Map<List<PokemonListResponseBo>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokenmons);
        }

        [HttpGet("{pokeId}")]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokenmon(pokeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId)) return NotFound();

            var rating = _pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rating);
        }

        [HttpPost]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonPayload)
        {
        if(pokemonPayload == null) return BadRequest();
        if(!ModelState.IsValid) return BadRequest();

        var pokemon = _pokemonRepository.PokemonExistsByName(pokemonPayload.Name);

        if (pokemon)
        {
            ModelState.AddModelError("", "Pokemon Already Exists");
            return StatusCode(422, ModelState);
        }

        if (!_ownerRepository.OwnerExists(ownerId))
        {
            ModelState.AddModelError("", "Owner does not exist");
            return BadRequest(ModelState);
        }

        if (!_categoryRepository.CategoryExists(categoryId))
        {
            ModelState.AddModelError("", "Category does not exist");
            return BadRequest(ModelState);
        }

        var pokemonMap = _mapper.Map<Pokemon>(pokemonPayload);

        if (!_pokemonRepository.CreatePokemon(pokemonMap, ownerId, categoryId))
        {
            ModelState.AddModelError("", "Something went wrong while Saving.");
            return StatusCode(500, ModelState);
        }

        return Ok("SuccessFully Created.");
        }
    }
}
