using System;

namespace PesssoaLibrary
{
    [Serializable]
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataDeNascimento { get; set; }

        public int CalcularDataProximoAniversario()
        {
            DateTime dataAtual = DateTime.Today;
            DateTime proximaData = DataDeNascimento.AddYears(dataAtual.Year - DataDeNascimento.Year);

            if (proximaData < dataAtual)
                proximaData = proximaData.AddYears(1);

            int numeroDias = (proximaData - dataAtual).Days;

            return numeroDias;
        }

        public string ExibirFelicitacoes()
        {

            string felicitacao = $"\t Parabéns, {Nome} {Sobrenome}!" +
                $"\n\t Hoje é seu aniversário :)\n";

            return felicitacao;
        }

        public string ExibirDadosCadastrados()
        {
            string dadosPessoais = $"\nNome completo: {Nome} {Sobrenome}" +
                $"\nData de Nascimento: {DataDeNascimento.ToString("d")}" +
                $"\nFaltam {CalcularDataProximoAniversario()} dias para seu próximo aniversário";
            return dadosPessoais;
        }
    }
}