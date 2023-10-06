namespace API_REST.Models
{
    public class Estudiante
    {
        public int IdLector { get; set; }
        public string CI { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Carrera { get; set; }
        public int Edad { get; set; }


        public override string ToString() => $"Idlector: {IdLector}, CI: {CI}, Nombre: {Nombre}, Direccion: {Direccion}, Carrera: {Carrera}, Edad: {Edad}";
    
    }
}
