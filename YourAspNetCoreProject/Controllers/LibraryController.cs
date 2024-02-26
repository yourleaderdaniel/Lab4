using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

[Route("[controller]")]
public class LibraryController : Controller
{
    private IConfiguration _configuration;

    public LibraryController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return Content("Greetings fellow book enjoyer!!!!");
    }

    [HttpGet("Books")]
    public IActionResult Books()
    {
        var booksConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("books.json")
            .Build();

        var books = booksConfig.GetSection("Books").Get<List<string>>();
        return Content(string.Join("\n", books)); 
    }


    [HttpGet("Profile/{id?}")]
    public IActionResult Profile(int? id)
    {
        
        var userId = id.GetValueOrDefault(0); // 0 это id профиля по умолчанию

        var profilesConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("profiles.json")
            .Build();

        
        var profile = profilesConfig.GetSection($"Profiles:{userId}").Get<string>();
        return Content(string.IsNullOrEmpty(profile) ? "Profile not found" : profile);
    }


}
