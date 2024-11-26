using MarketAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarketAPI.Services;
using MarketAPI.Models;


namespace MarketAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Store([FromBody] UserCreateRequest newUser)
    {
        int id = this.userService.Create(newUser.FirstName, newUser.LastName, newUser.Age, newUser.City);
        return Ok(id);
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
       var users = this.userService.GetAll();
        return Ok(users);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update (int id, string firstName, string lastName, int age, string city)
    {
        var result = this.userService.Update(id, firstName, lastName, age, city);
 
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
       this.userService.Delete(id);

        return NoContent();
    }
}
