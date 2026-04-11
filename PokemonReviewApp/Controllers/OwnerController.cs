using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OwnerController: Controller
  {
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;
    public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
    {
      _ownerRepository = ownerRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetOwners()
    {
      var owners = _ownerRepository.GetOwners();
      var mappedOwners = _mapper.Map<List<OwnerDto>>(owners);
      return Ok(mappedOwners);
    }

    [HttpGet("{id}")]
    public IActionResult GetOwner(int id)
    {
      if(!_ownerRepository.OwnerExists(id))
      {
        return NotFound();
      }
      var owner = _ownerRepository.GetOwner(id);
      var mappedOwner = _mapper.Map<OwnerDto>(owner);
      return Ok(mappedOwner);
    }

    [HttpGet("GetOwnersOfPokemon/{pokeId}")]
    public IActionResult GetOwnersOfPokemon(int pokeId)
    {
      var owners = _ownerRepository.GetOwnersOfPokemon(pokeId);
      var mappedOwners = _mapper.Map<List<OwnerDto>>(owners);
      return Ok(mappedOwners);
    }

    [HttpGet("GetPokemonByOwner/{ownerId}")]
    public IActionResult GetPokemonByOwner(int ownerId)
    {
      var pokemon = _ownerRepository.GetPokemonByOwner(ownerId);
      var mappedPokemon = _mapper.Map<List<PokemonDto>>(pokemon);
      return Ok(mappedPokemon);
    }
  }
}