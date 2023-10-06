namespace API_REST.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int IdLector { get; set; }
        public int IdLibro { get; set; }
        public string FechaPrestamo { get; set; }
        public string FechaDevolucion { get; set; }
        public bool Devuelto { get; set; }

        public override string ToString() => $"IdLector: {IdLector}, IdLibro: {IdLibro}, FechaPrestamo: {FechaPrestamo}, FechaDevolucion: {FechaDevolucion}, Devuelto: {Devuelto}";

    }
}
