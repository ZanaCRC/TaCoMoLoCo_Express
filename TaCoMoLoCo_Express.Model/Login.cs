﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Login
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        
    }
}
