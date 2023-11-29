using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama
{
    public class Table
    {
        public static string Player { get; set; }
        public static int Move { get; set; }

        public static string[,] table = new string[8, 8];

        public void CreateTable()
        {
            

            for (byte i = 0; i < 8; i++)
            {
                for (byte j = 0; j < 8; j++)
                {
                    if (j % 2 != 0 && (i == 0 || i == 2))
                        table[i, j] = "O";

                    else if (j % 2 == 0 && i == 1)
                        table[i, j] = "O";
                    else if (j % 2 == 0 && (i == 5 || i == 7))
                        table[i, j] = "0";
                    else if (j % 2 != 0 && i == 6)
                        table[i, j] = "0";
                    else
                        table[i, j] = " ";
                }
            }

        }



        public static void drawTable()
        {

            Player = "O"; //is not
            Console.Clear();
            ConsoleColor colorRed = ConsoleColor.Red;
            ConsoleColor colorBlue = ConsoleColor.Cyan;
            ConsoleColor colorYellow = ConsoleColor.DarkYellow;
            ConsoleColor switchColor;

            for (byte i = 0; i < 8; i++)
            {

                for (byte j = 0; j < 8; j++)
                {
                    if (j % 2 == 0 && (i == 0 || i == 2 || i == 4 || i == 6 || i == 8))
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                        if(table[i, j] == "O")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (table[i, j] == "0")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if(table[i, j] == "@")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (table[i, j] == "#")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else
                            Console.Write(table[i, j]);

                        //Console.Write(table[i, j]);
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (j % 2 != 0 && (i == 1 || i == 3 || i == 5 || i == 7))
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                        if (table[i, j] == "O")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (table[i, j] == "0")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (table[i, j] == "@")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (table[i, j] == "#")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else
                            Console.Write(table[i, j]);
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                        if (table[i, j] == "O")
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (table[i, j] == "0")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (table[i, j] == "@")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (table[i, j] == "#")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(table[i, j]);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        else
                            Console.Write(table[i, j]);
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                }
                Console.Write(" " + (i-8)*-1);
                Console.WriteLine();
            }
            Console.WriteLine(" A  B  C  D  E  F  G  H ");
            switch (Move % 2 == 0)
            {
                case true: Player = "Red"; switchColor = ConsoleColor.Red; break;
                case false: Player = "Blue"; switchColor = ConsoleColor.Cyan; break;
            }
            Console.ForegroundColor = switchColor;
            Console.WriteLine("\nPlayer: " + Player);
            Console.ForegroundColor = colorYellow;
            Console.WriteLine("Move: " + (Move + 1));
        }

        public static void RefreshTable()
        {
            Console.Clear();
            Console.Beep();
            Table.drawTable();
            Thread.Sleep(200);
        }

    }
}
