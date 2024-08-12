namespace TesteandoSRWebServer.Models
{
    public class Usuario
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Movil { get; set; }

        public Usuario() { }

        public Usuario(string nombre, string apellido, string movil) 
        { 
            Nombre = nombre;
            Apellido = apellido;
            Movil = movil;
        }

        public override string ToString()
        {
            return $"nombre: {Nombre}, apellido: {Apellido}, movil: {Movil}";
        }
    }
}
