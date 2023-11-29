
using System.Threading;
using System;

namespace Dama
{
    class Program
    {
        
        static void Main(string[] args)
        {

            var tabuleiro = new Table();
            tabuleiro.CreateTable();
            Table.drawTable();


            while (true)
            {
                Moviment.Move();
                

            }






            //Thread.Sleep(1000);
            //Console.WriteLine("");
            //Console.WriteLine("");

            //Moviment.MovimentRed();
            //Moviment.MovimentRed();


        }


    }
}