﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Interface;
using MonitoraSUS.Utils;
using Model.ViewModel;
using System.Security.Claims;

namespace MonitoraSUS.Controllers
{

    public class AgenteSecretarioController : Controller
    {
        private readonly IMunicipioService _municipioService;
        private readonly IEstadoService _estadoService;
        private readonly IPessoaService _pessoaService;
        private readonly IPessoaTrabalhaEstadoService _pessoaTrabalhaEstadoService;
        private readonly IPessoaTrabalhaMunicipioService _pessoaTrabalhaMunicipioService;

        public AgenteSecretarioController(IMunicipioService municipioService, IEstadoService estadoService,
            IPessoaService pessoaService, IPessoaTrabalhaMunicipioService pessoaTrabalhaMunicipioService,
            IPessoaTrabalhaEstadoService pessoaTrabalhaEstadoService)
        {
            _municipioService = municipioService;
            _estadoService = estadoService;
            _pessoaService = pessoaService;
            _pessoaTrabalhaEstadoService = pessoaTrabalhaEstadoService;
            _pessoaTrabalhaMunicipioService = pessoaTrabalhaMunicipioService;
        }
        // GET: AgenteSecretario
        public ActionResult Index()
        {
            var pessoas = _pessoaService.GetAll();
            var secMuniEst = new List<SecretarioMunicipioEstadoViewModel>();

            var secretariosEstadoPendentes = _pessoaTrabalhaEstadoService.GetAllSecretariesPendents();
            var secretariosMunicipioPendente = _pessoaTrabalhaMunicipioService.GetAllSecretariesPendents();

            secretariosEstadoPendentes.ForEach(item => secMuniEst.Add(new SecretarioMunicipioEstadoViewModel { Pessoa = _pessoaService.GetById(item.IdPessoa), PessoaEstado = item, Situacao = 0 }));
            secretariosMunicipioPendente.ForEach(item => secMuniEst.Add(new SecretarioMunicipioEstadoViewModel { Pessoa = _pessoaService.GetById(item.IdPessoa), PessoaMunicipio = item, Situacao = 0 }));

            return View(secMuniEst);
        }
        /// <summary>
        /// Se for secretario poderá administrar o status do agente no sistema, A, I, S. 
        /// </summary>
        /// <returns></returns>
        // GET: Aprovacao do agente de saúde
        []
        public ActionResult IndexApproveAgent()
        {
            // usuario logado
            var usuario = Utils.Methods.RetornLoggedUser((ClaimsIdentity)User.Identity);

            List<>

                var pessoaTrabalhaEstado = _pessoaTrabalhaEstadoService.GetById(usuario.IdPessoa);
            if (pessoaTrabalhaEstado != null)
            {
                var agentesEstado = _pessoaTrabalhaEstadoService.GetAllAgents();
            }
            else
            {
                var agentesMunicipio = _pessoaTrabalhaMunicipioService.GetAllAgents();
            }



        }

        // GET: AgenteSecretario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AgenteSecretario/Create
        public ActionResult Create(int userType)
        {
            ViewBag.userType = userType;
            return View();
        }

        // POST: AgenteSecretario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAgent(IFormCollection collection)
        {
            try
            {
                // INSERTING A USER AND RETURNING THE ID.
                var idPessoaInserida = PeopleInserted(collection);

                // ===================== OTHERS ENTITIES =====================
                var atuacao = collection["areaAtuacao"];
                if (atuacao.Equals("Municipal"))
                    if (_pessoaTrabalhaMunicipioService
                            .Insert(new PessoaTrabalhaMunicipioModel
                            {
                                IdPessoa = idPessoaInserida,
                                IdMunicipio = Convert.ToInt32(collection["select-Cidade"]),
                                EhSecretario = false,
                                EhResponsavel = false
                            })
                        )
                        return Ok();
                    else
                        return BadRequest();

                if (atuacao.Equals("Estadual"))
                    if (_pessoaTrabalhaEstadoService
                            .Insert(new PessoaTrabalhaEstadoModel
                            {
                                IdPessoa = idPessoaInserida,
                                IdEstado = Convert.ToInt32(collection["select-Estado"]),
                                EhSecretario = false,
                                EhResponsavel = false
                            })
                        )
                        return Ok();
                    else
                        return BadRequest();

                // Redirecting
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // POST: AgenteSecretario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSec(IFormCollection collection)
        {
            try
            {
                // INSERTING A USER AND RETURNING THE ID.
                var idPessoaInserida = PeopleInserted(collection);

                // ===================== OTHERS ENTITIES =====================
                var atuacao = collection["areaAtuacao"];
                if (atuacao.Equals("Municipal"))
                    if (_pessoaTrabalhaMunicipioService
                            .Insert(new PessoaTrabalhaMunicipioModel
                            {
                                IdPessoa = idPessoaInserida,
                                IdMunicipio = Convert.ToInt32(collection["select-Cidade"]),
                                EhSecretario = true,
                                EhResponsavel = true
                            })
                        )
                        return Ok();
                    else
                        return BadRequest();

                if (atuacao.Equals("Estadual"))
                    if (_pessoaTrabalhaEstadoService
                            .Insert(new PessoaTrabalhaEstadoModel
                            {
                                IdPessoa = idPessoaInserida,
                                IdEstado = Convert.ToInt32(collection["select-Estado"]),
                                EhSecretario = true,
                                EhResponsavel = true
                            })
                        )
                        return Ok();
                    else
                        return BadRequest();

                // Redirecting
                return RedirectToAction("Create");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public IActionResult ReturnCities(string UF)
        {
            var listOfCities = _municipioService.GetByUFCode(UF);
            if (listOfCities.Count != 0)
                return Ok(listOfCities);
            else
                return NoContent();
        }

        public ActionResult ReturnStates() => Ok(_estadoService.GetAll());

        // GET: AgenteSecretario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AgenteSecretario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AgenteSecretario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AgenteSecretario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // ======================== PRIVATE METHODS ========================
        private int PeopleInserted(IFormCollection collection)
        {
            // Info Pessoal
            var cpf = Methods.RemoveSpecialsCaracts(collection["Cpf"]);
            var nome = collection["Nome"];
            var dataNascimento = collection["DataNascimento"];
            var sexo = collection["sexo"];
            var cell = Methods.RemoveSpecialsCaracts(collection["FoneCelular"]);
            var fixo = Methods.RemoveSpecialsCaracts(collection["FoneFixo"]);
            var email = collection["Email"];

            // Localização
            var cep = Methods.RemoveSpecialsCaracts(collection["Cep"]);
            var rua = collection["Rua"];
            var numero = collection["Numero"];
            var bairro = collection["Bairro"];
            var cidade = collection["Cidade"];
            var estado = collection["Estado"];
            var complemento = collection["Complemento"];
            var latitude = collection["Latitude"];
            var longitude = collection["Longitude"];

            // Doenças
            var hipertenso = collection["Hipertenso"];
            var diabetes = collection["Diabetes"];
            var obeso = collection["Obeso"];
            var cardiopata = collection["Cardiopatia"];
            var imunoDepri = collection["Imunodeprimido"];
            var cancer = collection["Cancer"];
            var doencaResp = collection["DoencaRespiratoria"];

            // Inserção e recebendo o objeto inserido (ID)
            var pessoa = _pessoaService.Insert(new PessoaModel
            {
                Cpf = cpf,
                Nome = nome,
                DataNascimento = Convert.ToDateTime(dataNascimento),
                Sexo = sexo,
                FoneCelular = cell,
                FoneFixo = fixo,
                Email = email,
                Cep = cep,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado,
                Complemento = complemento,
                Latitude = Convert.ToDecimal(latitude),
                Longitude = Convert.ToDecimal(longitude),
                Hipertenso = hipertenso.Contains("true") ? true : false,
                Cardiopatia = cardiopata.Contains("true") ? true : false,
                Cancer = cancer.Contains("true") ? true : false,
                Diabetes = diabetes.Contains("true") ? true : false,
                DoencaRespiratoria = doencaResp.Contains("true") ? true : false,
                Imunodeprimido = imunoDepri.Contains("true") ? true : false,
                Obeso = obeso.Contains("true") ? true : false
            });

            return pessoa.Idpessoa;
        }
    }
}