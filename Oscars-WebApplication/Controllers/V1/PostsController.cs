using Microsoft.AspNetCore.Mvc;
using Oscars_WebApplication.Contracts;
using Oscars_WebApplication.Contracts.V1;
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
}