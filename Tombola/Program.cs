using System;
using System.Threading;

namespace Tombola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //variabile stampa tabellone
            int var = 0;

            //dichiarazione variabili
            int num, x;
            int y = 2;

            //dichiarazione variabili cartelle per segnalazione tombola
            int car1t = 0;
            int car2t = 0;

            //dichiarazione matrici per cartelle
            int[,] car1 = new int[9, 3];
            int[,] car2 = new int[9, 3];

            //variabile random
            Random r = new Random();

            //dichiarazione di un array booleano
            bool[] b = new bool[90];

            //generazione tabellone
            Console.SetCursorPosition(22, 1);
            Console.WriteLine("Tabellone: ");
            for(int i = 0; i < 9; i++)
            {
                x = 13; //posizionamento tabellone
                for (int j = 2; j < 12; j++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(var);
                    var++;
                    x += 3;
                }
                y++;
            }
        }
    }
}
