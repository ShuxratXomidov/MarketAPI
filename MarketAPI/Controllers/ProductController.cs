using MarketAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarketAPI.Models;

namespace MarketAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly DataContext dataContext;

    public ProductController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Store(string p_name, string p_quantity, int p_price, string p_country)
    {
        Product product = new Product()
        {
            Name = p_name,
            Quantity = p_quantity,
            Price = p_price,
            Country = p_country
        };

        dataContext.Products.Add(product);
        dataContext.SaveChanges();
        
        return Ok("Product added!");
    }

    [HttpGet]
    [Route("")]
    public List<Product> Index()
    {
        return dataContext.Products.ToList();
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Show(int id)
    {
        var product = dataContext.Products.FirstOrDefault(product => product.Id == id);
        if(product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, string p_name, string p_quantity, int p_price, string p_country)
    {
        var product = dataContext.Products.FirstOrDefault(p => p.Id == id);
        if(product == null)
        {
            return NotFound();
        }
        product.Name = p_name;
        product.Quantity = p_quantity;
        product.Price = p_price;
        product.Country = p_country;

        dataContext.SaveChanges();

        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        var product = dataContext.Products.FirstOrDefault(p => p.Id == id);
        if (product is not null)
        {
            dataContext.Products.Remove(product);
            dataContext.SaveChanges();
        }

        return NoContent();
    }
}
