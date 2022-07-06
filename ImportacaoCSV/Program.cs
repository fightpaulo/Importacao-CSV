using ImportacaoCSV.Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoCSV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string arquivo = desktop + "\\" + "funcionarios_teste.csv";

            var funcionarios = FileHelper.ImportarArquivo(arquivo).ListaFuncionariosImportados;

            Console.WriteLine("Funcionarios importados: ");

            foreach (var func in funcionarios)
            {
                Console.WriteLine(func.ToString());
            }

            Console.ReadKey();
        }
    }
}
