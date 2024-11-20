using MarketAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarketAPI.Models;

namespace MarketAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DataContext dataContext;

    public UserController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Store(string user_firstname, string user_lastname, int user_age, string user_city)
    {
        User user = new User()
        {
            FirstName = user_firstname,
            LastName = user_lastname,
            Age = user_age,
            City = user_city
        };

        dataContext.Users.Add(user);
        dataContext.SaveChanges();

        return Ok("User added!");
    }

    [HttpGet]
    [Route("")]
    public List<User> Index()
    {
        List<User> user = dataContext.Users.ToList();

        return user;
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Show(int id)
    {
        var user = dataContext.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update (int id, string user_firstname, string user_lastname, int user_age, string user_city)
    {
        var user = dataContext.Users.FirstOrDefault(u => u.Id == id);
        if(user is null)
        {
            return NotFound();
        }

        user.FirstName = user_firstname;
        user.LastName = user_lastname;
        user.Age = user_age;
        user.City = user_city;

        dataContext.SaveChanges();

        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        var user = dataContext.Users.FirstOrDefault(u => u.Id == id);
        if(user is not null)
        {
            dataContext.Users.Remove(user);
            dataContext.SaveChanges();
        }

        return NoContent();
    }
}
