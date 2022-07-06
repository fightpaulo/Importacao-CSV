using ImportacaoCSV.Entidade;
using ImportacaoCSV.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoCSV.Negocio
{
    public class FileHelper
    {
        public static RestModel<Funcionario> ImportarArquivo(string arquivo, bool ehTemplateDefinido = true)
        {
            var restModel = new RestModel<Funcionario>();

            List<Funcionario> funcionariosLista;
            

            if (ehTemplateDefinido)
            {
                funcionariosLista = MontarListaFuncionariosFromArquivoString(arquivo);
            }
            else
            {
                using (var reader = new StreamReader(arquivo, Encoding.UTF7))
                {
                    var funcionariosDatatable = MontarDataTableFromArquivoFromStream(reader);

                    funcionariosLista = MontarListaFuncionariosFromDataTable(funcionariosDatatable);
                }
            }

            if (funcionariosLista.Count > 0)
            {
                //Lógica para subir as informações para o banco de dados

                restModel.Mensagem = "Os dados foram importados com sucesso";
                restModel.Sucesso = true;
                restModel.ListaFuncionariosImportados = funcionariosLista;
            }

            return restModel;
        }

        private static List<Funcionario> MontarListaFuncionariosFromDataTable(DataTable funcionariosDatatable)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

            foreach (DataRow row in funcionariosDatatable.Rows)
            {
                Funcionario func = new Funcionario();

                func.Nome = row["Nome"].ToString();
                func.Profissao = row["Profissão"].ToString();
                func.Sexo = row["Sexo"].ToString();
                func.DataNascimento = Convert.ToDateTime(row["Data de Nascimento"].ToString());

                funcionarios.Add(func);
            }

            return funcionarios;
        }

        //Usando esse método, você terá todas as colunas do arquivo e se não as quiser, terá de fazer algumas validações
        private static List<Funcionario> MontarListaFuncionariosFromArquivoString(string arquivo)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            var dados = File.ReadAllLines(arquivo, Encoding.UTF7);

            string[] cabecalhos = dados[0].Split(';');

            //Remove o cabeçalho
            dados = dados.Skip(1).ToArray();

            foreach (var dado in dados)
            {
                string[] linha = dado.Split(';');

                Funcionario func = new Funcionario();
                func.Nome = linha[0];
                func.DataNascimento = Convert.ToDateTime(linha[1]);
                func.InformacaoAvulsa1 = linha[2];
                func.Profissao = linha[3];
                func.Sexo = linha[4];
                func.InformacaoAvulsa2 = linha[5];

                funcionarios.Add(func);
            }

            return funcionarios;
        }

        // Usando esse método, você pode pegar as colunas do arquivo que deseja e colocar num datatable
        private static DataTable MontarDataTableFromArquivoFromStream(StreamReader arquivoStream)
        {
            DataTable dataTable = new DataTable();

            while (!arquivoStream.EndOfStream)
            {
                string linha = arquivoStream.ReadLine();

                if (linha.ToUpperInvariant().Contains("NOME"))
                {
                    string[] cabecalhos = linha.Split(';');

                    foreach (var cabecalho in cabecalhos)
                    {
                        DataColumn dataColumn = new DataColumn(cabecalho);
                        dataTable.Columns.Add(dataColumn);
                    }

                    continue;
                }

                string[] dados = linha.Split(';');

                dataTable.Rows.Add(dados);
            }

            return dataTable;
        }
    }
}

