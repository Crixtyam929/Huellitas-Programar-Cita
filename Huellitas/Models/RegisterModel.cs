﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Huellitas.Models
{
    public class UsuarioModel
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Password { get; set; }
    }

}