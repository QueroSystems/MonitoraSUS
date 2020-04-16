﻿using Model;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace MonitoraSUS.Utils
{
    public class Methods
    {
        public static string RemoveSpecialsCaracts(string poluatedString) => Regex.Replace(poluatedString, "[^0-9a-zA-Z]+", "");

        /// <summary>
        /// Recebe o Usuario da sessão em questão e retorna os dados do mesmo em um objeto usuario.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UsuarioModel RetornLoggedUser(ClaimsIdentity claimsIdentity)
            => new UsuarioModel
            {
                IdUsuario = int.Parse(claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.SerialNumber).Select(s => s.Value).FirstOrDefault()),
                Cpf = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.UserData).Select(s => s.Value).FirstOrDefault(),
                Email = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.Email).Select(s => s.Value).FirstOrDefault(),
                TipoUsuario = Convert.ToInt32(claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.Role).Select(s => s.Value).FirstOrDefault()),
                IdPessoa = Convert.ToInt32(claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.NameIdentifier).Select(s => s.Value).FirstOrDefault())
            };
    }
}
