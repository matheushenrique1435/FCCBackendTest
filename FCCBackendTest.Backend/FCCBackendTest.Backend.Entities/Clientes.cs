using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCBackendTest.Backend.Entities
{
    public class Clientes
    {
        public int ID { get; set; }

        public string CPF { get; set; }

        public string Nome { get; set; }

        public string RG { get; set; }

        public DateTime DataExpedicao { get; set; }

        public string OrgaoExpedidor { get; set; }
        public string UF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        public string EstadoCivil { get; set; }

        public Enderecos Endereco { get; set; }
    }
}
