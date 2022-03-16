using CSVTrades.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVTrades
{
    public class Program
    {
        //
        //public static string aluno = "aluno 03";
        //public static string csv = string.Format($@"C:\Users\{aluno}\Downloads\transferoacademy_transações2.csv");

        public static string csv = string.Format(@"C:\Users\gabri\Documents\transferoacademy_transacoes.csv");



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
            RealizaCalculo(list);
            //"conta da Binance"
            //"29/12/2021 15:44"
            //"26/11/2020 00:33"
            //"conta da FTX"
            //"deposit"
            IEnumerable<Transaction> filtered = list.Where(item => item.Conta.Contains("conta da Binance"));



            foreach (Transaction t in filtered)
            {
                Console.WriteLine(t.ToString());
                Console.WriteLine(t.MoedaQueSaiu.GetType());
                Console.WriteLine();
            }

            Console.WriteLine("Acabou");
            Console.ReadKey();
        }

        public static void RealizaCalculo(List<Transaction> list)
        {
            HashSet<string> moedasNaListaSemRepeticao = new HashSet<string>();
            List<string> moedasNaLista = new List<string>();
            List<decimal> saldoFinal = new List<decimal>();
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.MoedaQueEntrou))
                    moedasNaListaSemRepeticao.Add(item.MoedaQueEntrou);
                
                if (string.IsNullOrEmpty(item.MoedaQueSaiu))
                    moedasNaListaSemRepeticao.Add(item.MoedaQueSaiu);

                if (string.IsNullOrEmpty(item.MoedaDeTaxa))
                    moedasNaListaSemRepeticao.Add(item.MoedaDeTaxa);
            }
            moedasNaLista = moedasNaListaSemRepeticao.ToList();
            foreach (var item in list)
            {
                
                switch (item.TipoDeOperacao)
                {
                    case "trade":
                        if(moedasNaLista.IndexOf(item.MoedaQueEntrou) != -1) saldoFinal[moedasNaLista.IndexOf(item.MoedaQueEntrou)] += item.Entrou;
                        if(moedasNaLista.IndexOf(item.MoedaQueSaiu) != -1) saldoFinal[moedasNaLista.IndexOf(item.MoedaQueSaiu)] -= item.Saiu;
                        if(moedasNaLista.IndexOf(item.MoedaDeTaxa) != -1) saldoFinal[moedasNaLista.IndexOf(item.MoedaDeTaxa)] -= item.Taxa;
                        break;
                    case "deposit":
                        if (moedasNaLista.IndexOf(item.MoedaQueEntrou) != -1) saldoFinal[moedasNaLista.IndexOf(item.MoedaQueEntrou)] += item.Entrou;
                        break;
                    case "withdraw":
                        if (moedasNaLista.IndexOf(item.MoedaQueSaiu) != -1) saldoFinal[moedasNaLista.IndexOf(item.MoedaQueSaiu)] -= item.Saiu;
                        if (moedasNaLista.IndexOf(item.MoedaDeTaxa) != -1) saldoFinal[moedasNaLista.IndexOf(item.MoedaDeTaxa)] -= item.Taxa;
                        break;
                }
                
            }
            foreach(var item in moedasNaLista)
            {
                switch (item)
                {
                    case "BCH":
                        Console.WriteLine(item + " : " + saldoFinal[moedasNaLista.IndexOf(item)]);
                        break;
                    case "DOGE":
                        break;
                    case "USDT":
                        break;
                    case "XLM":
                        break;
                    case "XRP":
                        break;

                }
            }

        }
    }
}
