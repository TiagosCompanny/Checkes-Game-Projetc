using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dama
{
    public class Moviment : Table
    {
        public static string pieceSelected { get; set; }
        public static byte line { get; set; }
        public static byte column { get; set; }


        static void selectPiecePosition()
        {
            string selectedPosition; //Length
            string firstChar;
            string lastChar;
            string[] validChars1 = { "A", "B", "C", "D", "E", "F", "G", "H" };
            string[] validChars2 = { "1", "2", "3", "4", "5", "6", "7", "8" };

            //---------------------------------- Taking the position as a string ----------------------------------         
            do
            {

                Console.Write("Give the piece position you want to move: ");
                selectedPosition = Console.ReadLine(); //Reading value

                selectedPosition = selectedPosition.ToUpper();

                if (selectedPosition == "")
                {
                    Console.WriteLine("Invalid Posaition, try again");
                    Moviment.selectPiecePosition();
                }

                firstChar = selectedPosition.Substring(0, 1); //Stract the fist char
                lastChar = selectedPosition[selectedPosition.Length - 1].ToString();  //Stract the last char

            }
            //Validations
            while (selectedPosition.Length != 2 || !validChars1.Contains(selectedPosition.Substring(0, 1)) ||
            selectedPosition[0] == 'a' || !validChars2.Contains(selectedPosition[selectedPosition.Length - 1].ToString())
             );

            //---------------------------------------------Traforming selectedPosition to int-------------------------------

            //int column = 0; //initial position x
            switch (selectedPosition[0].ToString())
            {
                case "A": column = 0; break;
                case "B": column = 1; break;
                case "C": column = 2; break;
                case "D": column = 3; break;
                case "E": column = 4; break;
                case "F": column = 5; break;
                case "G": column = 6; break;
                case "H": column = 7; break;
            }
            //int line = 0;//initial position y
            switch (selectedPosition[1].ToString())
            {
                case "1": line = 7; break;
                case "2": line = 6; break;
                case "3": line = 5; break;
                case "4": line = 4; break;
                case "5": line = 3; break;
                case "6": line = 2; break;
                case "7": line = 1; break;
                case "8": line = 0; break;
            }
        }
        //---------------------------------------------Selected the diretion you want to move-------------------------------

        public static void Move()
        {
            sbyte switchPiece; //+1 -> "O"  //-1 -> "0"
            //string pieceSelected;
            string adversaryPiece;
            string direction;


            selectPiecePosition();
            Console.WriteLine("Move:    <-    OR    ->");
            if (Table.table[line, column] == " ")
            {
                Console.WriteLine("Não há peça nessa posição, tente novamente");
                selectPiecePosition();
            }

            //verificação de peça
            if (Table.table[line, column] == "0")
            {
                switchPiece = 1;
                pieceSelected = "0";
                adversaryPiece = "O";
            }
            else
            {
                switchPiece = -1;
                pieceSelected = "O";
                adversaryPiece = "0";
            }

            //Movimentação

            ConsoleKey keyboardKey = Console.ReadKey(true).Key;
            string directionText = "";
            int directionValue = 0;

            switch (keyboardKey)
            {
                case ConsoleKey.LeftArrow: directionValue = -1; directionText = "Esquerda"; break;
                case ConsoleKey.RightArrow: directionValue = +1; directionText = "Direita"; break;
                case ConsoleKey.Escape: System.Environment.Exit(0); break;
            }

            Console.WriteLine(directionText); Table.table[line, column] = " ";

            //Validação se existe peça aliada para posição desejada
            if (Table.table[line - 1 * switchPiece, column + (directionValue)] == pieceSelected)
            {
                Table.table[line, column] = pieceSelected;
                Console.WriteLine("Movimento invalido, tente novamente");
                Move();
            }
            //Verificação de captura retroativa
            else if (VerificaoCapturaRetroativa(switchPiece, directionValue, adversaryPiece, directionValue))
            {
                Captura(switchPiece, pieceSelected, directionText, adversaryPiece, true);
            }


            //Validaçao se é caso de captura 
            else if (Table.table[line - (1 * switchPiece), column + (directionValue)] == adversaryPiece &&
                Table.table[line - (2 * switchPiece), column + (directionValue * 2)] == " ")
            {
                Captura(switchPiece, pieceSelected, directionText, adversaryPiece, false);
            }
            else if (Table.table[line - 1 * switchPiece, column + (directionValue)] == " ")
            {
                Table.table[line - 1 * switchPiece, column + (directionValue)] = pieceSelected;
            }
            Table.Move++;
            RefreshTable();
        }

        private static bool VerificaoCapturaRetroativa(sbyte switchPiece, int directionValue, string adversaryPiece, int direction)
        {
            try
            {
                if (Table.table[line + switchPiece, column + (directionValue)] == adversaryPiece &&
                   Table.table[line + (2 * switchPiece), column + (directionValue * 2)] == " ")
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;

        }


        private static void Captura(sbyte switchPiece, string pieceSelected, string direction, string adversaryPiece, bool capturaRetroativa)
        {
            
            sbyte directionValue = 0;


            //Declaration of Direction
            switch (direction)
            {
                case "Esquerda":
                    directionValue = -1; break;
                case "Direita":
                    directionValue = 1; break;
            }


            if (capturaRetroativa)
            {
                Table.table[line + switchPiece, column + (directionValue)] = " ";
                Table.table[line + (2 * switchPiece), column + (directionValue * 2)] = pieceSelected;
            }
            else //(Captura frontal)
            {
                Table.table[line + (-1 * switchPiece), column + directionValue] = " ";
                Table.table[line + (-2 * switchPiece), column + (2 * directionValue)] = pieceSelected;
            }


            //Verificação se exites mais possibilidades de capturas para repetir a vez do jogador
            if (VerificacaoSeExisteMaisPeçasQuePossamSerCapturadas(switchPiece, adversaryPiece, directionValue, capturaRetroativa))
            {
               //RefreshTable();
               // Move(); //Não é exatamente move

            }

        }

        private static bool VerificacaoSeExisteMaisPeçasQuePossamSerCapturadas(sbyte switchPiece, string adversaryPiece, sbyte directionValue, bool capturaRetroativa)
        {

            if(capturaRetroativa)
            {
                switchPiece *= -1;
            }

            int[] currentPosition = new int[2];
            currentPosition[0] = line + (-2 * switchPiece);
            currentPosition[1] = column+ (2 * directionValue);

            try
            {
                if (Table.table[currentPosition[0] - (1 * -1), currentPosition[1] + (1)] == adversaryPiece &&
                               Table.table[currentPosition[0] - (2 * -1), currentPosition[1] + (1 * 2)] == " ")
                {
                    Table.Move--;
                    return true;
                    //Repetir vez do jogador, pois existe mais possibilidades de captura na mesma jogada
                }
            }
            catch (IndexOutOfRangeException)
            { }
            try
            {
                if (Table.table[currentPosition[0] - (1 * -1), currentPosition[1] + (1)] == adversaryPiece &&
                                Table.table[currentPosition[0] - (2 * -1), currentPosition[1] + (-1 * 2)] == " ")
                {
                    Table.Move--;
                    return true;
                }
            }
            catch (IndexOutOfRangeException)
            {}
            try
            {
                if (Table.table[currentPosition[0] - (1 * +1), currentPosition[1] + (-1)] == adversaryPiece &&
                            Table.table[currentPosition[0] - (2 * +1), currentPosition[1] + (-1 * 2)] == " ")
                {
                    Table.Move--;
                    return true;
                }  
            }
            catch (IndexOutOfRangeException)
            {}
            try
            {
                if (Table.table[currentPosition[0] - (1 * +1), currentPosition[1] + (1)] == adversaryPiece &&
                            Table.table[currentPosition[0] - (2 * +1), currentPosition[1] + (-1 * 2)] == " ")
                {
                    Table.Move--;
                    return true;
                }
            }
            catch (IndexOutOfRangeException)
            { }

            return false;
            
        }


    }

}
