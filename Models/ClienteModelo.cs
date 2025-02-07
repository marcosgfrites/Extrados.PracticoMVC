﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticoMVC.Models
{
    public class ClienteModelo
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Código")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El campo {0} tiene un mínimo permitido de {2} caracteres y un máximo de {1} caracteres")]
        [DisplayName("Razón Social")]
        public string RazonSocial { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayName("Usuario")]
        public int IdUsuario { get; set; }
    }
}