using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Models;

namespace OrderApi.Controllers
{
    [Route("api/bestelling")]
    [ApiController]
    public class BestellingController : ControllerBase
    {

        // Deze dbcontext property is aangemaakt om met de InMemory database te praten
        private readonly OrderAPIDbContext dbContext;

        public BestellingController(OrderAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        // Deze methode haalt alle bestellingen op
        public async Task<IActionResult> GetBestelling()
        {
            

            var set = dbContext.Set<Bestelling>();
            if (set.Any()) return Ok(await dbContext.Bestellingen.ToListAsync());

            var g = "00000000-0000-0000-0000-000000000000";
            Guid newGuid = Guid.Parse(g);
            var bestelling = new List<Bestelling>
            {
                new Bestelling
                {
                    Klant_Id = newGuid,
                    Product_Id = 1
                }
            };


            return Ok(bestelling);
        }

        // Deze methode voegt nieuwe bestelling toe
        [HttpPost]
        public async Task<IActionResult> AddBestelling(AddBestelling AddBestelling)
        {
            var bestelling = new Bestelling()
            {
                Klant_Id = AddBestelling.Klant_Id,
                Product_Id = AddBestelling.Product_Id

            };

            await dbContext.Bestellingen.AddAsync(bestelling);
            await dbContext.SaveChangesAsync();

            return Ok(bestelling);
        }

        // Deze methode wijzigt bestellingen op basis van de id
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditBestelling([FromRoute] Guid id, UpdateBestelling updatebestelling)
        {
            // De id wordt gezocht in de Bestellingen tabel
            var bestelling = await dbContext.Bestellingen.FindAsync(id);
            // Als de id gevonden is dan wordt de informatie gewijzigd
            if (bestelling != null)
            {
                bestelling.Product_Id = updatebestelling.Product_Id;
                bestelling.Klant_Id = updatebestelling.Klant_Id;

                await dbContext.SaveChangesAsync();

                return Ok(bestelling);
            }

            // Als de id niet gevonden is geeft het NotFound 
            return NotFound();
        }

        //Deze methode verwijderd een bestelling obv de id
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBestelling([FromRoute] Guid id)
        {
            var bestelling = await dbContext.Bestellingen.FindAsync(id);

            // Als er een klant is gevonden met de id
            if (bestelling != null)
            {
                dbContext.Remove(bestelling);
                await dbContext.SaveChangesAsync();
                return Ok(bestelling);
            }

            return NotFound();

        }

        
    }
}
