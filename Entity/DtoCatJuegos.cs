using System.Data.SqlTypes;

namespace Entity
{
    public class DtoCatJuegos
    {
        public int IdJuego { get; set; }
        public string NombreJ { get; set; }
        public decimal precioJ { get; set; }
        public string FecEstrenoJ { get; set;}
        public string CategoriaJ { get; set; }
        public int IdProv { get; set; }

    }
}

