using Model;
using Model.ViewModel;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;


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
            var uri = new Uri("http://www.monitorasus.ufs.br/");

            var link = "<a href='" + uri.Scheme + "://" + uri.Host + ":" + uri.Port + "/Login/RecuperarSenha/";

            switch (finalidadeEmail)
            {
                case 0:
                    return "<html><body>" +
                        "Foi solicitado a recuperação de sua senha para acesso ao MonitoraSUS, clique no link abaixo para iniciar o processo de recuperação.<br>" +
                        link + senhaModel.Token + "'>Clique aqui mudar a senha</a>";

                case 1:
                    return "<html><body>" +
                    "Seu cadastro foi aprovado para acesso ao MonitoraSUS. Para acessar o sistema clique no link abaixo para criar uma nova senha.<br>" +
                    link + senhaModel.Token + "'>Clique aqui para criar uma senha</a>";

                default: return null;
            }
        }

        public static string ReturnRole(int userType)
        {
            switch (userType)
            {
                case 0: return "USUARIO";
                case 1: return "AGENTE";
                case 2: return "COORDENADOR";
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
                IdPessoa = Convert.ToInt32(claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.NameIdentifier).Select(s => s.Value).FirstOrDefault())
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
    }
}
