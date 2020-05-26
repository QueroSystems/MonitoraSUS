using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ExameViewModel
    {
        public ExameViewModel()
        {
            IgM = "N";
            IgG = "N";
            Pcr = "N";
            IdAgenteSaude = new PessoaModel();
            IdVirusBacteria = new VirusBacteriaModel();
            IdPaciente = new PessoaModel();
            IdPaciente.Sexo = "M";
            DataExame = DateTime.Now;
            DataInicioSintomas = DateTime.Now;
            IdEstado = 0;
            MunicipioId = 0;
            PesquisarCpf = 0;
            CodigoColeta = "";
			ResponsavelRealizacaoExame = "";
			EhProfissionalSaude = false;
            IdNotificacao = "";
            StatusNotificacao = ExameModel.NOTIFICADO_NAO;
        }

        public int IdExame { get; set; }
        [Display(Name = "Virus")]
        public VirusBacteriaModel IdVirusBacteria { get; set; }
        [Display(Name = "Paciente")]
        public PessoaModel IdPaciente { get; set; }
        [Display(Name = "Agente de Sa�de")]
        public PessoaModel IdAgenteSaude { get; set; }
        [Display(Name = "Data do Exame")]
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
        public int? MunicipioId { get; set; }
        public int? IdEmpresaSaude { get; set; }
        public int PesquisarCpf { get; set; }
        public bool EhProfissionalSaude { get; set; }
        public string CodigoColeta { get; set; }
        public string StatusNotificacao { get; set; }
		//public string SituacaoSaude { get; set; }
		public string IdNotificacao { get; set; }
        public DateTime DataNotificacao { get; set; }
		public String ResponsavelRealizacaoExame { get; set; }

		[Display(Name = "Resultado")]
        public string Resultado
        {
            get
            {
                return new ExameModel() { IgM = this.IgM, IgG = this.IgG, Pcr = this.Pcr }.Resultado;
            }
        }
        public string ResultadoStatus
        {
            get
            {
                return new ExameModel() { IgM = this.IgM, IgG = this.IgG, Pcr = this.Pcr }.ResultadoStatus;
            }
        }

    }
}
