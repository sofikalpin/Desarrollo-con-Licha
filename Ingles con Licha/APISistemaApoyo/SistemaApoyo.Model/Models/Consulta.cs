using System;
using System.Collections.Generic;

namespace SistemaApoyo.Model.Models;

public partial class Consulta
{
    public int Idconsulta { get; set; }

    public string Contenido { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public DateTime Hora { get; set; }

    public string Titulo { get; set; } = null!;

    public int Idusuario { get; set; }

    public virtual ICollection<Foro> Foros { get; set; } = new List<Foro>();

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;

    public virtual ICollection<Respuesta> Respuesta { get; set; } = new List<Respuesta>();
}
