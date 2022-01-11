using System;
using System.ComponentModel.DataAnnotations;

namespace Bitnvest.Model.ModelView
{
    public class CadastroDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string EmailConfirm { get; set; }
        public string CPF { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }
        public string Cel { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirm { get; set; }

    }
}
