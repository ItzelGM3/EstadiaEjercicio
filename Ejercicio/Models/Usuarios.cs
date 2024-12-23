using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Contrasena { get; set; }
        public virtual ICollection<App> Apps { get; set; }
    }
}
