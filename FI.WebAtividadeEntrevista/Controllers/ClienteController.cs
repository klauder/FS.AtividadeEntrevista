using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            ClienteModel model = new ClienteModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                model.Id = 0; //Isso é para verificar se já existe o CPF na base de dados 

                //Verficamos se o CPF é válido
                if (bo.CPFValido(CPF: model.CPF))
                {
                    //Verificamos se o CPF já está cadastrado
                    if (!bo.VerificarExistencia(ID: model.Id, CPF: model.CPF))
                    {
                        model.Id = bo.Incluir(new Cliente()
                        {
                            CEP = model.CEP,
                            Cidade = model.Cidade,
                            CPF = model.CPF,
                            Email = model.Email,
                            Estado = model.Estado,
                            Logradouro = model.Logradouro,
                            Nacionalidade = model.Nacionalidade,
                            Nome = model.Nome,
                            Sobrenome = model.Sobrenome,
                            Telefone = model.Telefone
                        });

                        if (model.Id > 0)
                        {

                            List<string> errosBenef = new List<string>();

                            foreach (Beneficiario beneficiario in model.Beneficiarios)
                            {
                                //Id do cliente a qual pertence o beneficiário
                                beneficiario.IdCliente = model.Id;

                                //Verifico se o CPF é válido cada Beneficiário
                                if (bo.CPFValido(beneficiario.CPF))
                                {
                                    //Verifico se já existe um Beneficiário com o CPF informado no Cliente em questão
                                    if (!bo.VerificarExistenciaCPFBenef(ID: beneficiario.Id, CPF: beneficiario.CPF, IdCliente: model.Id))
                                    {
                                        //ao cadastrar um novo Cliente, sempre insertamos os beneficiários (se houver)
                                        bo.IncluirBeneficiario(beneficiario);                                     

                                    }
                                    else
                                    {
                                        errosBenef.Add(string.Format("CPF: {0} pertence a outro beneficiário.", beneficiario.CPF));
                                    }
                                }
                                else
                                {
                                    errosBenef.Add(string.Format("CPF Beneficiário: {0} inválido.", beneficiario.CPF));
                                }

                            }

                            if (errosBenef.Count == 0)
                            {
                                return Json("Cadastro efetuado com sucesso");
                            }
                            else
                            {
                                Response.StatusCode = 400;
                                return Json(string.Join(Environment.NewLine, errosBenef));
                            }
                        }
                        else
                        {
                            Response.StatusCode = 400;
                            return Json("Ocorreu um error cadastrar o Cliente. Favor informar ao administrador do sistema.");
                        }
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return Json("Esse CPF já está cadastro em nossa base de dados.");
                    }
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json("CPF inválido.");
                }

            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            List<Beneficiario> listaBeneficiarios = new List<Beneficiario>();
            listaBeneficiarios = bo.ConsultarBeneficiarios(id);

            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    CPF = cliente.CPF,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    Beneficiarios = listaBeneficiarios,
                };
            }


            return View(model);
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
       
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                List<string> errosBenef = new List<string>();

                //Verficamos se o CPF é válido
                if (bo.CPFValido(CPF: model.CPF))
                {
                    //Verificamos se o CPF já está cadastrado
                    if (!bo.VerificarExistencia(ID: model.Id, CPF: model.CPF))
                    {
                        bo.Alterar(new Cliente()
                        {
                            Id = model.Id,
                            CEP = model.CEP,
                            Cidade = model.Cidade,
                            CPF = model.CPF,
                            Email = model.Email,
                            Estado = model.Estado,
                            Logradouro = model.Logradouro,
                            Nacionalidade = model.Nacionalidade,
                            Nome = model.Nome,
                            Sobrenome = model.Sobrenome,
                            Telefone = model.Telefone
                        });

                        //vamos deletar todos os beneficiarios e logo insertar os novos beneficiarios
                        //Isso podemos melhorar, verificando quais foram alterado, deletados e insertados
                        bo.DeletarBeneficiariosCliente(model.Id);

                        foreach(Beneficiario beneficiario in model.Beneficiarios)
                        {
                            //Id do cliente a qual pertence o beneficiário
                            beneficiario.IdCliente = model.Id;

                            //Verifico se o CPF é válido cada Beneficiário
                            if (bo.CPFValido(beneficiario.CPF))
                            {
                                //Verifico se já existe um Beneficiário com o CPF informado no Cliente em questão
                                if (!bo.VerificarExistenciaCPFBenef(ID: beneficiario.Id, CPF: beneficiario.CPF, IdCliente: model.Id))
                                {
                                    //Como deletei todos os beneficiarios vou sempre incluir
                                    bo.IncluirBeneficiario(beneficiario);
                                    /*
                                    //Isso é para quando vamos adicionar os primeiros beneficiários de um cliente já cadastrado
                                    if (beneficiario.Id == 0)
                                    {
                                        
                                    }
                                    else
                                    {
                                        bo.AlterarBeneficiario(beneficiario);
                                    }*/
                                    
                                }
                                else
                                {
                                    errosBenef.Add(string.Format("CPF: {0} pertence a outro beneficiário.", beneficiario.CPF));
                                }                                
                            }
                            else
                            {
                                errosBenef.Add(string.Format("CPF Beneficiário: {0} inválido.", beneficiario.CPF));
                            }

                        }

                        if(errosBenef.Count == 0)
                        {
                            return Json("Cadastro alterado com sucesso");
                        }
                        else
                        {
                            Response.StatusCode = 400;
                            return Json(string.Join(Environment.NewLine, errosBenef));
                        }
                        
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return Json("Esse CPF já está cadastro em nossa base de dados.");
                    }
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json("CPF inválido.");
                }

            }
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public ActionResult Beneficiarios(ClienteModel model)
        {
            Beneficiario beneficiario = new Beneficiario();
            beneficiario.Nome = model.Nome;
            beneficiario.CPF = model.CPF;
            model.Beneficiarios.Add(beneficiario);
            return View();
        }
    }
}