﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaCoMoLoCo_Express.Model
{
    public class Restaurante
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public double Comision { get; set; }
        
    }
}
