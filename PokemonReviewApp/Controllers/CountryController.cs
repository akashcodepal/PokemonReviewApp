using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CountryController : Controller
  {
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    public CountryController(ICountryRepository countryRepository, IMapper mapper)
    {
      _countryRepository = countryRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCountries()
    {
      var countries = _countryRepository.GetCountries();
      var mappedCountries = _mapper.Map<List<CountryDto>>(countries);
      return Ok(mappedCountries);
    }

    [HttpGet("{id}")]
    public IActionResult GetCountry(int id)
    {
      if (!_countryRepository.CountryExists(id))
      {
        return NotFound();
      }
      var country = _countryRepository.GetCountry(id);
      
      var mappedCountry = _mapper.Map<CountryDto>(country);
      return Ok(mappedCountry);
    }

    [HttpGet("getCountryByOwner/{countryId}")]
    public IActionResult GetCountryByOwner(int countryId)
    {
      var country = _countryRepository.GetCountryByOwner(countryId);
      var mappedCountry = _mapper.Map<Country>(country);
      return Ok(mappedCountry);
    }

    [HttpGet("getOwnersFromCountry/{countryId}")]
    public IActionResult GetOwnersFromCountry(int countryId)
    {
      var owners = _countryRepository.GetOwnersFromCountry(countryId);
      return Ok(owners); 
    }

    [HttpPost]
    public IActionResult CreateCountry([FromBody] CountryDto countryPayload)
    {
      if(countryPayload == null)
      {
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      if (_countryRepository.CountryExistsByName(countryPayload.Name))
      {
        ModelState.AddModelError("", "Country Already Exists.");
        return StatusCode(422, ModelState);
      }

      var countryMap = _mapper.Map<Country>(countryPayload);

      if (!_countryRepository.CreateCountry(countryMap))
      {
        ModelState.AddModelError("", "Something went wrong while Saving.");
        return StatusCode(500, ModelState);
      }

      return Ok("SuccessFully created.");
    }

    [HttpPut]
    public IActionResult UpdateCountry([FromBody] CountryDto countryPayload)
    {
      if(countryPayload == null)
      {
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      if (!_countryRepository.CountryExists(countryPayload.id))
      {
        return NotFound();
      }

      if (_countryRepository.CountryExistsByName(countryPayload.Name))
      {
        ModelState.AddModelError("", "Country Already Exists.");
        return StatusCode(422, ModelState);
      }

      var countryMap = _mapper.Map<Country>(countryPayload);

      if (!_countryRepository.UpdateCountry(countryMap))
      {
        ModelState.AddModelError("", "Something went wrong while Saving.");
        return StatusCode(500, ModelState);
      }

      return Ok("SuccessFully updated.");
    }

  }
}