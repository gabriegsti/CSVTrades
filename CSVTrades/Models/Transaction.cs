using System.Globalization;

namespace CSVTrades.Models
{
    public class Transaction
    {
        public string Horario { get; private set; }
        public string Conta { get; private set; }
        public string TipoDeOperacao { get; private set; }
        public string MoedaQueEntrou { get; private set; }
        public double Entrou { get; private set; }
        public string MoedaQueSaiu { get; private set; }
        public double Saiu { get; private set; }
        public string MoedaDeTaxa { get; private set; }
        public double Taxa { get; private set; }
        public double MoedaRecebidaEmReais { get; private set; }
        public double MoedaEnviadaEmReais { get; private set; }

        public Transaction() { }

        public Transaction(string horario, string conta, string tipoDeOperacao,
            string moedaQueEntrou, string entrou, string moedaQueSaiu, string saiu, string moedaDeTaxa,
            string taxa, string moedaRecebidaEmReais, string moedaEnviadaEmReais)
        {
            Horario = horario;
            Conta = conta;
            TipoDeOperacao = tipoDeOperacao;
            MoedaQueEntrou = moedaQueEntrou;
            Entrou = ToDouble(entrou);
            MoedaQueSaiu = moedaQueSaiu;
            Saiu = ToDouble(saiu);
            MoedaDeTaxa = moedaDeTaxa;
            Taxa = ToDouble(taxa);
            MoedaRecebidaEmReais = ToDouble(moedaRecebidaEmReais);
            MoedaEnviadaEmReais = ToDouble(moedaEnviadaEmReais);
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

        public static double ToDouble(string value)
        {
            if (value.Contains(","))
            {
                return double.Parse(value, CultureInfo.CreateSpecificCulture("pt-BR"));
            }
            else
            {
                return double.Parse(value);
            }
        }
    }
}
