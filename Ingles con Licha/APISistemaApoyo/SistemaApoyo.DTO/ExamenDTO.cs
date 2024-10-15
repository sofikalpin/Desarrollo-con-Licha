using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaApoyo.DTO
{
    public class ExamenDTO
    {
        public int id_Examen { get; set; }
        public string? titulo { get; set; }
        public int duracion { get; set; }
        public int finalizacion { get; set; }
    }
}