using MarketAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarketAPI.Models;
using Bogus;
using Bogus.DataSets;

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
    public IActionResult Store(string p_name, int p_quantity, decimal p_price)
    {
        Product product = new Product()
        {
            Name = p_name,
            Quantity = p_quantity,
            Price = p_price,
        };

        dataContext.Products.Add(product);
        dataContext.SaveChanges();
        
        return Ok("Product added!");
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        // create fake products
        var faker = new Faker<Product>("en")
            .RuleFor(p => p.Name, f => f.Commerce.Product())
            .RuleFor(p => p.Quantity, f => Random.Shared.Next(0, 50))
            .RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price()));
            
        var fakeProducts = faker.Generate(40);

        // database save
        dataContext.Products.AddRange(fakeProducts);
        dataContext.SaveChanges();

        // return result
        var products = dataContext.Products.ToList();

        return Ok(products);
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
    public IActionResult Update(int id, string p_name, int p_quantity, decimal p_price)
    {
        var product = dataContext.Products.FirstOrDefault(p => p.Id == id);
        if(product == null)
        {
            return NotFound();
        }
        product.Name = p_name;
        product.Quantity = p_quantity;
        product.Price = p_price;

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
