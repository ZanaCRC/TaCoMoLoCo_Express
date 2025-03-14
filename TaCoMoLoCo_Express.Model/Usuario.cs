﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaCoMoLoCo_Express.Model
{
    public class Usuario
    {
        [Key]
        public string Cedula { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int IdDireccion { get; set; }
        public EnumRol IdRol { get; set; }
        public Rol Rol { get; set; }

    }
}
