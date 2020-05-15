﻿using System;
using System.Collections.Generic;

namespace Model.ViewModel
{
    public class PesquisaPacienteViewModel
    {

        public PesquisaPacienteViewModel() 
        {
            Pacientes = new List<MonitoraPacienteViewModel>();
            Positivos = 0;
            Imunizados = 0;
            Indeterminados = 0;
        }

        public List<MonitoraPacienteViewModel> Pacientes { get; set; }
        public int Positivos { get; set; }
        public int Imunizados { get; set; }
        public int Indeterminados { get; set; }
        public string Pesquisa { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int VirusBacteria { get; set; }
        public string Resultado { get; set; }
        public bool RealizouPesquisa { get; set; }
    }
}
