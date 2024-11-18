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
    public string Store(string p_name, string p_quantity, int p_price, string p_country)
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
        
        return $"{p_name} dan {p_country} da ishlab chiqarilgan {p_price} so`m narxda {p_quantity} dona qo`shildi!";
    }

    [HttpGet]
    [Route("")]
    public List<Product> Index()
    {
        return dataContext.Products.ToList();
    }

    [HttpGet]
    [Route("{id}")]
    public string Show(int id)
    {
        var product = dataContext.Products.FirstOrDefault(product => product.Id == id);
        if(product == null)
        {
            return "Product not found!";
        }

        return product.Name;
    }

    [HttpPut]
    [Route("{id}")]
    public string Update(int id, string p_name, string p_quantity, int p_price, string p_country)
    {
        var product = dataContext.Products.FirstOrDefault(p => p.Id == id);
        if(product == null)
        {
            return "Not found";
        }
        product.Name = p_name;
        product.Quantity = p_quantity;
        product.Price = p_price;
        product.Country = p_country;

        dataContext.SaveChanges();

        return $"Product with {id} updated";
    }

    [HttpDelete]
    [Route("{id}")]
    public string Delete(int id)
    {
        var product = dataContext.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            dataContext.Products.Remove(product);
            dataContext.SaveChanges();
        }

        return "Deleted!";
    }
}
