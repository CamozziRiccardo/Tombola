using System;
using System.Configuration;
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
            Console.SetCursorPosition(22, 1);           //posizionamento centrato per generazione tabellone
            Console.WriteLine("Tabellone: ");
            for(int i = 0; i < 9; i++)
            {
                x = 13;                                 //posizionamento tabellone
                for (int j = 2; j < 12; j++)
                {
                    Console.SetCursorPosition(x, y);    //posizionamento cursore per stampa
                    Console.WriteLine(var);             //stampa variabile
                    var++;                              //incremento variabile di stampa
                    x += 3;
                }
                y++;
            }

            //generazione cartella 1
            gcart1();

            //generazione cartella 2
            gcart2();

            //caricamento matrice della prima cartella
            mcart1();

            //caricamento matrice della seconda cartella
            mcart2();

            //estrazione dei numeri e controllo
            for (int i = 0; i < 90; i++)
            {
                num = estrazione();                                 //estrazione numero attraverso funzione apposita
                x = Cx();                                           //calcolo delle x
                y = Cy();                                           //calcolo delle y

                //evidenziamento numero sul tabellone
                Console.SetCursorPosition(x, y);                    //impostare posizione per evidenziare
                Console.BackgroundColor = ConsoleColor.Green;       //impostazione colore di evidenziamento

                //evidenziamento colore sulle cartelle attraverso funzioni apposita
                contcart1();
                contcart2();
                Thread.Sleep(500);
            }

            //Funzioni {

                //funzione di estrazione
                int estrazione()
                {
                    int num1;                                       //dichiarazione variabile locale
                    do                                              //ciclo per l'estrazione di un numero non estratto
                    {
                        num1 = r.Next(1, 91);                       //generazione numero casuale
                    } while (b[num1-1] == true);                    //verifica che il numero non sia ancora stato estratto
                    b[num1-1] = true;                               //segnare il numero estratto attraverso l'array booleano creato precedentemente
                    return num1;                                    //ritornare il valore estratto
                }

                //funzione di calcolo della x per l'evidenziazione di un numero sul tabellone
                int Cx()
                {
                    if (num / 10 == 0)                              //controllo che il numero abbia 0 come decina (minore di 10)
                    {
                        x = 11 + (num % 10 * 3);                    //calcolo delle x
                    }
                    else                                            //istruzione nel caso il numero non abbia 0 come decina (minore di 10)
                    {
                        if (num % 10 != 0)                          //verifica che il numero non sia multiplo di dieci
                        {
                            x = 11 + (num % 10 * 3 - 1);            //calcolo delle x
                        }
                        else                                        //istruzione nel caso il numero sia multiplo di 10
                        {
                            x = 11 + num / (num / 10) * 3 - 1;      //calcolo delle x
                        }
                    }
                    return x;                                       //ritornare il valore calcolato
                }

            //funzione di calcolo della y per l'evidenziazione di un numero sul tabellone


            //}
        }
    }
}
