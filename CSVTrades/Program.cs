using CSVTrades.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVTrades
{
    public class Program
    {
        public static string aluno = "aluno 03";
        public static string csv = string.Format($@"C:\Users\{aluno}\Downloads\transferoacademy_transações2.csv");

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



            foreach (Transaction t in filtered)
            {
                Console.WriteLine(t.ToString());
                Console.WriteLine(t.MoedaQueSaiu.GetType());
                Console.WriteLine();
            }

            Console.WriteLine("Acabou");
            Console.ReadKey();
        }
    }
}
