using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models
{
    public class AddBestelling
    {
        [Required]
        public Guid Klant_Id { get; set; }
        [Required]
        public int Product_Id { get; set; }
    }
}
