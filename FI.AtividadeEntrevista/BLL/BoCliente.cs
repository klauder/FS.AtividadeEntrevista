using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Incluir(cliente);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// Verifica CPF existe
        /// </summary>
        /// <param name="ID">Identificador único do registro</param>
        /// <param name="CPF">CPF a ser verificado</param>
        /// <returns>Retorna um bool informando se o CPF informado existe em banco</returns>
        public bool VerificarExistencia(long ID,string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(ID, CPF);
        }

        /// <summary>
        /// Verificar se o CPF cumpre os padrões de verificação do dígito verificador de CPF
        /// </summary>
        /// <param name="CPF">CPF informado a ser verificado</param>
        /// <returns>Retorna um bool informando se o CPF é válido</returns>
        public bool CPFValido(string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.CPFValido(CPF);
        }

        /// <summary>
        /// Consulta os beneficiarios de um cliente pelo id
        /// </summary>
        /// <param name="IdCliente">id do cliente</param>
        /// <returns>Lista de beneficiários de um cliente</returns>
        public List<DML.Beneficiario> ConsultarBeneficiarios(long IdCliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.ConsultarBeneficiarios(IdCliente);
        }

        /// <summary>
        /// Inclui un beneficiário ao cliente
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiario</param>
        public void IncluirBeneficiario(DML.Beneficiario beneficiario)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.IncluirBeneficiario(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void AlterarBeneficiario(DML.Beneficiario beneficiario)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.AlterarBeneficiario(beneficiario);
        }

        /// <summary>
        /// Verificar CPF dos beneficiários
        /// </summary>
        /// <param name="ID">Identificador único do registro</param>
        /// <param name="CPF">CPF a ser verificado</param>
        /// <returns>Retorna um bool informando se o CPF informado existe em banco</returns>
        public bool VerificarExistenciaCPFBenef(long ID, string CPF, long IdCliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistenciaCPFBenef(ID, CPF,IdCliente);
        }

        /// <summary>
        /// Deleta os beneficiarios de um cliente
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void DeletarBeneficiariosCliente(long idCliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.DeletarBeneficiario(idCliente);
        }
    }
}
