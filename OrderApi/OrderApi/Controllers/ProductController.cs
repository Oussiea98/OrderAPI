using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using OrderApi.Models;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Deze dbcontext property is aangemaakt om met de InMemory database te praten
        private readonly OrderAPIDbContext dbContext;

        public ProductController(OrderAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducten()
        {
            var producten = new List<Product>
            {
                new Product{

                    Id = 1,
                    Naam = "Hamburger"
                },

                new Product{

                    Id = 2,
                    Naam = "Frietje met mayo"
                },

                new Product{

                    Id = 3,
                    Naam = "Pasta zalm"
                },

                new Product{

                    Id = 4,
                    Naam = "Kipnuggets"
                }
            };

            return Ok(producten);

        }
    }
}
