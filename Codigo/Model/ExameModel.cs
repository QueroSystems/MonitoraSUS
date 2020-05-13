﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ExameModel
    {
        public const string RESULTADO_POSITIVO = "Positivo";
        public const string RESULTADO_NEGATIVO = "Negativo";
        public const string RESULTADO_IMUNIZADO = "Imunizado";
        public const string RESULTADO_INDETERMINADO = "Indeterminado";

        public const string NOTIFICADO_SIM = "S";
        public const string NOTIFICADO_NAO = "N";
        public const string NOTIFICADO_ENVIADO = "E";

        public int IdExame { get; set; }
        [Display(Name = "Virus")]
        public int IdVirusBacteria { get; set; }
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }
        public int IdAgenteSaude { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataExame { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInicioSintomas { get; set; }
        public string IgG { get; set; }
        public string IgM { get; set; }
        public string Pcr { get; set; }
        public int IdEstado { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdEmpresaSaude { get; set; }
        public string IdNotificacao { get; set; }
        public bool EhProfissionalSaude { get; set; }
        public string CodigoColeta { get; set; }
        public string StatusNotificacao { get; set; }
        public string StatusNotificacaoDescricao
        {
            get
            {
                if (StatusNotificacao.Equals("N"))
                    return "NÃO";
                else if (StatusNotificacao.Equals("S"))
                    return "SIM";
                else
                    return "ENVIADA";
            }
        }



        public string Resultado
        {
            get
            {
                if (IgM.Equals("S") || Pcr.Equals("S"))
                    return RESULTADO_POSITIVO;
                else if (IgM.Equals("N") && Pcr.Equals("N") && IgG.Equals("N"))
                    return RESULTADO_NEGATIVO;
                else if ((IgM.Equals("N") && IgG.Equals("S")) || (Pcr.Equals("N") && IgG.Equals("S")))
                    return RESULTADO_IMUNIZADO;
                else
                    return RESULTADO_INDETERMINADO;
            }
        }

        public string ResultadoStatus
        {
            get
            {
                if (Resultado.Equals(RESULTADO_POSITIVO))
                    return "P";
                else if (Resultado.Equals(RESULTADO_NEGATIVO))
                    return "N";
                else if (Resultado.Equals(RESULTADO_IMUNIZADO))
                    return "C";
                else
                    return "I";
            }
        }
    }
}
