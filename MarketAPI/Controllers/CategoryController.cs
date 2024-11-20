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
    public IActionResult Store(string c_type)
    {
        Category category = new Category()
        {
            Type = c_type
        };

        dataContext.Categories.Add(category);
        dataContext.SaveChanges();

        return Ok("Category added");
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
    public IActionResult Show(int id)
    {
        var category = dataContext.Categories.FirstOrDefault(c => c.Id == id);
        if(category is null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, string c_type)
    {
        var category = dataContext.Categories.FirstOrDefault(c => c.Id == id);
        if(category is null)
        {
            return NotFound();
        }

        category.Type = c_type;

        dataContext.SaveChanges();

        return Ok();
    }


    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        var category = dataContext.Categories.FirstOrDefault(c => c.Id == id);
        if(category is not null)
        {
            dataContext.Categories.Remove(category);
            dataContext.SaveChanges();
        }

        return NoContent();
    }
}
