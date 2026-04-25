using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

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

    [HttpPost]
    public IActionResult CreateReviewer(ReviewerDto reviewerPayload)
    {
      if(reviewerPayload == null) return BadRequest();
      if(!ModelState.IsValid) return BadRequest();

      if(_reviewerRepository.ReviewExistsByName(reviewerPayload.FirstName + " " + reviewerPayload.LastName))
      {
        ModelState.AddModelError("", "Reviewer Already Exists.");
        return StatusCode(422, ModelState);
      }

      var reviewerMap = _mapper.Map<Reviewer>(reviewerPayload);
      if(!_reviewerRepository.CreateReviewer(reviewerMap))
      {
        ModelState.AddModelError("", "Something went wrong while Saving.");
        return StatusCode(500, ModelState);
      }

      return Ok("SuccessFully Created.");
    }
  }
}