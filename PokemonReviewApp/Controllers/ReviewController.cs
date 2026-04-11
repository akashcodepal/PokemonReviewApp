using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ReviewController : Controller
  {
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
    {
      _reviewRepository = reviewRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetReviews()
    {
      var reviews = _reviewRepository.GetReviews();
      var mappedReviews = _mapper.Map<List<ReviewDto>>(reviews);
      return Ok(mappedReviews);
    }

    [HttpGet("{id}")]
    public IActionResult GetReview(int id)
    {
      if (!_reviewRepository.ReviewExists(id))
      {
        return NotFound();
      }

      var review = _reviewRepository.GetReview(id);
      var mappedReview = _mapper.Map<ReviewDto>(review);
      return Ok(mappedReview);
    }

    [HttpGet("GetReviewsOfPokemon/{pokeId}")]
    public IActionResult GetReviewsOfPokemon(int pokeId)
    {
      var reviews = _reviewRepository.GetReviewsOfPokemon(pokeId);
      var mappedReviews = _mapper.Map<List<ReviewDto>>(reviews);

      return Ok(mappedReviews);
    }

  }
}