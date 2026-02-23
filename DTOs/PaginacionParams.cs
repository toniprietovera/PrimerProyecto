namespace PrimerProyecto.DTOs
{
    public class PaginacionParams
    {
        public int Pagina { get; set; } = 1;
        public int TamanoPagina { get; set; } = 10;

        public bool? Completada { get; set; }
        public string? BuscarTexto { get; set; }
        public int GetTamanoPagina()
        {
            return TamanoPagina > 50 ? 50 : TamanoPagina; 
        }
    }
}