using MessagePack;
using System;
using System.Collections.Generic;

namespace Agenda_Virtual.Models
{
    public partial class Visita
    {
        public int Id { get; set; }
        public string Alumno { get; set; } = null!;
        public string Padre { get; set; } = null!;
        public double Telefono { get; set; }
        public string Escuela { get; set; } = null!;
        public double Grado { get; set; }
        public string Municipio { get; set; } = null!;
        public string Observacion { get; set; } = null!;
    }
}
