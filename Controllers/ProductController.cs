using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_GB.Models;

namespace Store_GB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
      
        [HttpGet("getProducts")]
       public IActionResult GetProducts()
        {
            try 
            { 
                using(var context = new ProductContext())
                {
                    var products = context.Products.Select(x=> new Product()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    });
                    return Ok(products);
                }
            }
            catch 
            {
                return StatusCode(500);
            }
        }


        //
        [HttpPost("putProduct")]
        public IActionResult PutProduct([FromQuery]string name, string description, int categoryID, int cost)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Products.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Product()
                        {
                            Name = name,
                            Description = description,
                            Cost = cost,
                            CategoryId = categoryID
                        });
                        return Ok();
                    }
                    return StatusCode(409);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }




        [HttpPost("removeProduct")]
        public IActionResult RemoveProduct([FromQuery] int productId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                   var product = context.Products.FirstOrDefaultAsync(x => x.Id == productId);

                    if (product != null) {
                        context.Remove(product);
                    }
                    return StatusCode(409);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("changeCostProduct")]
        public IActionResult ChangeCostProduct([FromQuery] int productId, int newCost)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var product = context.Products.FirstOrDefault(x => x.Id == productId);

                    if (product != null)
                    {
                      product.Cost = newCost;
                    }
                    return StatusCode(409);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


    }
}
