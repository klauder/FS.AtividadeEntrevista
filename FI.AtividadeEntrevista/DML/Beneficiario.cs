namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de cliente que representa o registo na tabela Cliente do Banco de Dados
    /// </summary>
    public class Beneficiario
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// ID Cliente
        /// </summary>
        public long IdCliente { get; set; }


        /// <summary>
        /// CPF formato (999.999.999-99)
        /// </summary>
        public string CPF { get; set; }
        
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

    }    
}
