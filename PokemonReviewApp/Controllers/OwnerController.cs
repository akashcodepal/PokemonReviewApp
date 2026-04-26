using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OwnerController: Controller
  {
    private readonly IOwnerRepository _ownerRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
    {
      _ownerRepository = ownerRepository;
      _countryRepository = countryRepository;
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

    [HttpPost]
    public IActionResult CreateOwner([FromBody] OwnerDto ownerPayload)
    {
      if(ownerPayload == null) return BadRequest();
      if(!ModelState.IsValid) return BadRequest();

      var ownerName = (ownerPayload.FirstName + " " + ownerPayload.LastName).Trim().ToUpper();
      var owner = _ownerRepository.OwnerExistsByName(ownerName);

      if (owner)
      {
        ModelState.AddModelError("", "Owner Already Exists");
        return StatusCode(422, ModelState);
      }

      if (!_countryRepository.CountryExists(ownerPayload.CountryId))
      {
          ModelState.AddModelError("", "Country does not exist");
          return BadRequest(ModelState);
      }

      var ownerMap = _mapper.Map<Owner>(ownerPayload);

      if (!_ownerRepository.CreateOwner(ownerMap))
      {
        ModelState.AddModelError("", "Something went wrong while Saving.");
        return StatusCode(500, ModelState);
      }

      return Ok("SuccessFully Created.");
    }

    [HttpPut]
    public IActionResult UpdateOwner([FromBody] OwnerDto ownerPayload)
    {
      if(ownerPayload == null) return BadRequest();
      if(!ModelState.IsValid) return BadRequest();
      if (!_ownerRepository.OwnerExists(ownerPayload.Id)) return NotFound();

      var ownerName = (ownerPayload.FirstName + " " + ownerPayload.LastName).Trim().ToUpper();
      var owner = _ownerRepository.OwnerExistsByName(ownerName);

      if (owner)
      {
        ModelState.AddModelError("", "Owner Already Exists");
        return StatusCode(422, ModelState);
      }

      if (!_countryRepository.CountryExists(ownerPayload.CountryId))
      {
          ModelState.AddModelError("", "Country does not exist");
          return BadRequest(ModelState);
      }

      var ownerMap = _mapper.Map<Owner>(ownerPayload);

      if (!_ownerRepository.UpdateOwner(ownerMap))
      {
        ModelState.AddModelError("", "Something went wrong while Saving.");
        return StatusCode(500, ModelState);
      }

      return Ok("SuccessFully Updated.");
    }
  }
}