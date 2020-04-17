﻿using Model;
using System.Collections.Generic;

namespace Service.Interface
{
    public interface IPessoaTrabalhaEstadoService
    {
        bool Insert(PessoaTrabalhaEstadoModel pessoaTrabalhaEstadoModel);
        bool Update(PessoaTrabalhaEstadoModel pessoaTrabalhaEstadoModel);
        bool Delete(int id);
        List<PessoaTrabalhaEstadoModel> GetAll();
        PessoaTrabalhaEstadoModel GetById(int idPessoa);
        List<PessoaTrabalhaEstadoModel> GetAllSecretariesPendents();
        List<PessoaTrabalhaEstadoModel> GetAllAgents();
        PessoaTrabalhaEstadoModel GetSecretarioAtivoByIdPessoa(int idPessoa);
    }
}
