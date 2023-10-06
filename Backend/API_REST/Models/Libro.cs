namespace API_REST.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Editorial { get; set; }
        public string Area { get; set; }

        public override string ToString() => $"Id: {Id}, Titulo: {Titulo}, Editorial: {Editorial}, Area: {Area}";
        
    }
}
