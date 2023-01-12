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
                    Thread.Sleep(250);
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
                                if (numr == 90)                     //verifica che il numero sia uguale a 90, in questo caso la procedura cambia
                                {
                                    j--;                            //diminuzione del contatore, poiche appartenendo alla colonna 8 potrebbe sovrascriverne un valore
                                }
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
                                if (numr == 90)                     //verifica che il numero sia uguale a 90, in questo caso la procedura cambia
                                {
                                    j--;                            //diminuzione del contatore, poiche appartenendo alla colonna 8 potrebbe sovrascriverne un valore
                                }
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

            void gcart2()//funzione di stampa della seconda cartella
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
            int evidcart1()//funzione di evidenziazione dei numeri estratti presenti nella cartella 1 e segnalazione di eventuale vincitore
            {
                x = 0;//assegnazione del valore 0 a x
                y = 14;//assegnazione del valore 14 a y
                for (int k = 0; k < 3; k++)//ciclo che identifica le righe della cartella
                {
                    for (int j = 0; j < 9; j++)//ciclo che identifica ogni numero presente/assente nella riga
                    {
                        if (car1[j, k] == num)//condizione che verifica la presenza del numero estratto
                        {
                            if (j == 0)//condizione che verifica se il numero estratto ha decina == 0
                            {
                                x = 0;//assegnazione del valore 0 a x nel caso la condizione sia verificata (decina == 0)
                            }
                            else//istruzioni da eseguire se la condizione non è verificata
                            {
                                x += j * 3 - 1;//calcolo della x in base alla decina  del numero
                            }
                            y += k * 2;//calcolo della y in base alla riga presa in considerazione
                            car1t++;//incremento del contatore che segnala la tombola
                            Console.SetCursorPosition(x, y);//impostare la posizione a x e y
                            Console.BackgroundColor = ConsoleColor.Magenta;//impostare il colore dello sfondo a magenta
                            Console.Write(num);//output del numero con sfondo magenta
                            Console.BackgroundColor = ConsoleColor.Black;//impostare il colore dello sfondo a nero
                            if (car1t == 15)//condizione che verifica l'eventuale tombola
                            {
                                Console.SetCursorPosition(0, 20);//impostare la posizione a 0, 20
                                Console.Write("Il giocatore 1 ha fatto tombola");//output del messaggio "Il giocatore 1 ha fatto tombola"
                                Console.SetCursorPosition(1, 1);//impostare la posizione a 1, 1
                                Environment.Exit(1);//chiusura del programma
                            }
                        }
                    }
                }
                return car1t;//ritorna il valore aggiornato del contatore per eventuale tombola
            }
            //}
        }
    }
}
