﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCBackendTest.Backend.Entities
{
    public class Enderecos
    {
        public int ID { get; set; }

        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }

        public int ClienteID { get; set; }
    }
}
