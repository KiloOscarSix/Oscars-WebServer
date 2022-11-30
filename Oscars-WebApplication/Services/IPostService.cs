using Oscars_WebApplication.Domain;

namespace Oscars_WebApplication.Services;

public interface IPostService
{
    List<Post> GetPosts();

    Post? GetPostById(Guid postId);
}