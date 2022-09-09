using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models
{
    public class AddKlant
    {
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Achternaam { get; set; }
    }
}
