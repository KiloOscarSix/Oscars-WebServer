using Microsoft.AspNetCore.Mvc;
using Oscars_WebApplication.Contracts.V1;
using Oscars_WebApplication.Contracts.V1.Requests;
using Oscars_WebApplication.Contracts.V1.Responses;
using Oscars_WebApplication.Domain;
using Oscars_WebApplication.Services;

namespace Oscars_WebApplication.Controllers.V1;

public class PostsController : Controller
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpGet(ApiRoutes.Posts.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_postService.GetPosts());
    }

    [HttpGet(ApiRoutes.Posts.Get)]
    public IActionResult Get([FromRoute] Guid postId)
    {
        Post? post = _postService.GetPostById(postId);

        if (post is null)
        {
            return NotFound();
        }
        
        return Ok(post);
    }
    
    [HttpPost(ApiRoutes.Posts.Create)]
    public IActionResult Create([FromBody] CreatePostRequest postRequest)
    {
        Post post = new(){Id = postRequest.Id};
        
        if (post.Id == Guid.Empty)
        {
            post.Id = Guid.NewGuid();
        }

        _postService.GetPosts().Add(post);

        string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        string locationUrl = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString())}";

        PostResponse response = new() {Id = post.Id};
        
        return Created(locationUrl, response);
    }
}