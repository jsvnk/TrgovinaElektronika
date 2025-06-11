namespace TrgovinaElektronika.Models
{
    public class Izdelek
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public int Zaloga { get; set; }
    }
}
