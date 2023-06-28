using FCCBackendTest.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCBackendTest.Backend.Repository.Interfaces
{
    public interface IClienteRepository
    {
        void Alterar(Clientes cliente);
        void Criar(Clientes cliente);
        void Excluir(int ID);
        List<Clientes> Listar();
        Clientes ListarPorId(int id);

        Clientes ListarPorCPF(string CPF);

    }
}
