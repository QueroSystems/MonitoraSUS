﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PessoaTrabalhaEstadoModel
    {
        public int IdPessoa { get; set; }
        public int IdEstado { get; set; }
        public bool EhResponsavel { get; set; }
        public bool EhSecretario { get; set; }
        public string SituacaoCadastro { get; set; }
    }
}