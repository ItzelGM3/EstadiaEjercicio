using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPiAsist.Models
{
    [Table("App")]
    public class App
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }  

        public DateTime FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; }
    }
}
