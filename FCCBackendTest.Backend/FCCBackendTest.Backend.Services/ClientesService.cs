using FCCBackendTest.Backend.Entities;
using FCCBackendTest.Backend.Repository.Interfaces;
using FCCBackendTest.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCBackendTest.Backend.Services
{
    

    public class ClientesService : IClientesService
    {

        private IClienteRepository _clienteRepository;
        public ClientesService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public List<Clientes> Listar()
        {
            return _clienteRepository.Listar();
        }

        public Clientes ListarPorId(int id)
        {
            return _clienteRepository.ListarPorId(id);
        }

        public Clientes ListarPorCPF(string CPF)
        {
            return _clienteRepository.ListarPorCPF(CPF);
        }

        public void Criar(Clientes cliente)
        {
            if (_clienteRepository.ListarPorCPF(cliente.CPF) == null)
                _clienteRepository.Criar(cliente);
        }

        public void Alterar(Clientes cliente)
        {
            if (_clienteRepository.ListarPorId(cliente.ID) != null)
                _clienteRepository.Alterar(cliente);
        }

        public void Excluir(int id)
        {
            if (_clienteRepository.ListarPorId(id) != null)
                _clienteRepository.Excluir(id);
        }
    }
}
