using System;
using System.Collections.Generic;

namespace SistemaApoyo.Model.Models;

public partial class Respuesta
{
    public int Idrespuesta { get; set; }

    public string Contenido { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public DateTimeOffset Hora { get; set; }

    public int Idconsulta { get; set; }

    public int Idusuario { get; set; }

    public virtual Consulta IdconsultaNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
