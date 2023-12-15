namespace Sistema_Web_Casa_Surá.Entities
{
    public class FacturaObj
    {
        public string IDUSUARIO { get; set; } = String.Empty;

        public string Producto { get; set; } = String.Empty;

        public int CANTCOMPRADA { get; set; } = 0;

        public decimal Precio { get; set; } = 0;

        public decimal TOTAL_LINEA { get; set; } = 0;

        public int IDPRODUCTO { get; set; } = 0;

        public decimal Total { get; set; } = 0;

        public string Detalle { get; set; } = String.Empty;
    }
    public class FacturaObj2
    {
        public int Cantidad { get; set; }

        public decimal Total { get; set; } = 0;
        public decimal TotalDia { get; set; } = 0;
    }
}
