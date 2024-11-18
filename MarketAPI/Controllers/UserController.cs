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
    public User Store(string user_firstname, string user_lastname, int user_age, string user_city)
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

        return user;
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
    public string Show(int id)
    {
        var user = dataContext.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
        {
            return "User is not found!";
        }

        return user.FirstName;
    }

    [HttpPut]
    [Route("{id}")]
    public string Update (int id, string user_firstname, string user_lastname, int user_age, string user_city)
    {
        var user = dataContext.Users.FirstOrDefault(u => u.Id == id);
        if(user is null)
        {
            return "User is not found!";
        }

        user.FirstName = user_firstname;
        user.LastName = user_lastname;
        user.Age = user_age;
        user.City = user_city;

        dataContext.SaveChanges();

        return "User changes!";
    }

    [HttpDelete]
    [Route("{id}")]
    public string Delete(int id)
    {
        var user = dataContext.Users.FirstOrDefault(u => u.Id == id);
        if(user is null)
        {
            return "User is not found";
        }

        dataContext.Users.Remove(user);
        dataContext.SaveChanges();

        return "Deleted";
    }
}
