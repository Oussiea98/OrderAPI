namespace OrderApi.Models
{
    public class Bestelling
    {
        public Guid Id { get; set; } 
        public Guid Klant_Id { get; set; }
        public int Product_Id { get; set; }
    }
}
