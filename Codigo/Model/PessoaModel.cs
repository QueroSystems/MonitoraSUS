﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class PessoaModel
    {
		public const string SITUACAO_SAUDAVEL = "S";
		public const string SITUACAO_ISOLAMENTO = "I";
		public const string SITUACAO_HOSPITALIZADO_INTERNAMENTO = "H";
		public const string SITUACAO_ESTABILIZACAO = "E";
		public const string SITUACAO_UTI = "U";
		public const string SITUACAO_OBITO = "O";
		public PessoaModel()
		{
			SituacaoSaude = SITUACAO_SAUDAVEL;
		}

		public int Idpessoa { get; set; }
        [Required]
        [Display(Name = "CPF")]
        [StringLength(11)]
        public string Cpf { get; set; }
        [Required]
        [Display(Name = "Nome")]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }
		public int IdProfissao { get; set; }
		public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FoneCelular { get; set; }
        public string FoneFixo { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }
        public bool Hipertenso { get; set; }
        public bool Diabetes { get; set; }
        public bool Obeso { get; set; }
        public bool Cardiopatia { get; set; }
        public bool Imunodeprimido { get; set; }
        public bool Cancer { get; set; }
        public bool DoencaRespiratoria { get; set; }
		[Display(Name = "Doença Renal")]
		public bool DoencaRenal { get; set; }
		public bool Epilepsia{ get; set; }
		public string OutrasComorbidades { get; set; }
		public string SituacaoSaude { get; set; }
		[Display(Name = "Data Óbito")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime ?DataObito { get; set; }

		public string SituacaoSaudeDescricao
		{
			get
			{
				if (SituacaoSaude.Equals(SITUACAO_ISOLAMENTO))
					return "Isolamento";
				if (SituacaoSaude.Equals(SITUACAO_HOSPITALIZADO_INTERNAMENTO))
					return "Internamento Clínico";
				if (SituacaoSaude.Equals(SITUACAO_ESTABILIZACAO))
					return "Estabilização";
				if (SituacaoSaude.Equals(SITUACAO_UTI))
					return "UTI";
				if (SituacaoSaude.Equals(SITUACAO_OBITO))
					return "Óbito";
				else
					return "Recuperado";
			}
		}
		public bool TemFoneCelularValido
        {
            get
            {
                if (FoneCelular == null)
                    return false;
                if (FoneCelular.Length != 11)
                    return false;
                if (FoneCelular.StartsWith("0"))
                    return false;
                return true;
            }
        }
		public bool Febre { get; set; }
		public bool Tosse { get; set; }
		public bool Coriza { get; set; }
		[Display(Name = "Dificuldade Respiratória")]
		public bool DificuldadeRespiratoria { get; set; }
		[Display(Name = "Dor na Garganta")]
		public bool DorGarganta { get; set; }
		[Display(Name = "Diarréia")]
		public bool Diarreia { get; set; }
		[Display(Name = "Dor no Ouvido")]
		public bool DorOuvido { get; set; }
		[Display(Name = "Náusea")]
		public bool Nausea { get; set; }
		[Display(Name = "Dor Abdominal")]
		public bool DorAbdominal { get; set; }
		[Display(Name = "Perda Olfato/Paladar")]
		public bool PerdaOlfatoPaladar { get; set; }
		public string OutrosSintomas { get; set; }
		public int IdAreaAtuacao { get; set; }
		public string Profissao { get; set;  }
		public string Cns { get; set; }
	}
}
