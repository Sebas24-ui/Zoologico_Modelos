using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoologico_Modelos
{
    public class Raza
    {
        public int Id { get; set; }
        public string NombreRaza { get; set; }
        public List<Animal>? Animales { get; set; } = new List<Animal>();

    }
}
