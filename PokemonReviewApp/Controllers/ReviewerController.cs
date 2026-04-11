using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ReviewerController : Controller
  {
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IMapper _mapper;
    public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
    {
      _reviewerRepository = reviewerRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetReviewers()
    {
      var reviewers = _reviewerRepository.GetReviewers();
      var mappedReviewers = _mapper.Map<List<ReviewerDto>>(reviewers);
      return Ok(mappedReviewers);
    }

    [HttpGet("{id}")]
    public IActionResult GetReviewer(int id)
    {
      if (!_reviewerRepository.ReviewExists(id))
      {
        return NotFound();
      }

      var reviewer = _reviewerRepository.GetReviewer(id);
      var mappedReviewer = _mapper.Map<ReviewerDto>(reviewer);
      return Ok(mappedReviewer);
    }

    [HttpGet("GetReviewsByReviewer/{reviewerId}")]
    public IActionResult GetReviewsByReviewer(int reviewerId)
    {
      var reviews = _reviewerRepository.GetReviewsByReviewer(reviewerId);
      var mappedReviews = _mapper.Map<List<ReviewDto>>(reviews);

      return Ok(mappedReviews);
    }

  }
}