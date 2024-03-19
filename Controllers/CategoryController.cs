using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_GB.Models;

namespace Store_GB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
      
        [HttpGet("getCategories")]
       public IActionResult GetCategory()
        {
            try 
            { 
                using(var context = new ProductContext())
                {
                    var category = context.Categories.Select(x=> new Category()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    });
                    context.SaveChanges();
                    return Ok(category);
                }
            }
            catch 
            {
                return StatusCode(500);
            }
        }


        //
        [HttpPost("putCategory")]
        public IActionResult PutCategory([FromQuery]string name, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Categories.Any(x => x.Name.ToLower().Equals(name)))
                    {
                        context.Add(new Category()
                        {
                            Name = name,
                            Description = description                       
                        });
                        context.SaveChanges();
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




        [HttpPost("removeCategory")]
        public IActionResult RemoveCategory([FromQuery] int categoryId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                   var category = context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

                    if (category != null) {
                        context.Remove(category);
                    }
                    context.SaveChanges();
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
