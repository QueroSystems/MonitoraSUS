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
        PessoaTrabalhaEstadoModel GetById(int idPessoa, int idEstado);
        List<PessoaTrabalhaEstadoModel> GetAllSecretariesPendents();
        PessoaTrabalhaEstadoModel GetByIdPessoa(int idPessoa);
    }
}
