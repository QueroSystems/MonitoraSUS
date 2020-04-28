﻿using Model;
using System;
using System.Collections.Generic;

namespace Service.Interface
{
    public interface IExameService
    {
        bool Insert(ExameModel exameModel);
        bool Update(ExameModel exameModel);
        bool Delete(int id);
        List<ExameModel> GetAll();
        ExameModel GetById(int id);
        List<ExameModel> GetByIdAgente(int id);
        List<ExameModel> GetByIdEstado(int idEstado);
        List<ExameModel> GetByIdEmpresa(int idEempresa);
        List<ExameModel> GetByIdPaciente(int idPaciente);
        List<ExameModel> CheckDuplicateExamToday(int idPaciente,int idVirusBacteria, DateTime dateExame);
    }
}
