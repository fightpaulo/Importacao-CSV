using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoCSV.Entidade
{
    public class Funcionario
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Profissao { get; set; }
        public string Sexo { get; set; }
        public string InformacaoAvulsa1 { get; set; }
        public string InformacaoAvulsa2 { get; set; }

        public override string ToString()
        {
            string ret = $"Nome: {Nome} - Profissão: {Profissao}";
            return ret;
        }
    }
}
