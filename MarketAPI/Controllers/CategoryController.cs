using MarketAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarketAPI.Models;

namespace MarketAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly DataContext dataContext;

    public CategoryController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpPost]
    [Route("")]
    public Category Store(string c_type)
    {
        Category category = new Category()
        {
            Type = c_type
        };

        dataContext.Categories.Add(category);
        dataContext.SaveChanges();

        return category;
    }

    [HttpGet]
    [Route("")]
    public List<Category> Index()
    {
        List<Category> categories = new List<Category>();
        categories = dataContext.Categories.ToList();

        return categories;
    }

    [HttpGet]
    [Route("{id}")]
    public string Show(int id)
    {
        var category = dataContext.Categories.FirstOrDefault(c => c.Id == id);
        if(category is null)
        {
            return "Category is not found!";
        }

        return category.Type;
    }

    [HttpPut]
    [Route("{id}")]
    public string Update(int id, string c_type)
    {
        var category = dataContext.Categories.FirstOrDefault(c => c.Id == id);
        if(category is null)
        {
            return "Category is not found!";
        }

        category.Type = c_type;

        dataContext.SaveChanges();

        return category.Type;
    }
}
