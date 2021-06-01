using System;
using System.Threading;

//Kelvin Mozart  RA: 221150295

//Victor Milani  RA: 221170533

namespace Bio_Diesel
{
    class Program
    {
        public static float qtd_oleo = 0;
        public static float qtd_naoh = 0;
        public static float qtd_etoh = 0;
        public static int tempo_oleo = 1;
        public static float qtd_total_naoh_reator = 0;
        public static float qtd_total_etoh_reator = 0;
        public static float qtd_total_oleo_reator = 0;
        public static int ativa_reator = 0;
        public static int qtd_mistura = 0;
        public static float qtd_total_decantador = 0;
        public static int tempo_dec = 1;
        public static float qtd_glicerina = 0;
        public static float qtd_etoh_decantador = 0;
        public static float qtd_lavagem = 0;
        public static float qtd_mistura_decantador = 0;
        public static float qtd_total_glicerina = 0;
        public static float qtd_biodiesel_total = 0;
        public static int ciclo = 0;

        static void Main(string[] args) //Kelvin
        {
            var rand = new Random();
            
            while( true )
            {
                float j = rand.Next() % 10;
                qtd_oleo = (float)j / 10 + 1;
                //Console.WriteLine(qtd_oleo);

                var t1 = new Thread(() => tanque_oleo(qtd_oleo));
                t1.Start();
                t1.Join();

                var t2 = new Thread(() => tanque_naoh_etoh());
                t2.Start();
                t2.Join();

                //Program.tanque_oleo(qtd_oleo);
                Thread.Sleep(1000);
                //Program.tanque_naoh_etoh();
            }
        }

        public static void tanque_oleo(float qtd_oleo)  //Kelvin
        {
            Console.WriteLine("----------TANQUE DE OLEO----------");

            if(tempo_oleo == 5)
            {
                float qtd_total_oleo = 0;
                Console.WriteLine("| Recebendo " + qtd_oleo + "L |");
                qtd_total_oleo += qtd_oleo;
                tempo_oleo = 1;

                if(qtd_total_oleo >= 1.25)
                {
                    qtd_total_oleo -= 1.25f;
                    Console.WriteLine("| Transferindo 1.25L de oleo para o reator |");
                    Console.WriteLine("| Quantidade de oleo restante: " + qtd_total_oleo + "L |");

                    var treator = new Thread(() => reator(0.0f, 0.0f, 1.25f));
                    treator.Start();
                    treator.Join();
                }
                Console.WriteLine("--------------------------------------");
            }
            else
            {
                Console.WriteLine(" Tanque de Oleo nao abastecido");
                tempo_oleo++;
            }
        }

        public static void tanque_naoh_etoh()  //Kelvin
        {
            Console.WriteLine(" ----------TANQUE NAOH/ETOH----------");
            Console.WriteLine(" Quantidade total de naoh: " + qtd_naoh + "L");
            Console.WriteLine(" Quantidade total de etoh: " + qtd_etoh + "L");
            Console.WriteLine("--------------------------------------");

            qtd_naoh += 0.25f;
            qtd_etoh += 0.125f;

            var t2reator = new Thread(() => reator(0.125f, 0.25f, 0.0f));
            t2reator.Start();
            t2reator.Join();
        }

        public static void reator(float qtd_naoh, float qtd_etoh, float qtd_oleo)  //Victor
        {
            ciclo++;
            Console.WriteLine("--------------------------------------");
            Console.WriteLine(" ----------REATOR----------");
            Console.WriteLine("Ciclo: " + ciclo);
            Console.WriteLine(" Recebendo " + qtd_naoh + " de naoh");
            Console.WriteLine(" Recebendo " + qtd_etoh + " de etoh");
            Console.WriteLine(" Recebendo " + qtd_oleo + " de oleo");

            qtd_total_naoh_reator += qtd_naoh;
            qtd_total_etoh_reator += qtd_etoh;
            qtd_total_oleo_reator += qtd_oleo;

            Console.WriteLine(" Quantidade total de naoh no reator " + qtd_total_naoh_reator);
            Console.WriteLine(" Quantidade total de etoh no reator " + qtd_total_etoh_reator);
            Console.WriteLine(" Quantidade total de oleo no reator " + qtd_total_oleo_reator);
            Console.WriteLine("--------------------------------------");
        
            if(qtd_total_naoh_reator >= 1.25 && qtd_total_etoh_reator >= 2.50 && qtd_total_oleo_reator >= 1.25)
            {
                ativa_reator = 1;

                Console.WriteLine(" ----------REATOR ATIVO----------");

                qtd_total_naoh_reator -= 1.25f;
                qtd_total_etoh_reator -= 2.50f;
                qtd_total_oleo_reator -= 1.25f;
                qtd_mistura = 5;

                var tdecantador = new Thread(() => decantador(qtd_mistura));
                tdecantador.Start();
                tdecantador.Join();
            }
            else
            {
                var t2decantador = new Thread(() => decantador(0));
                t2decantador.Start();
                t2decantador.Join();
            }
        }

        public static void decantador(float qtd_mistura)  //Victor
        {
            Console.WriteLine(" ----------DECANTADOR----------");
            qtd_total_decantador += qtd_mistura;

            if (tempo_dec == 5)
            {
                if (qtd_total_decantador >= 3f)
                {
                    qtd_glicerina = qtd_total_decantador * 0.05f;
                    qtd_etoh_decantador = qtd_total_decantador * 0.13f;
                    qtd_lavagem = qtd_total_decantador * 0.82f;
                    qtd_mistura_decantador = qtd_glicerina + qtd_etoh + qtd_lavagem;
                    qtd_total_decantador = qtd_total_decantador - 3.0f;

                    Console.WriteLine(" DECANTADOR PROCESSOU 3L");
                    Console.WriteLine(" Quantidade de mistura no decantador: " + qtd_total_decantador + "L");
                    
                    tempo_dec = 1;
                    
                    Console.WriteLine("--------------------------------------");

                    var ttanque_glicerina = new Thread(() => tanque_glicerina(qtd_glicerina));
                    ttanque_glicerina.Start();
                    ttanque_glicerina.Join();

                    var ttanque_lavagem = new Thread(() => tanque_lavagem(qtd_mistura_decantador));
                    ttanque_lavagem.Start();
                    ttanque_lavagem.Join();
                }
                else
                {
                    Console.WriteLine(" MISTURA INSUFICIENTE PARA PROCESSAMENTO! ");
                    Console.WriteLine(" Quantidade de mistura disponivel: " + qtd_total_decantador + "L");
                    Console.WriteLine("--------------------------------------");
                }
            }
            else
            {
                Console.WriteLine(" Decantador nao foi ativado");
                Console.WriteLine("--------------------------------------");
                
                tempo_dec++;
            }
        }

        public static void tanque_glicerina(float qtd_glicerina)  //Kelvin
        {
            Console.WriteLine(" ----------TANQUE DE GLICERINA----------");
            
            qtd_total_glicerina = qtd_total_glicerina + qtd_glicerina;

            Console.WriteLine(" Quantidade total de glicerina: " + qtd_total_glicerina);
            Console.WriteLine("--------------------------------------");
        }

        public static void tanque_lavagem(float qtd_mistura_decantador)  //Kelvin
        {
            Console.WriteLine(" ----------TANQUE DE LAVAGEM----------");
            Console.WriteLine(" Recebendo " + qtd_mistura_decantador + " de Solucao para lavagem.... ");
            
            qtd_mistura_decantador = qtd_mistura_decantador - (qtd_mistura_decantador * 0.15f);
            
            Console.WriteLine(" Quantidade de lavagem restante: " + qtd_mistura_decantador);

            var tsecadora = new Thread(() => secador(qtd_mistura_decantador));
            tsecadora.Start();
            tsecadora.Join();

            Console.WriteLine("--------------------------------------");
        }

        public static void secador(float qtd_mistura_decantador) //Victor
        {
            Console.WriteLine(" ----------SECADOR----------");

            if (qtd_naoh == 0f && qtd_etoh == 0f)
            {
                Console.WriteLine(" O SECADOR AINDA NÃO RECEBEU A MISTURA");
            }
            else
            {
                Console.WriteLine(" Recebendo " + qtd_mistura_decantador + " de mistura do tanque de lavagem");
                qtd_mistura_decantador = qtd_mistura_decantador - (qtd_mistura_decantador * 0.015f);
                Console.WriteLine(" Apos perder 3 por cento do volume, restaram " + qtd_mistura_decantador + " de mistura no secador");

                var ttanque_biodiesel = new Thread(() => tanque_biodiesel(qtd_mistura_decantador));
                ttanque_biodiesel.Start();
                ttanque_biodiesel.Join();    
            }
            Console.WriteLine("--------------------------------------");
        }

        public static void tanque_biodiesel(float qtd_biodiesel)  //Kelvin
        {
            Console.WriteLine(" ----------TANQUE DE BIODIESEL----------");
            qtd_biodiesel_total = qtd_biodiesel_total + qtd_biodiesel;
            Console.WriteLine(" Quantidade de Bio Diesel no Tanque: " + qtd_biodiesel_total);
            Console.WriteLine("--------------------------------------");
        }
    }
}
