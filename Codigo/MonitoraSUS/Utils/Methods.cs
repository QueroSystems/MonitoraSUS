using Model;
using Model.AuxModel;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonitoraSUS.Utils
{
    public class Methods
    {
        public static string RemoveSpecialsCaracts(string poluatedString) => Regex.Replace(poluatedString, "[^0-9a-zA-Z]+", "");

        public static string GenerateToken()
        {
            var frase = new StringBuilder();
            var random = new Random();
            int length = random.Next(30, 99);
            char letra;

            for (int i = 0; i < length; i++)
            {
                var flt = random.NextDouble();
                var shift = Convert.ToInt32(Math.Floor(length * flt));
                letra = Convert.ToChar(shift + length);
                frase.Append(letra);
            }

            return Criptography.GenerateHashString(frase.ToString());
        }

        public static string MessageEmail(RecuperarSenhaModel senhaModel, int finalidadeEmail)
        {
            var uri = new Uri("https://www.monitorasus.ufs.br/");
            var site = "<a href='" + uri.Scheme + "://" + uri.Host + ":" + uri.Port;
            var link = site + "/Login/RecuperarSenha/";

            switch (finalidadeEmail)
            {
                case 0:
                    return "<html><body>" +
                        "Foi solicitado a recuperação de sua senha para acesso ao MonitoraSUS.<br>" +
                        "Você possui 24 horas para fazer a alteração da sua senha de acesso.<br>" +
                        link + senhaModel.Token + "'>Clique aqui mudar a senha</a>" +
                        RodapeEmail();


                case 1:
                    return "<html><body>" +
                        "Parabéns! Seu cadastro foi aprovado para acesso ao MonitoraSUS.<br>" +
                        "Você possui 24 horas para criar sua senha de acesso ao sistema.<br>" +
                        link + senhaModel.Token + "'>Clique aqui para criar sua senha.</a>" +
                        RodapeEmail();

                case 2:
                    return "<html><body>" +
                        "Parabéns! Seu cadastro foi ativado para acesso ao MonitoraSUS. <br>" +
                        "Acesse o sistema " + site + "'>aqui</a>." +
                        "<br>Caso não lembre da sua senha de acesso, você possui 24 horas para criar uma nova senha.<br><a href='" +
                        link + senhaModel.Token + "'>Clique aqui para criar uma nova senha.</a>" +
                        RodapeEmail();
				case 4:
					return "<html><body>" +
						"Obrigado por solicitar o cadastro no MonitoraSUS! <br/>" +
						"O objetivo do sistema é auxiliar gestores municipais e estaduais no monitoramento de pacientes <br/>" +
	  					"através da integração dos resultados de testes que estão sendo realizados em todo Brasil. <br/>" +
						"O sistema permite gerenciar os testes realizados pela gestão e fazer o monitoramento dos pacientes <br/>" +
						"residentes no município que foram positivados e notificados pelo MonitoraSUS. <br/>" +
						"Seu cadastro foi aprovado com perfil de ADMINISTRADOR do Município ou Estado solicitado.<br/>" +
						"Acesse o sistema através da url " + site + " e consulte o manual do sistema." +
						"Entraremos em contato para agendarmos uma apresentação das funcionalidades." +
						"Você possui 24 horas para criar sua senha de acesso ao sistema.<br><a href='" + 
						link + senhaModel.Token + "'>Clique aqui para criar sua senha.</a>" +
						RodapeEmail();

				default: return null;
            }
        }

        private static string RodapeEmail()
        {
            return "<br>" +
                    "Qualquer dúvida ou sugestão entre em contato com o nosso time." +
                        "<br><br>" +
                        "<br>Equipe MonitoraSUS" +
                        "<br>KNUTH-Fábrica de Software da Universidade Federal de Sergipe" +
                        "<br>fabricadesoftware@ufs.br";
        }

        public static string ReturnRole(int userType)
        {
            switch (userType)
            {
                case 0: return "USUARIO";
                case 1: return "AGENTE";
                case 2: return "GESTOR";
                case 3: return "SECRETARIO";
                case 4: return "ADM";
                default: return "UNDEFINED";
            }
        }

        public static int ReturnRoleId(string userType)
        {
            switch (userType.ToUpper())
            {
                case "USUARIO": return 0;
                case "AGENTE": return 1;
                case "GESTOR": return 2;
                case "SECRETARIO": return 3;
                case "ADM": return 4;
                default: return -1;
            }
        }

        /// <summary>
        /// Recebe o Usuario da sessão em questão e retorna os dados do mesmo em um objeto usuario.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UsuarioViewModel RetornLoggedUser(ClaimsIdentity claimsIdentity)
        {
            var usuario = new UsuarioModel
            {
                IdUsuario = int.Parse(claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.SerialNumber).Select(s => s.Value).FirstOrDefault()),
                Cpf = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.UserData).Select(s => s.Value).FirstOrDefault(),
                Email = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.Email).Select(s => s.Value).FirstOrDefault(),
                IdPessoa = Convert.ToInt32(claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.NameIdentifier).Select(s => s.Value).FirstOrDefault()),

            };



            var usuarioViewModel = new UsuarioViewModel
            {
                UsuarioModel = usuario,
                RoleUsuario = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.Role).Select(s => s.Value).FirstOrDefault()
            };

            return usuarioViewModel;
        }

        /// <summary>
        /// retrnar cpf com padrao de caracteres - mask
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static string PatternCpf(string cpf)
        {
            cpf = cpf.Substring(0, 3) + "." + cpf.Substring(3, 3) + "." + cpf.Substring(6, 3) + "-" + cpf.Substring(9, 2);
            return cpf;
        }

        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool SoContemNumeros(string texto)
        {
            texto = texto.Replace(".", "").Replace("-", "");
            var value = Regex.IsMatch(texto, "^[0-9]*$");
            return value;
        }

        public static bool SoContemLetras(string texto)
        {
            texto = texto.Replace(".", "").Replace("-", "");
            var value = Regex.IsMatch(texto, @"^([a-zA-Z ])*$");
            return value;
        }

        /// <summary>
        /// Realiza requisição para o google e retorna o valor do captcha.
        /// </summary>
        /// <param name="token">Valor do campo g-recaptcha-response do formulario.</param>
        /// <param name="secretKey">Valor da chave secreta do AppSettings.</param>
        /// <returns></returns>
        public static async Task<float> ValidateCaptcha(string token, string secretKey)
        {
            try
            {
                var cliente = new HttpClient();
                var uri = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}";

                var resultado = await cliente.GetStringAsync(uri);
                var jsonResponse = JsonConvert.DeserializeObject<RecaptchaModel>(resultado);

                if (jsonResponse.Success)
                    return jsonResponse.Score;
                else
                    throw new Exception(jsonResponse.ErrorCodes.ToString());
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        /**public static string GetResultadoExame(ExameViewModel exame)
        {

            string resultado = "I";

            if (exame.Pcr.Equals("S") || exame.IgM.Equals("S"))
            {
                resultado = "P";
            }
            else if (exame.Pcr.Equals("I") || exame.IgM.Equals("I"))
            {
                resultado = "I";
            }
            else if (exame.IgG.Equals("S"))
            {
                resultado = "C";
            }
            else if (exame.Pcr.Equals("N") || exame.IgM.Equals("N"))
            {
                resultado = "N";
            }

            return resultado;
        }*/


        /**public static string GetStatusExame(string status)
        {
            switch (status)
            {
                case "I": return "INDETERMINADO";
                case "N": return "NEGATIVO";
                case "C": return "CURADO";
                case "P": return "POSITIVO";

                default: return "IDETERMINADO";
            }
        }*/
    }
}
