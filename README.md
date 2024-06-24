# FS.AtividadeEntrevista
Exercícios da Atividade da Entrevista da empresa Função Sistema


Klauder Dias - 24/06/2024

Criei o campo CPF como VARCHAR(14), porque está guardando o CPF formatado no padrão 999.999.999-99, assim estava os demais campos (Telefone, CEP), por isso fiz assim.

Crei o campo CPF como UNIQUE VARCHAR(14), na tabela de CLIENTE, porque a relação CLIENTE -> CPF é 1 -> 1, ou seja, NÃO PODE haver um mesmo CPF para distintos Clientes.

Crei o campo CPF como VARCHAR(14), na tabela de BENEFICIARIO, porque a relação BENEFICIARIO -> CPF é N -> N, ou seja, PODE haver um mesmo CPF para distintos Clientes.

Deletei o SP FI_SP_IncClienteV2, porque não está sendo utilizado. Utilizamos somente o SP FI_SP_IncCliente

Crei o campo CPF como UNIQUE na tabela de CLIENTE, porque a relação CLIENTE -> CPF é 1 -> 1, ou seja, não pode hacer um mesmo CPF para distintos Clientes.

FI_SP_IncBenef -> Um sugestão de como fazer um verificação adicional em Base de Dados, além da que já é feita no C#.

Ao Cadastrar um novo cliente, limpo o formaulario para um novo cadastro. Exibo a mensagem "Cadastro efetuado com sucesso". E após 02 segundos redireciono para a listagem de clientes.

Apaguei a pasta Procedures (FI.AtividadeEntrevista\DAL\Clientes\Procedures\), porque não se usa

Ao "Alterar" um Cliente, decide deletar todos os beneficiarios e logo inserir os dados que foram preenchidos.

Como melhora poderiamos colocar as funções ModalDialog, ValidarCPF, excluir, alterar e inserirLinha em um único arquivo js e importar globalmente, assim facilitaria
a manuteção dessas funções.

Não realizei Jquery Validation e nem MaskInput porque os demais campos não utilizam esses plugins.
