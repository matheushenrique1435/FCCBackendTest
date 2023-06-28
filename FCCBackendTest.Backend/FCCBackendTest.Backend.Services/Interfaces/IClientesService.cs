using FCCBackendTest.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCBackendTest.Backend.Services.Interfaces
{
    public interface IClientesService
    {
        void Alterar(Clientes cliente);
        void Criar(Clientes cliente);
        void Excluir(int id);
        List<Clientes> Listar();
        Clientes ListarPorId(int id);

        Clientes ListarPorCPF(string CPF);
    }
}
