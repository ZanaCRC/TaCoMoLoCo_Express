namespace TaCoMoLoCo_Express.UI.ViewModel
{
    public class PlatoVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string NombreCategoria { get; set; }

        public int IdRestaurante { get; set; }
        public byte[] Image { get; set; }
    }

}
