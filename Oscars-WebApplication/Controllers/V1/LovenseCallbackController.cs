using Microsoft.AspNetCore.Mvc;
using Oscars_WebApplication.Contracts.V1;
using Oscars_WebApplication.Contracts.V1.Requests;
using Oscars_WebApplication.Contracts.V1.Responses;
using Oscars_WebApplication.Domain;

namespace Oscars_WebApplication.Controllers.V1;

public class LovenseCallbackController : Controller
{
    private readonly Dictionary<string, LovenseUser> _users;

    public LovenseCallbackController()
    {
        _users = new Dictionary<string, LovenseUser>();
    }

    [HttpGet(ApiRoutes.LovenseCallback.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_users);
    }
    
    [HttpGet(ApiRoutes.LovenseCallback.Get)]
    public IActionResult Get(string userId)
    {
        return Ok(_users[userId]);
    }
    
    [HttpPost(ApiRoutes.LovenseCallback.Create)]
    public IActionResult Create([FromBody] LovenseCallbackRequest callbackRequest)
    {
        if (callbackRequest.Uid is null || callbackRequest.Toys is null)
        {
            return BadRequest();
        }
        
        Dictionary<string, LovenseToy> userToys = callbackRequest.Toys.Values.ToDictionary(toy => toy.Id,
            toy => new LovenseToy
            {
                Id = toy.Id,
                Nickname = toy.Nickname,
                Name = toy.Name,
                Status = toy.Status
            });

        LovenseUser user = new()
        {
            Uid = callbackRequest.Uid,
            AppVersion = callbackRequest.AppVersion,
            Toys = userToys,
            WssPort = callbackRequest.WssPort,
            HttpPort = callbackRequest.HttpPort,
            WsPort = callbackRequest.WsPort,
            AppType = callbackRequest.AppType,
            Domain = callbackRequest.Domain,
            UToken = callbackRequest.UToken,
            HttpsPort = callbackRequest.HttpsPort,
            Version = callbackRequest.Version,
            Platform = callbackRequest.Platform
        };

        _users.Add(user.Uid, user);

        string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        string locationUrl = $"{baseUrl}/{ApiRoutes.LovenseCallback.Get.Replace("{userId}", user.Uid)}";

        Dictionary<string, LovenseToyResponse> userToyResponse = user.Toys.Values.ToDictionary(toy => toy.Id,
            toy => new LovenseToyResponse
            {
                Id = toy.Id,
                Nickname = toy.Nickname,
                Name = toy.Name,
                Status = toy.Status
            })!;
        
        LovenseUserResponse response = new()
        {
            Uid = user.Uid,
            AppVersion = user.AppVersion,
            Toys = userToyResponse,
            WssPort = user.WssPort,
            HttpPort = user.HttpPort,
            WsPort = user.WsPort,
            AppType = user.AppType,
            Domain = user.Domain,
            UToken = user.UToken,
            HttpsPort = user.HttpsPort,
            Version = user.Version,
            Platform = user.Platform
        };
        
        return Created(locationUrl, response);
    }
}