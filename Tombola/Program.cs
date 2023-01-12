using System;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace Tombola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //disattivazione del cursore
            Console.CursorVisible = false;

            //variabile di stampa del tabellone
            int var = 1;

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
                    Console.Write(var);                 //stampa numeri
                    var++;                              //aumento della variabile di stampa
                    x += 3;
                }
                y++;
            }

            //generazione cartella 1
            mcart1();

            //generazione cartella 2
            mcart2();

            //caricamento matrice della prima cartella
            gcart1();

            //caricamento matrice della seconda cartella
            gcart2();

            //estrazione dei numeri e controllo
            for (int i = 0; i < 90; i++)
            {
                num = estrazione();                                 //estrazione numero attraverso funzione apposita
                x = Cx();                                           //calcolo delle x
                y = Cy();                                           //calcolo delle y
                for (int j = 0; j < 3; j++)                         //evidenziamento numero sul tabellone
                {
                    Console.SetCursorPosition(x, y);                //impostare posizione per evidenziare
                    Console.ForegroundColor = ConsoleColor.Green;   //impostare colore scritta per lettura efficente
                    Console.WriteLine(num);
                    Thread.Sleep(100);
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(num);
                    Thread.Sleep(100);

                }
                Console.SetCursorPosition(x, y);                    //impostazione colore di background per evidenziare il numero estratto
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(num);
                Console.BackgroundColor = ConsoleColor.Black;       //reimpostazione del background al colore originale della console per il numero seuccessivo

                //evidenziamento colore sulle cartelle attraverso funzioni apposita
                evidcart1();
                evidcart2();
                endl();
                Thread.Sleep(250);
            }

            //Funzioni
            //{

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
                        x = 10 + (num % 10 * 3);                    //calcolo delle x
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
                int Cy()
                {
                    if (num / 10 == 0)                              //controllo che il numero abbia 0 come decina (minore di 10)
                    {
                        y = 2;                                      //calcolo delle y
                    }
                    else                                            //istruzione nel caso il numero non abbia 0 come decina (minore di 10)
                    {
                        if (num % 10 != 0)                          //verifica che il numero non sia multiplo di dieci
                        {
                            y = 2 + num / 10;                       //calcolo delle y
                        }
                        else                                        //istruzione nel caso il numero sia multiplo di 10
                        {
                            y = 1 + num / 10;                       //calcolo delle y
                        }
                    }
                    return y;                                       //ritornare il valore calcolato
                }

                int mcart1()                                        //funzione di generazione della matrice della prima cartella
                {
                    bool[] cartv = new bool[90];                    //generazione array booleano locale per verificare l'univocità dei numeri generati
                    int numr;                                       //dichiarazione di una variabile locale
                    for (int i = 0; i < 3; i++)                     //ciclo per l'identificazione delle righe
                    {

                        bool[] dec = new bool[10];                  //array booleano per verifica delle decine
                        for (int j = 0; j < 5; j++)                 //ciclo di generazione delle righe
                        {
                            do                                      //ciclo di estrazione affinche il numero non venga ripetuto
                            {
                                numr = r.Next(1, 91);               //estrazione numero casuale
                            } while (cartv[numr - 1] == true || dec[numr / 10] == true);    //verifica che il numero non venga ripetuto e che la decina stessa non venga ripetuta
                            cartv[numr - 1] = true;                 //segnare il numero generato come già presente tramite l'array dichiarato precedentemente
                            dec[numr / 10] = true;                  //segnare la decina del numero generato come già presente tramite l'array dichiarato precedentemente
                            if (numr == 90)                         //spostamento del valore 90 nell'ottava colonna
                            {
                                car1[8, i] = 90;                    //assegnazione del valore 90 nell'ottava colonna
                            }
                            else
                            {
                                car1[numr / 10, i] = numr;          //assegnazione del valore all'apposita colonna
                            }
                        }
                        for (int k = 0; k < 9; k++)
                        {
                            dec[k] = false;                         //assegnazione false per l'array booleano nelle decine per la generazione della nuova riga
                        }
                    }
                    return 0;
                }

                int mcart2()                                        //funzione di generazione della matrice della prima cartella
                {
                    bool[] cartv = new bool[90];                    //generazione array booleano locale per verificare l'univocità dei numeri generati
                    int numr;                                       //dichiarazione di una variabile locale
                    for (int i = 0; i < 3; i++)                     //ciclo per l'identificazione delle righe
                    {

                        bool[] dec = new bool[10];                  //array booleano per verifica delle decine
                        for (int j = 0; j < 5; j++)                 //ciclo di generazione delle righe
                        {
                            do                                      //ciclo di estrazione affinche il numero non venga ripetuto
                            {
                                numr = r.Next(1, 91);               //estrazione numero casuale
                            } while (cartv[numr - 1] == true || dec[numr / 10] == true);    //verifica che il numero non venga ripetuto e che la decina stessa non venga ripetuta
                            cartv[numr - 1] = true;                 //segnare il numero generato come già presente tramite l'array dichiarato precedentemente
                            dec[numr / 10] = true;                  //segnare la decina del numero generato come già presente tramite l'array dichiarato precedentemente
                            if (numr == 90)                         //spostamento del valore 90 nell'ottava colonna
                            {
                                car2[8, i] = 90;                    //assegnazione del valore 90 nell'ottava colonna
                            }
                            else
                            {
                                car2[numr / 10, i] = numr;          //assegnazione del valore all'apposita colonna
                            }
                        }
                        for (int k = 0; k < 9; k++)
                        {
                            dec[k] = false;                         //assegnazione false per l'array booleano nelle decine per la generazione della nuova riga
                        }
                    }
                    return 0;
                }

                void gcart1()                                       //funzione di stampa della prima cartella
                {
                    x = 0;                                          //assegnazione di x
                    y = 12;                                         //assegnazione di x
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("Cartella 1: ");
                    y++;
                    for (int i = 0; i < 5; i++)                     //ciclo di stampa della cartella
                    {
                        x = 0;
                        y++;
                        if (i % 2 == 1)                             //controlla stampa (trattini o numeri)
                        {
                            Console.SetCursorPosition(x, y);
                            Console.WriteLine("-------------------------");
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y);
                            for (int j = 0; j < 9; j++)             //ciclo stampa spazi o numeri
                            {
                                if (car1[j, i / 2 + i % 2] != 0)    //condizione che verifica se è necessario stampare un numero o lo spazio
                                {
                                    Console.Write($"{car1[j, i / 2 + i % 2]} ");
                                }
                                else
                                {
                                    if (j == 0)                     //controllo per la stampa di due spazi o tre spazi
                                    {
                                        Console.Write("  ");        //stampa di due spazi
                                    }
                                    else
                                    {
                                        Console.Write("   ");       //stampa di tre spazi
                                    }
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                }

                void gcart2()                                       //funzione di stampa della seconda cartella
                {
                    x = 30;                                         //assegnazione di 30 ad x per la posizione iniziale della cartella
                    y = 12;                                         //assegnazione di 30 ad y per la posizione iniziale della cartella
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("Cartella 2: ");
                    y++;
                    for (int i = 0; i < 5; i++)                     //ciclo di stampa della seconda cartella
                    {
                        x = 30;
                        y++;
                        if (i % 2 == 1)                             //condizione che verifica se la riga è di trattini o numeri
                        {
                            Console.SetCursorPosition(x, y);
                            Console.WriteLine("-------------------------");
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y);
                            for (int j = 0; j < 9; j++)             //ciclo di stampa dei numeri o degli spazi
                            {
                                if (car2[j, i / 2 + i % 2] != 0)    //condizione di stampa del numero
                                {
                                    Console.Write($"{car2[j, i / 2 + i % 2]} ");
                                }
                                else
                                {
                                    if (j == 0)
                                    {
                                        Console.Write("  ");        //stampa di due spazi
                                    }
                                    else
                                    {
                                        Console.Write("   ");       //stampa di tre spazi
                                    }
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                }

                int evidcart1()                                     //funzione di evidenziazione della prima cartella e segnalazione del vincitore
                {
                    x = 0;
                    y = 14;
                    for (int k = 0; k < 3; k++)                     //cicli di identificazione
                    {
                        for (int j = 0; j < 9; j++)                 //ciclo di identificazione dei numeri estratti e non
                        {
                            if (car1[j, k] == num)                  //condizione di verifica dell'estrazione
                            {
                                if (j == 0)                         //condizione di verifica che il numero sia minore o uguale a 10
                                {
                                    x = 0;
                                }
                                else
                                {
                                    x += j * 3 - 1;                 //calcolo delle x attraverso le decine del numero
                                }
                                y += k * 2;                         //calcolo delle y in base alla riga presa in considerazione
                                car1t++;                            //incremento del contatore per la tombola
                            for (int f = 0; f < 3; f++)             //evidenziamento numero sul tabellone
                            {
                                Console.SetCursorPosition(x, y);                    //impostare posizione per evidenziare
                                Console.ForegroundColor = ConsoleColor.Green;       //impostare colore scritta per lettura efficente
                                Console.WriteLine(num);
                                Thread.Sleep(50);
                                Console.SetCursorPosition(x, y);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine(num);
                                Thread.Sleep(50);

                            }
                            Console.SetCursorPosition(x, y);                        //impostazione colore di background per evidenziare il numero estratto
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(num);
                            Console.BackgroundColor = ConsoleColor.Black;           //reimpostazione del background al colore originale della console per il numero seuccessivo
                            }
                        }
                    }
                    return car1t;
                }

                int evidcart2()                                 //funzione di evidenziazione della seconda cartella e segnalazione del vincitore
                {
                    x = 30;
                    y = 14;
                    for (int k = 0; k < 3; k++)                 //cicli di identificazione
                    {
                        for (int j = 0; j < 9; j++)             //ciclo di identificazione dei numeri estratti e non
                        {
                            if (car2[j, k] == num)              //condizione di verifica dell'estrazione
                            {
                                if (j == 0)                     //condizione di verifica che il numero sia minore o uguale a 10
                                {
                                    x = 30;
                                }
                                else
                                {
                                    x += j * 3 - 1;             //calcolo delle x attraverso le decine del numero
                                }
                                y += k * 2;                     //calcolo delle y in base alla riga presa in considerazione
                                car2t++;                        //incremento del contatore per la tombola
                                for (int f = 0; f < 3; f++)     //evidenziamento numero sul tabellone
                                {
                                    Console.SetCursorPosition(x, y);                //impostare posizione per evidenziare
                                    Console.ForegroundColor = ConsoleColor.Green;   //impostare colore scritta per lettura efficente
                                    Console.WriteLine(num);
                                    Thread.Sleep(50);
                                    Console.SetCursorPosition(x, y);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine(num);
                                    Thread.Sleep(50);

                                }
                                Console.SetCursorPosition(x, y);                    //impostazione colore di background per evidenziare il numero estratto
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine(num);
                                Console.BackgroundColor = ConsoleColor.Black;       //reimpostazione del background al colore originale della console per il numero seuccessivo

                            }
                        }
                    }
                    return car2t;
                }
                int endl()
                {
                    if (car1t == 15 && car2t == 15)                     //condizione dell'eventuale tombola di entrambi i giocatori
                    {
                        Console.SetCursorPosition(13, 21);
                        Console.Write("Entrambi i giocatori hanno vinto");
                        Console.SetCursorPosition(0, 0);
                        return 0;                                           //chiusura del programma
                    }
                    else if (car1t == 15)                               //condizione di verifica dell'eventuale tombola del giocatore 1
                    {
                        Console.SetCursorPosition(0, 20);
                        Console.Write("Il giocatore 1 ha fatto tombola");
                        Console.SetCursorPosition(0, 0);
                        return 0;                            //chiusura del programma
                    }
                    else if (car2t == 15)                               //condizione di verifica dell'eventuale tombola del giocatore 2
                    {
                        Console.SetCursorPosition(30, 20);
                        Console.Write("Il giocatore 2 ha fatto tombola");
                        Console.SetCursorPosition(0, 0);
                        return 0;                            //chiusura del programma
                    }
                    return 0;
                }

            //}
        }
    }
}
