using Microsoft.AspNetCore.Mvc;
using Oscars_WebApplication.Contracts.V1;
using Oscars_WebApplication.Contracts.V1.Requests;
using Oscars_WebApplication.Contracts.V1.Responses;
using Oscars_WebApplication.Domain;

namespace Oscars_WebApplication.Controllers.V1;

public class PostsController : Controller
{
    private readonly List<Post> _posts;

    public PostsController()
    {
        _posts = new List<Post>();

        for (int i = 0; i < 5; i++)
        {
            _posts.Add(new Post{Id = Guid.NewGuid().ToString()});
        }
    }

    [HttpGet(ApiRoutes.Posts.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_posts);
    }

    [HttpGet(ApiRoutes.Posts.Get)]
    public IActionResult Get(string postId)
    {
        return Ok();
    }
    
    [HttpPost(ApiRoutes.Posts.Create)]
    public IActionResult Create([FromBody] CreatePostRequest postRequest)
    {
        Post post = new(){Id = postRequest.Id};
        
        if (string.IsNullOrEmpty(post.Id))
        {
            post.Id = Guid.NewGuid().ToString();
        }

        _posts.Add(post);

        string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        string locationUrl = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}", post.Id)}";

        PostResponse response = new() {Id = post.Id};
        
        return Created(locationUrl, response);
    }
}