using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoCSV.Model
{
    public class RestModel<T>
    {
        public List<T> ListaFuncionariosImportados { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }
}
