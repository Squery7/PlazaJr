namespace API_REST.Models
{
    public class LibAut
    {
        public int IdAutor { get; set; }
        public int IdLibro { get; set; }

        public override string ToString() => $"IdLibro: {IdLibro}, IdAutor: {IdAutor}";
    }
}
