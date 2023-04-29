using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RESTAPI.Models
{
    public partial class Clientes
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellidos { get; set; }
        public string Identificacion { get; set; }
        public string Mail { get; set; }
        public bool? Estado { get; set; }
        public string EstadoDesc { get; set; }
    }
}
