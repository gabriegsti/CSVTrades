using System;
using System.Globalization;

namespace CSVTrades.Models
{
    public class Transaction
    {
        public string Horario { get; private set; }
        public string Conta { get; private set; }
        public string TipoDeOperacao { get; private set; }
        public string MoedaQueEntrou { get; private set; }
        public decimal Entrou { get; private set; }
        public string MoedaQueSaiu { get; private set; }
        public decimal Saiu { get; private set; }
        public string MoedaDeTaxa { get; private set; }
        public decimal Taxa { get; private set; }
        public decimal MoedaRecebidaEmReais { get; private set; }
        public decimal MoedaEnviadaEmReais { get; private set; }

        public Transaction() { }

        public Transaction(string horario, string conta, string tipoDeOperacao,
            string moedaQueEntrou, string entrou, string moedaQueSaiu, string saiu, string moedaDeTaxa,
            string taxa, string moedaRecebidaEmReais, string moedaEnviadaEmReais)
        {
            Horario = horario;
            Conta = conta;
            TipoDeOperacao = tipoDeOperacao;
            MoedaQueEntrou = moedaQueEntrou;
            Entrou = ToDecimal(entrou);
            MoedaQueSaiu = moedaQueSaiu;
            Saiu = ToDecimal(saiu);
            MoedaDeTaxa = moedaDeTaxa;
            Taxa = ToDecimal(taxa);
            MoedaRecebidaEmReais = ToDecimal(moedaRecebidaEmReais);
            MoedaEnviadaEmReais = ToDecimal(moedaEnviadaEmReais);
        }

        public override string ToString()
        {
            return "Horario: " + Horario + "\n" +
                "Conta: " + Conta + "\n" +
                "TipoDeOperacao: " + TipoDeOperacao + "\n" +
                "MoedaQueEntrou: " + MoedaQueEntrou + "\n" +
                "Entrou: " + Entrou + "\n" +
                "MoedaQueSaiu: " + MoedaQueSaiu + "\n" +
                "Saiu: " + Saiu + "\n" +
                "MoedaDeTaxa: " + MoedaDeTaxa + "\n" +
                "Taxa: " + Taxa + "\n" +
                "MoedaRecebidaEmReais: " + MoedaRecebidaEmReais + "\n" +
                "MoedaEnviadaEmReais: " + MoedaEnviadaEmReais + "\n";
        }

        public static decimal ToDecimal(string value)
        {
            if (value.Contains(","))
            {
                return decimal.Parse(value, CultureInfo.CreateSpecificCulture("pt-BR"));
            }
            else
            {
                return 0.0M;
            }
        }
    }
}
