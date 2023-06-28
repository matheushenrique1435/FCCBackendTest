using FCCBackendTest.Backend.Entities;
using FCCBackendTest.Backend.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using Dapper;
using System.Data.SqlClient;
using FCCBackendTest.Backend.DbContext;

namespace FCCBackendTest.Backend.Repository
{

    public class ClienteRepository : IClienteRepository
    {
        private RelationalDatabaseContext _dbConnection;

        public ClienteRepository(RelationalDatabaseContext relationalDatabaseContext)
        {
            _dbConnection = relationalDatabaseContext;
        }

        public List<Clientes> Listar()
        {
            return _dbConnection.DatabaseConnection.Query<Clientes, Enderecos,Clientes>("SELECT * FROM Clientes INNER JOIN Enderecos ON Clientes.ID = Enderecos.clienteID",
                                                                                        (cli, end) =>  
                                                                                        {
                                                                                            cli.Endereco = end;
                                                                                            return cli;
                                                                                        }).ToList();

        }

        public Clientes ListarPorId(int id)
        {
            List<Clientes> listaClientes = Listar();
            Clientes cliente = listaClientes.SingleOrDefault(cli => cli.ID.Equals(id));
            if(cliente != null)
                cliente.Endereco = _dbConnection.DatabaseConnection.QuerySingleOrDefault<Enderecos>("SELECT * FROM Enderecos WHERE Enderecos.clienteID=@clienteID", new { clienteID = cliente.ID });

            return cliente;
        }

        public Clientes ListarPorCPF(string CPF)
        {
            List<Clientes> listaClientes = Listar();
            Clientes cliente = listaClientes.SingleOrDefault(cli => cli.CPF.Equals(CPF));
            if (cliente != null)
            {
                cliente.Endereco = _dbConnection.DatabaseConnection.QuerySingleOrDefault<Enderecos>("SELECT * FROM Enderecos WHERE Enderecos.clienteID=@clienteID", new { clienteID = cliente.ID });
            }
            
            return cliente;
        }

        public void Criar(Clientes cliente)
        {
            _dbConnection.DatabaseConnection.Open();
            IDbTransaction transaction = _dbConnection.DatabaseConnection.BeginTransaction();
            try
            {
                string sql = @"INSERT INTO Clientes (CPF, Nome, RG, DataExpedicao, OrgaoExpedidor, UF, DataNascimento, Sexo, EstadoCivil)
                              OUTPUT INSERTED.ID VALUES (@CPF, @Nome, @RG,@DataExpedicao,@OrgaoExpedidor,@UF,@DataNascimento,@Sexo,@EstadoCivil);";
                cliente.ID = _dbConnection.DatabaseConnection.ExecuteScalar<int>(sql, cliente, transaction);

                if (cliente.ID != 0 && cliente.Endereco != null)
                {
                    sql = @"INSERT INTO Enderecos (CEP, Logradouro, Numero, Complemento, Bairro, Cidade, UF, clienteID)
                          OUTPUT INSERTED.ID VALUES (@CEP, @Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @UF, @ClienteID);";

                    cliente.Endereco.ClienteID = cliente.ID;

                    cliente.Endereco.ID = _dbConnection.DatabaseConnection.ExecuteScalar<int>(sql, cliente.Endereco, transaction);

                }

                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    throw new Exception("Falha ao criar registro do cliente.");
                }

            }
            finally
            {
                _dbConnection.DatabaseConnection.Close();
            }


        }

        public void Alterar(Clientes cliente)
        {
            _dbConnection.DatabaseConnection.Open();
            IDbTransaction transaction = _dbConnection.DatabaseConnection.BeginTransaction();

            try
            {
                string sql = @"UPDATE Clientes set CPF=@CPF, Nome=@Nome, RG=@RG, DataExpedicao=@DataExpedicao, UF=@UF, DataNascimento=@DataNascimento,Sexo=@Sexo, 
                            EstadoCivil=@EstadoCivil WHERE Clientes.ID=@Id";

                _dbConnection.DatabaseConnection.Execute(sql, cliente, transaction);

                if (cliente.Endereco != null)
                {
                    sql = @"UPDATE Enderecos set CEP=@CEP, Logradouro=@Logradouro, Numero=@Numero, Complemento=@Complemento, Bairro=@Bairro, Cidade=@Cidade, 
                          UF=@UF WHERE Enderecos.ID = @Id";
                    _dbConnection.DatabaseConnection.Execute(sql, cliente.Endereco, transaction);
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {

                    throw new Exception("Falha ao atualizar registro do cliente");
                };
            }
            finally
            {
                _dbConnection.DatabaseConnection.Close();
            }
        }

        public void Excluir(int id)
        {

            _dbConnection.DatabaseConnection.Open();

            IDbTransaction transaction = _dbConnection.DatabaseConnection.BeginTransaction();
            try
            {
                string sql = "DELETE FROM Enderecos where clienteID=@Id";
                _dbConnection.DatabaseConnection.Execute(sql, new { Id = id}, transaction);

                sql = "DELETE FROM Clientes where ID=@Id";
                _dbConnection.DatabaseConnection.Execute(sql, new { Id = id }, transaction);

                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {

                    throw new Exception("Falha ao remover dados de registro do cliente"); ;
                }

            }
            finally
            {
                _dbConnection.DatabaseConnection.Close();
            }
        }

    }
}
