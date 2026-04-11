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

  }
}