namespace CSVTrades.Models
{
    public class Trade
    {
        public string Horario { get; private set; }
        public string Conta { get; private set; }
        public string Moeda { get; private set; }
        public double Entrou { get; private set; }
        public double Saiu { get; private set; }
        public double Saldo { get; private set; }

        public Trade() { }

        public Trade(string horario, string conta, string moeda, double entrou, double saiu, double saldo)
        {
            Horario = horario;
            Conta = conta;
            Moeda = moeda;
            Entrou = entrou;
            Saiu = saiu;
            Saldo = saldo;
        }
    }
}
