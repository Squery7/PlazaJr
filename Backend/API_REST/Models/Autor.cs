namespace API_REST.Models
{
    public class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }

        public override string ToString() => $"IdAutor: {IdAutor}, Nombre: {Nombre}, Nacionalidad: {Nacionalidad}";

    }
}
