namespace CSVTrades.Models
{
    public class Trade
    {
        public string Horario { get; private set; }
        public string Conta { get; private set; }
        public string Moeda { get; private set; }
        public decimal Entrou { get; private set; }
        public decimal Saiu { get; private set; }
        public decimal Saldo { get; private set; }

        public Trade() { }

        public Trade(string horario, string conta, string moeda, decimal entrou, decimal saiu, decimal saldo)
        {
            Horario = horario;
            Conta = conta;
            Moeda = moeda;
            Entrou = entrou;
            Saiu = saiu;
            Saldo = saldo;
        }

        public override string ToString()
        {
            return Horario + ";" +
                Conta + ";" +
                Moeda + ";" +
                Entrou + ";" +
                Saiu + ";" +
                Saldo;
        }
    }
}
