using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoologico_Modelos
{
    public class Especie
    {
        public int Id { get; set; }
        public string NombreEspecie { get; set; }
        public List<Animal>? Animales { get; set; } = new List<Animal>();

    }
}
