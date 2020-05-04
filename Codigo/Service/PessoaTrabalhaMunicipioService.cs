﻿using Model;
using Persistence;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class PessoaTrabalhaMunicipioService : IPessoaTrabalhaMunicipioService
    {
        private readonly monitorasusContext _context;
        public PessoaTrabalhaMunicipioService(monitorasusContext context)
        {
            _context = context;
        }

        public bool Delete(int idPessoa)
        {
            var pessoas = _context.Pessoatrabalhamunicipio.Where(p => p.IdPessoa == idPessoa);
			foreach(Pessoatrabalhamunicipio pessoa in pessoas)
				_context.Pessoatrabalhamunicipio.Remove(pessoa);
            return _context.SaveChanges() == 1 ? true : false;
        }

        public PessoaTrabalhaMunicipioModel GetAgentMunicipioByIdPessoa(int idPessoa, int idMunicipio)
        => _context
                .Pessoatrabalhamunicipio
                .Where(p => p.IdPessoa == idPessoa && p.IdMunicipio == idMunicipio && p.EhSecretario.Equals(0) && p.EhResponsavel.Equals(0))
                .Select(p => new PessoaTrabalhaMunicipioModel
                {
                    IdPessoa = p.IdPessoa,
                    IdMunicipio = p.IdMunicipio,
                    EhResponsavel = Convert.ToBoolean(p.EhResponsavel),
                    EhSecretario = Convert.ToBoolean(p.EhSecretario),
                    SituacaoCadastro = p.SituacaoCadastro
                }).FirstOrDefault();

        public List<PessoaTrabalhaMunicipioModel> GetAll()
            => _context
                .Pessoatrabalhamunicipio
                .Select(p => new PessoaTrabalhaMunicipioModel
                {
                    IdPessoa = p.IdPessoa,
                    IdMunicipio = p.IdMunicipio,
                    EhResponsavel = Convert.ToBoolean(p.EhResponsavel),
                    EhSecretario = Convert.ToBoolean(p.EhSecretario),
                    SituacaoCadastro = p.SituacaoCadastro
                }).ToList();

        public List<PessoaTrabalhaMunicipioModel> GetAllAgentsMunicipio(int idMunicipio)
            => _context
                .Pessoatrabalhamunicipio
                .Where(p => p.EhSecretario.Equals(0) && p.EhResponsavel.Equals(0) && p.IdMunicipio.Equals(idMunicipio))
                .Select(p => new PessoaTrabalhaMunicipioModel
                {
                    IdPessoa = p.IdPessoa,
                    IdMunicipio = p.IdMunicipio,
                    EhResponsavel = Convert.ToBoolean(p.EhResponsavel),
                    EhSecretario = Convert.ToBoolean(p.EhSecretario),
                    SituacaoCadastro = p.SituacaoCadastro
                }).ToList();

        public List<PessoaTrabalhaMunicipioModel> GetAllGestoresMunicipio(int idMunicipio)
            => _context
                .Pessoatrabalhamunicipio
                .Where(p => p.EhResponsavel.Equals(1) && p.IdMunicipio.Equals(idMunicipio))
                .Select(p => new PessoaTrabalhaMunicipioModel
                {
                    IdPessoa = p.IdPessoa,
                    IdMunicipio = p.IdMunicipio,
                    EhResponsavel = Convert.ToBoolean(p.EhResponsavel),
                    EhSecretario = Convert.ToBoolean(p.EhSecretario),
                    SituacaoCadastro = p.SituacaoCadastro
                }).ToList();

		public List<PessoaTrabalhaMunicipioModel> GetAllGestoresMunicipio()
			=> _context
				.Pessoatrabalhamunicipio
				.Where(p => p.EhResponsavel.Equals(1))
				.Select(p => new PessoaTrabalhaMunicipioModel
				{
					IdPessoa = p.IdPessoa,
					IdMunicipio = p.IdMunicipio,
					EhResponsavel = Convert.ToBoolean(p.EhResponsavel),
					EhSecretario = Convert.ToBoolean(p.EhSecretario),
					SituacaoCadastro = p.SituacaoCadastro
				}).ToList();

		public PessoaTrabalhaMunicipioModel GetById(int idPessoa, int idMunicipio)
            => _context
                .Pessoatrabalhamunicipio
                .Where(p => p.IdPessoa == idPessoa && p.IdMunicipio == idMunicipio)
                .Select(p => new PessoaTrabalhaMunicipioModel
                {
                    IdPessoa = p.IdPessoa,
                    IdMunicipio = p.IdMunicipio,
                    EhResponsavel = Convert.ToBoolean(p.EhResponsavel),
                    EhSecretario = Convert.ToBoolean(p.EhSecretario),
                    SituacaoCadastro = p.SituacaoCadastro
                }).FirstOrDefault();

        public PessoaTrabalhaMunicipioModel GetByIdPessoa(int idPessoa)
         => _context
                .Pessoatrabalhamunicipio
                .Where(pessoaMunicipio => pessoaMunicipio.IdPessoa == idPessoa)
                .Select(p => new PessoaTrabalhaMunicipioModel
                {
                    IdPessoa = p.IdPessoa,
                    IdMunicipio = p.IdMunicipio,
                    EhResponsavel = Convert.ToBoolean(p.EhResponsavel),
                    EhSecretario = Convert.ToBoolean(p.EhSecretario),
                    SituacaoCadastro = p.SituacaoCadastro
                }).FirstOrDefault();

        public bool Insert(PessoaTrabalhaMunicipioModel pessoaTrabalhaMunicipioModel)
        {
            if (pessoaTrabalhaMunicipioModel != null)
            {
                try
                {
                    _context.Pessoatrabalhamunicipio.Add(ModelToEntity(pessoaTrabalhaMunicipioModel));
                    return _context.SaveChanges() == 1 ? true : false;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return false;
        }

        public bool Update(PessoaTrabalhaMunicipioModel pessoaTrabalhaMunicipioModel)
        {

            _context.Update(ModelToEntity(pessoaTrabalhaMunicipioModel));
            return _context.SaveChanges() == 1 ? true : false;

        }
        private Pessoatrabalhamunicipio ModelToEntity(PessoaTrabalhaMunicipioModel model)
        {
            return new Pessoatrabalhamunicipio
            {
                IdMunicipio = model.IdMunicipio,
                IdPessoa = model.IdPessoa,
                EhResponsavel = Convert.ToByte(model.EhResponsavel),
                EhSecretario = Convert.ToByte(model.EhSecretario),
                SituacaoCadastro = model.SituacaoCadastro
            };
        }
    }
}
