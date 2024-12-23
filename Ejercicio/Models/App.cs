using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio.Models
{
    public class App
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; } 

        public DateTime FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
