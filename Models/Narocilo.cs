namespace TrgovinaElektronika.Models
{
    public class Narocilo
    {
        public int Id { get; set; }
        public string UporabnikIme { get; set; }
        public DateTime Datum { get; set; }
        public List<PostavkaKosarice> Postavke { get; set; }
        public decimal Skupaj => Postavke.Sum(p => p.Cena * p.Kolicina);
    }
}
