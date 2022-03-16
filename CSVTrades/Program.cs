using CSVTrades.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVTrades
{
    public class Program
    {
        
        public static string csv = string.Format($@"C:\Users\gabri\Downloads\transferoacademy_transações(2).csv");

        static void Main(string[] args)
        {
            StreamReader streamReader = new StreamReader(File.OpenRead(csv));

            //Console.WriteLine(streamReader.ReadLine());

            string line;
            string[] separated;

            List<Transaction> list = new List<Transaction>();

            while (true)
            {
                line = streamReader.ReadLine();

                //Console.WriteLine($"{index}: {line}");

                if (line == null) break;

                separated = line.Split(';');

                if (!separated[0].Contains("horario"))
                {
                    Transaction t = new Transaction(
                                            horario: separated[0],
                                            conta: separated[1],
                                            tipoDeOperacao: separated[2],
                                            moedaQueEntrou: separated[3],
                                            entrou: separated[4],
                                            moedaQueSaiu: separated[5],
                                            saiu: separated[6],
                                            moedaDeTaxa: separated[7],
                                            taxa: separated[8],
                                            moedaRecebidaEmReais: separated[9],
                                            moedaEnviadaEmReais: separated[10]
                                        );

                    list.Add(t);
                    // Console.WriteLine($"{t.DateTime}: {t.Acount}: {t.ValueCameOut}");
                }


            }
            //"conta da Binance"
            //"29/12/2021 15:44"
            //"26/11/2020 00:33"
            //"conta da FTX"
            //"deposit"
            IEnumerable<Transaction> filtered = list.Where(item => item.Conta.Contains("conta da Binance"));


            /*
            foreach (Transaction t in filtered)
            {
                Console.WriteLine(t.ToString());
                Console.WriteLine(t.MoedaQueSaiu.GetType());
                Console.WriteLine();
            }
            */

            HistoricoSaldo(list);

            Console.WriteLine("Acabou");
            Console.ReadKey();
        }

        static void HistoricoSaldo(List<Transaction> list)
        {

            List<Trade> registros = new List<Trade>();
            List<string> moedasNaLista = ListaDeMoedas(list);
            decimal[] saldoFinal = new decimal[moedasNaLista.Count()];
            string primeiraLinha = "horario;Conta;Moeda;Entrou;Saiu;Saldo";
            foreach (var item in list)
            {


                switch (item.TipoDeOperacao)
                {
                    case "trade":
                        if (moedasNaLista.IndexOf(item.MoedaQueEntrou) != -1)
                        {
                            saldoFinal[moedasNaLista.IndexOf(item.MoedaQueEntrou)] += item.Entrou;
                            registros.Add(new Trade(item.Horario, item.Conta, item.MoedaQueEntrou, item.Entrou,  0 , saldoFinal[moedasNaLista.IndexOf(item.MoedaQueEntrou)])) ;
                        }

                        if (moedasNaLista.IndexOf(item.MoedaQueSaiu) != -1)
                        {
                            saldoFinal[moedasNaLista.IndexOf(item.MoedaQueSaiu)] -= item.Saiu;
                            registros.Add(new Trade(item.Horario, item.Conta, item.MoedaQueSaiu, 0, item.Saiu, saldoFinal[moedasNaLista.IndexOf(item.MoedaQueSaiu)]));
                        }

                        if (moedasNaLista.IndexOf(item.MoedaDeTaxa) != -1)
                        {
                            saldoFinal[moedasNaLista.IndexOf(item.MoedaDeTaxa)] -= item.Taxa;
                            registros.Add(new Trade(item.Horario, item.Conta, item.MoedaDeTaxa, 0, item.Taxa, saldoFinal[moedasNaLista.IndexOf(item.MoedaDeTaxa)]));
                        }
                        break;

                    case "deposit":
                        if (moedasNaLista.IndexOf(item.MoedaQueEntrou) != -1)
                        {
                            saldoFinal[moedasNaLista.IndexOf(item.MoedaQueEntrou)] += item.Entrou;
                            registros.Add(new Trade(item.Horario, item.Conta, item.MoedaQueEntrou, item.Entrou, 0, saldoFinal[moedasNaLista.IndexOf(item.MoedaQueEntrou)]));
                        }
                        break;

                    case "withdraw":
                        if (moedasNaLista.IndexOf(item.MoedaQueSaiu) != -1)
                        {
                            saldoFinal[moedasNaLista.IndexOf(item.MoedaQueSaiu)] -= item.Saiu;
                            registros.Add(new Trade(item.Horario, item.Conta, item.MoedaQueSaiu, 0, item.Saiu, saldoFinal[moedasNaLista.IndexOf(item.MoedaQueSaiu)]));
                        }
                        if (moedasNaLista.IndexOf(item.MoedaDeTaxa) != -1)
                        {
                            saldoFinal[moedasNaLista.IndexOf(item.MoedaDeTaxa)] -= item.Taxa;
                            registros.Add(new Trade(item.Horario, item.Conta, item.MoedaDeTaxa, 0, item.Taxa, saldoFinal[moedasNaLista.IndexOf(item.MoedaDeTaxa)]));
                        }
                        break;
                }
               
            }
            var csv = new StringBuilder();
            if (File.Exists(@"C:\Users\gabri\Documents\resultado.csv"))
            {
                File.Delete(@"C:\Users\gabri\Documents\resultado.csv");
            }
            
            var listaOrdenada = registros.OrderBy(x => x.Moeda).ToList();
            
            csv.AppendLine(primeiraLinha);

            foreach (var i in listaOrdenada)
            {
              

                
                var item = i.ToString();
                
                if (i.Moeda == "BCH" || i.Moeda == "DOGE" || i.Moeda == "USDT" || i.Moeda == "XLM" || i.Moeda == "XRP")
                {
                    var newLine = item;
                    csv.AppendLine(newLine);
                }
              

            }
            File.WriteAllText(@"C:\Users\gabri\Documents\resultado.csv", csv.ToString());
            
        }
        static List<string> ListaDeMoedas(List<Transaction> list)
        {
            HashSet<string> moedasNaListaSemRepeticao = new HashSet<string>();
            List<string> moedasNaLista = new List<string>();

            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.MoedaQueEntrou))
                    moedasNaListaSemRepeticao.Add(item.MoedaQueEntrou);

                if (!string.IsNullOrEmpty(item.MoedaQueSaiu))
                    moedasNaListaSemRepeticao.Add(item.MoedaQueSaiu);

                if (!string.IsNullOrEmpty(item.MoedaDeTaxa))
                    moedasNaListaSemRepeticao.Add(item.MoedaDeTaxa);
            }
            moedasNaLista = moedasNaListaSemRepeticao.ToList();
            return moedasNaLista;
        }
    }
}
