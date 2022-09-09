using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Models;

namespace OrderApi.Controllers
{
    [ApiController]
    // De ApiController krijgt hier de route 'klant' van de 'KlantController'
    [Route("api/klant")]
    public class KlantController : Controller
    {
        // Deze dbcontext property is aangemaakt om met de InMemory database te praten
        private readonly OrderAPIDbContext dbContext;

        public KlantController(OrderAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Om gebruik te maken van SwaggerUI heb ik de HttpGet keyword gebruikt
        [HttpGet]
        // Deze methode haalt alle de klanten op
        public async Task<IActionResult>GetKlanten()
        {

            var set = dbContext.Set<Klant>();
            if (set.Any()) return Ok(await dbContext.Klanten.ToListAsync());
            else
            {
                var g = "00000000-0000-0000-0000-000000000000";
                Guid newGuid = Guid.Parse(g);
                var klanten = new List<Klant>
            {
                new Klant
                {
                    Voornaam = "Test123",
                    Achternaam = "Test456",
                    Order_Id = newGuid
                }
            };


                return Ok(klanten);
            }

           

        }

        // Deze methode haalt een klant op obv de id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetKlant([FromRoute] Guid id)
        {
            // De id wordt gezocht in de Klanten tabel
            var klant = await dbContext.Klanten.FindAsync(id);

            if (klant == null)
            {
                return NotFound();
            }

            return Ok(klant);
            
        }
        
        // Deze methode voegt nieuwe klanten toe
        [HttpPost]
        public async Task<IActionResult> AddKlant(AddKlant AddKlant)
        {
            var klant = new Klant()
            {
                Id = Guid.NewGuid(),
                Voornaam = AddKlant.Voornaam,
                Achternaam = AddKlant.Achternaam

            };

            await dbContext.Klanten.AddAsync(klant);
            await dbContext.SaveChangesAsync();

            return Ok(klant);
        }

        // Deze methode wijzigt klanten op basis van de id
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditKlant([FromRoute] Guid id, UpdateKlant updateklant)
        {
            // De id wordt gezocht in de Klanten tabel
            var klant = await dbContext.Klanten.FindAsync(id);
            // Als de id gevonden is dan wordt de informatie gewijzigd
            if (klant != null)
            {
                klant.Voornaam = updateklant.Voornaam;
                klant.Achternaam = updateklant.Achternaam;
                klant.Order_Id = updateklant.Order_Id;

                await dbContext.SaveChangesAsync();

                return Ok(klant);
            }

            // Als de id niet gevonden is geeft het NotFound 
            return NotFound();
        }

        //Deze methode verwijderd een klant obv de id
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteKlant([FromRoute] Guid id)
        {
            var klant = await dbContext.Klanten.FindAsync(id);

            // Als er een klant is gevonden met de id
            if (klant != null)
            {
                dbContext.Remove(klant);
                await dbContext.SaveChangesAsync();
                return Ok(klant);
            }

            return NotFound();

        }

       
    }
}
