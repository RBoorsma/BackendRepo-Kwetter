using System.Collections.ObjectModel;
using KweetService.Core.Service;
using KweetService.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace KweetService.Controllers;

[Route("[controller]")]
[ApiController]
public class KweetController(IKweetService kweetService) : ControllerBase
{
    [HttpPost("GetKweets")]
    public async Task<IActionResult> GetKweets([FromBody] Profile profileID)
    {
        List<KweetModel>? kweets = await kweetService.GetKweetsByProfileAsync(profileID);
        if (kweets.Count != null)
        {
            return NotFound();
        }
        
        return Ok(kweets);
    }
    
    [HttpPost("GetKweet")]
    public async Task<IActionResult> GetKweet([FromBody] Guid guid)
    {
        KweetModel? kweet = await kweetService.GetKweetByID(guid);
        if (kweet == null)
        {
            return NotFound();
        }
        return Ok(kweet); 
    }
    
    

    [HttpPost("AddKweet")]
    public async Task<IActionResult> AddKweet([FromBody] KweetModel kweet)
    {
        if (await kweetService.AddKweetAsync(kweet))
            return Created();

        return StatusCode(500);
    }
    
    
}