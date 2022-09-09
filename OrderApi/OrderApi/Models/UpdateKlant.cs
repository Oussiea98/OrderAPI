namespace OrderApi.Models
{
    public class UpdateKlant
    {

        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public Guid Order_Id { get; set; }

    }
}
