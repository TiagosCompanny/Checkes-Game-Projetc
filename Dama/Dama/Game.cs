using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dama
{
    public class Game : Table
    {
        public static string pieceSelected { get; set; }
        public static string promotedPiece { get; set; }
        public static byte line { get; set; }
        public static byte column { get; set; }


        static void selectPiecePosition()
        {
            string? selectedPosition; //Length
            string firstChar;
            string lastChar;
            string[] validChars1 = { "A", "B", "C", "D", "E", "F", "G", "H" };
            string[] validChars2 = { "1", "2", "3", "4", "5", "6", "7", "8" };

            //---------------------------------- Taking the position as a string ----------------------------------         
            do
            {
                Console.Write("Digite a posição da peça que você quer mover: ");
                selectedPosition = Console.ReadLine();

                selectedPosition = selectedPosition.ToUpper();

                if (selectedPosition == "")
                {
                    Console.WriteLine("Posição invalida, tente novamente!");
                    Game.selectPiecePosition();
                }
                try
                {
                    firstChar = selectedPosition.Substring(0, 1);//Stract the fist char
                    lastChar = selectedPosition[selectedPosition.Length - 1].ToString();//Stract the last char
                }
                catch (Exception)
                {
                    continue;
                }


            }
            //Validations
            while (selectedPosition.Length != 2 || !validChars1.Contains(selectedPosition.Substring(0, 1)) ||
            selectedPosition[0] == 'a' || !validChars2.Contains(selectedPosition[selectedPosition.Length - 1].ToString()));

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
            string adversaryPiece;
            string adversaryKingPice;

            selectPiecePosition();

            if (Table.table[line, column] == " ")
            {
                Console.WriteLine("Não há peça nessa posição, tente novamente");
                selectPiecePosition();
            }

            Console.WriteLine("Move:    <-    OU    ->");

            //verificação de peça e movimentação
            if (Table.table[line, column] == "0" || Table.table[line, column] == "O")
            {
                switch (Table.table[line, column])
                {
                    case "0":
                        if (VerificacaoVezJogadorRed())
                        {
                            switchPiece = 1;
                            pieceSelected = "0";
                            adversaryPiece = "O";
                            adversaryKingPice = "#";
                            promotedPiece = "@";
                            MoveSimplePiece(switchPiece, adversaryPiece, adversaryKingPice);
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Não é a vez deste jogador, Tente novamente selecionado o Jogador Correto: " + Player);
                            Console.ResetColor();
                        }
                        break;

                    case "O":
                        if (!VerificacaoVezJogadorRed())
                        {
                            switchPiece = -1;
                            pieceSelected = "O";
                            adversaryPiece = "0";
                            adversaryKingPice = "@";
                            promotedPiece = "#";
                            MoveSimplePiece(switchPiece, adversaryPiece, adversaryKingPice);
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Não é a vez deste jogador, Tente novamente selecionado o Jogador Correto: " + Player);
                            Console.ResetColor();
                        }
                        break;
                }
            }
            else
            {
                switch (Table.table[line, column])
                {
                    case "@":
                        if (VerificacaoVezJogadorRed())
                        {
                            switchPiece = 1;
                            pieceSelected = "@";
                            adversaryPiece = "O";
                            adversaryKingPice = "#";
                            promotedPiece = "@";
                            MoveKingPiece(adversaryPiece, pieceSelected, adversaryKingPice, "0");
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Não é a vez deste jogador, Tente novamente selecionado o Jogador Correto: " + Player);
                            Console.ResetColor();
                        }
                        break;
                    case "#":
                        if (!VerificacaoVezJogadorRed())
                        {
                            switchPiece = -1;
                            pieceSelected = "#";
                            adversaryPiece = "0";
                            adversaryKingPice = "@";
                            promotedPiece = "#";
                            MoveKingPiece(adversaryPiece, pieceSelected, adversaryKingPice, "O");
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Não é a vez deste jogador, Tente novamente selecionado o Jogador Correto: " + Player);
                            Console.ResetColor();
                        }
                        break;
                }
            }
        }

        private static void MoveSimplePiece(sbyte switchPiece, string adversaryPiece, string adversaryKingPiece)
        {
            bool incrementarContadorJogada = true;
            int directionValue = 0;
            string directionText = "";

            //--Validação de direção correta--
            bool arrowSelectedIsInvalid = false;
            do
            {
                ConsoleKey keyboardKey = Console.ReadKey(true).Key;

                switch (keyboardKey)
                {
                    case ConsoleKey.LeftArrow: directionValue = -1; directionText = "Esquerda"; arrowSelectedIsInvalid = false; break;
                    case ConsoleKey.RightArrow: directionValue = +1; directionText = "Direita"; arrowSelectedIsInvalid = false; break;
                    case ConsoleKey.Escape: System.Environment.Exit(0); break;
                    default: Console.WriteLine("Direção Invalida, tente novamente! ( '<-'    OR   '->')"); arrowSelectedIsInvalid = true; break;
                }
            } while (arrowSelectedIsInvalid);

            Console.WriteLine(directionText); Table.table[line, column] = " ";

            //Validação se existe peça aliada para posição desejada
            try
            {
                if (Table.table[line - 1 * switchPiece, column + (directionValue)] == pieceSelected)
                {
                    Table.table[line, column] = pieceSelected;
                    Console.WriteLine("Movimento invalido, tente novamente");
                    Move();
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Table.table[line, column] = pieceSelected;
                Console.WriteLine("Movimento inválido: Você está tentando acessar uma posição fora dos limites da matriz.\nVerifique os índices e tente novamente.");
                Move();
                return;
            }

            //Verificação de captura retroativa
            if (VerificaoCapturaRetroativa(switchPiece, directionValue, adversaryPiece, adversaryKingPiece))
            {
                incrementarContadorJogada = Captura_RetornandoNecessidadeDeIncrementarContador(switchPiece, pieceSelected, directionText, adversaryPiece, adversaryKingPiece, true);
            }
            //Validaçao se é caso de captura 
            else if ((Table.table[line - (1 * switchPiece), column + (directionValue)] == adversaryPiece || Table.table[line - (1 * switchPiece), column + (directionValue)] == adversaryKingPiece) &&
                Table.table[line - (2 * switchPiece), column + (directionValue * 2)] == " ")
            {
                incrementarContadorJogada = Captura_RetornandoNecessidadeDeIncrementarContador(switchPiece, pieceSelected, directionText, adversaryPiece, adversaryKingPiece, false);
            }
            //Movimentação Comum
            else if (Table.table[line - 1 * switchPiece, column + (directionValue)] == " ")
            {
                Table.table[line - 1 * switchPiece, column + (directionValue)] = pieceSelected;
            }
            else
            {
                Table.table[line, column] = pieceSelected;
                Console.WriteLine("Movimento inválido: Tente Novamente");
                Move();
                return;
            }
            //Verificação se é caso de promoção apenas se não houver mais capturas possíveis
            if (incrementarContadorJogada)
                VerificarEPromoverPecas(pieceSelected);

            RefreshTable(incrementarContadorJogada, true);
        }
        public static void MoveKingPiece(string adversaryPiece, string currentPiece, string adversaryKingPiece, string alliedpiece) //Teste
        {
            bool incrementarContadorJogada = true;
            int directionValue_X = 0;
            int directionValue_Y = 0;
            string directionText_X = "";
            string directionText_Y = "";

            //--Seleção direção correta eixo X--
            bool arrowSelected_X_IsInvalid = false;

            do
            {
                ConsoleKey keyboardKey = Console.ReadKey(true).Key;

                switch (keyboardKey)
                {
                    case ConsoleKey.LeftArrow: directionValue_X = -1; directionText_X = "Esquerda"; arrowSelected_X_IsInvalid = false; break;
                    case ConsoleKey.RightArrow: directionValue_X = +1; directionText_X = "Direita"; arrowSelected_X_IsInvalid = false; break;
                    case ConsoleKey.Escape: Environment.Exit(0); break;
                    default: Console.WriteLine("Direção Invalida, tente novamente! ( '<-'    OR   '->')"); arrowSelected_X_IsInvalid = true; break;
                }
            } while (arrowSelected_X_IsInvalid);

            Console.WriteLine(directionText_X);
            //Seleção Quantidade de casa eixo Y
            bool arrowSelected_Y_IsInvalid = false;

            Console.WriteLine("Move:    ↑    OU    ↓");

            do
            {
                ConsoleKey keyboardKey = Console.ReadKey(true).Key;

                switch (keyboardKey)
                {
                    case ConsoleKey.UpArrow: directionValue_Y = -1; directionText_Y = "Cima"; arrowSelected_Y_IsInvalid = false; break;
                    case ConsoleKey.DownArrow: directionValue_Y = +1; directionText_Y = "Baixo"; arrowSelected_Y_IsInvalid = false; break;
                    case ConsoleKey.Escape: System.Environment.Exit(0); break;
                    default: Console.WriteLine("Direção Invalida, tente novamente! ( '↑'    OR   '↓')"); arrowSelected_Y_IsInvalid = true; break;
                }
                //if ((Table.table[line +(- 1 * directionValue_Y), column + (directionValue_X)] == pieceSelected))
                //{
                //    Console.WriteLine("Direção Invalida, tente novamente! ( '↑'    OR   '↓')");
                //    arrowSelected_Y_IsInvalid = true;
                //}

            } while (arrowSelected_Y_IsInvalid);

            Console.WriteLine(directionText_Y + "\n");
            Console.WriteLine("Quantas casas deseja mover a dama ?");

            byte squareToWalk;
            while (!byte.TryParse(Console.ReadLine(), out squareToWalk))
            {
                Console.WriteLine("Por favor, insira um número válido: ");
            }
            //Movimentação e validação 

            byte incremento_Y = 1;
            byte incremento_X = 1;

            //Verificação se existe peça aliada no caminhon percorrido
            for (int i = 0; i < squareToWalk - 1; i++)
            {
                try
                {
                    if (Table.table[line + (incremento_Y * directionValue_Y), column + (incremento_X * directionValue_X)] == currentPiece ||
                   Table.table[line + (incremento_Y * directionValue_Y), column + (incremento_X * directionValue_X)] == alliedpiece)
                    {
                        Console.WriteLine("Movimento invalido: Existe uma peça aliada nesse percurso, tente novamente!");
                        return;
                    }
                    incremento_Y++;
                    incremento_X++;
                }
                catch
                {
                    Console.WriteLine("Movimento inválido: Você está tentando acessar uma posição fora dos limites da matriz.\nVerifique os índices e tente novamente.");
                    return;
                }

            }

            //Validar se a cada para qual a dama vai está vazia
            try
            {
                if (Table.table[line + (incremento_Y * directionValue_Y), column + (incremento_X * directionValue_X)] != " ")
                {
                    Console.WriteLine("Movimento inválido: A casa para onde você tentou ir já está ocupada, tente novamente!");
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Movimento inválido: Você está tentando acessar uma posição fora dos limites da matriz.\nVerifique os índices e tente novamente.");
                return;
            }

            //Movimentação e capturas
            incremento_Y = 1;
            incremento_X = 1;

            for (int i = 0; i < squareToWalk - 1; i++)
            {
                Table.table[line + (incremento_Y * directionValue_Y), column + (incremento_X * directionValue_X)] = " ";
                incremento_Y++;
                incremento_X++;
            }
            //Move a peça(retira da posição que estava e coloca na solicitada)
            Table.table[line, column] = " ";
            Table.table[line + (incremento_Y * directionValue_Y), column + (incremento_X * directionValue_X)] = currentPiece;

            int currentLine = line + (incremento_Y * directionValue_Y);
            int currentColumn = column + (incremento_X * directionValue_X);
            string mensagem = "";
            var verificacaoSeExisteMaisPeçasQuePossamSerCapturadas_KING_Result = VerificacaoSeExisteMaisPeçasQuePossamSerCapturadas_KING(currentPiece, alliedpiece, adversaryPiece, adversaryKingPiece, currentLine, currentColumn);
            if (verificacaoSeExisteMaisPeçasQuePossamSerCapturadas_KING_Result.Item1)
            {
                //Mapeando a peça e ionformando o player que existe mais possibilidade de captura
                string linhaAtual_Texto = "";
                string colunaAtual_Texto = "";
                switch (currentColumn)
                {
                    case 0: linhaAtual_Texto = "A"; break;
                    case 1: linhaAtual_Texto = "B"; break;
                    case 2: linhaAtual_Texto = "C"; break;
                    case 3: linhaAtual_Texto = "D"; break;
                    case 4: linhaAtual_Texto = "E"; break;
                    case 5: linhaAtual_Texto = "F"; break;
                    case 6: linhaAtual_Texto = "G"; break;
                    case 7: linhaAtual_Texto = "H"; break;
                }
                switch (currentLine)
                {
                    case 0: colunaAtual_Texto = "8"; break;
                    case 1: colunaAtual_Texto = "7"; break;
                    case 2: colunaAtual_Texto = "6"; break;
                    case 3: colunaAtual_Texto = "5"; break;
                    case 4: colunaAtual_Texto = "4"; break;
                    case 5: colunaAtual_Texto = "3"; break;
                    case 6: colunaAtual_Texto = "2"; break;
                    case 7: colunaAtual_Texto = "1"; break;
                }
                mensagem = "Existe mais uma captura possível, mova a peça de: " + linhaAtual_Texto + colunaAtual_Texto + " para a direção: " + verificacaoSeExisteMaisPeçasQuePossamSerCapturadas_KING_Result.Item2;
                RefreshTable(false, false, mensagem);
                incrementarContadorJogada = false;
            }

            VerificarEPromoverPecas(pieceSelected);
            RefreshTable(incrementarContadorJogada, true, mensagem);
        }

        private static bool VerificacaoVezJogadorRed()
        {
            return Player == "Red";
        }
        private static bool VerificaoCapturaRetroativa(sbyte switchPiece, int directionValue, string adversaryPiece, string adversaryKingPiece)
        {
            try
            {
                if ((Table.table[line + switchPiece, column + (directionValue)] == adversaryPiece || Table.table[line + switchPiece, column + (directionValue)] == adversaryKingPiece) &&
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

        private static bool Captura_RetornandoNecessidadeDeIncrementarContador(sbyte switchPiece, string pieceSelected, string direction, string adversaryPiece, string adversaryKingPiece, bool capturaRetroativa)
        {

            sbyte directionValue = 0;


            //Declaration of Direction
            switch (direction)
            {
                case "Esquerda": directionValue = -1; break;
                case "Direita": directionValue = 1; break;
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
            if (VerificacaoSeExisteMaisPeçasQuePossamSerCapturadas(switchPiece, adversaryPiece, adversaryKingPiece, directionValue, capturaRetroativa))
            {
                RefreshTable(false, false);
                return false;
            }
            return true;
        }

        private static bool VerificacaoSeExisteMaisPeçasQuePossamSerCapturadas(sbyte switchPiece, string adversaryPiece, string adversaryKingPiece, sbyte directionValue, bool capturaRetroativa)
        {

            if (capturaRetroativa)
            {
                switchPiece *= -1;
            }

            int[] currentPosition = new int[2];
            currentPosition[0] = line + (-2 * switchPiece);
            currentPosition[1] = column + (2 * directionValue);

            try // ↙ 
            {
                if (Table.table[currentPosition[0] - (-1), currentPosition[1] - (+1)] == adversaryPiece && Table.table[currentPosition[0] - (2 * -1), currentPosition[1] + (-1 * 2)] == " ")
                    return true;

            }
            catch (IndexOutOfRangeException)
            { }
            try // ↘
            {
                if (Table.table[currentPosition[0] - (-1), currentPosition[1] - (-1)] == adversaryPiece && Table.table[currentPosition[0] - (2 * -1), currentPosition[1] + (+1 * 2)] == " ")
                    return true;

            }

            catch (IndexOutOfRangeException)
            { }
            try // ↖
            {
                if (Table.table[currentPosition[0] - (+1), currentPosition[1] - (+1)] == adversaryPiece && Table.table[currentPosition[0] - (2 * +1), currentPosition[1] + (-1 * 2)] == " ")
                    return true;

            }
            catch (IndexOutOfRangeException)
            { }
            try// ↗
            {
                if (Table.table[currentPosition[0] - (+1), currentPosition[1] - (-1)] == adversaryPiece && Table.table[currentPosition[0] - (2 * +1), currentPosition[1] + (1 * 2)] == " ")
                    return true;
            }
            catch (IndexOutOfRangeException)
            { }

            return false;

        }

        private static Tuple<bool, string> VerificacaoSeExisteMaisPeçasQuePossamSerCapturadas_KING(string currentPieceKing, string alliedpiece, string adversaryPiece, string adversaryKingPiece, int currentLine, int currentColumn)
        {
            //↗
            try
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (((Table.table[currentLine - i, currentColumn + i] == adversaryPiece) || (Table.table[currentLine - i, currentColumn + i] == adversaryKingPiece)) && //Proxima casa da direção contém preça adversária E
                        (Table.table[currentLine - (i + 1), currentColumn + (i + 1)] == " ")) //Um espeço vazio na casa posterior
                    {
                        return new Tuple<bool, string>(true, "sentido nordeste '/'"); ;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            { }
            //↖
            try
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (((Table.table[currentLine - i, currentColumn - i] == adversaryPiece) || (Table.table[currentLine - i, currentColumn - i] == adversaryKingPiece)) &&
                        (Table.table[currentLine - (i + 1), currentColumn - (i + 1)] == " "))
                    {
                        return new Tuple<bool, string>(true, "sentido noroeste '\\'");
                    }
                }
            }
            catch (IndexOutOfRangeException)
            { }
            //↙
            try
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (((Table.table[currentLine + i, currentColumn - i] == adversaryPiece) || (Table.table[currentLine + i, currentColumn - i] == adversaryKingPiece)) &&
                        (Table.table[currentLine + (i + 1), currentColumn - (i + 1)] == " "))
                    {
                        return new Tuple<bool, string>(true, "sentido sudoeste '/'");
                    }
                }
            }
            catch (IndexOutOfRangeException)
            { }
            //↘
            try
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (((Table.table[currentLine + i, currentColumn + i] == adversaryPiece) || (Table.table[currentLine + i, currentColumn + i] == adversaryKingPiece)) &&
                        (Table.table[currentLine + (i + 1), currentColumn + (i + 1)] == " "))
                    {
                        return new Tuple<bool, string>(true, "sentido sudeste '\\'");
                    }
                }
            }
            catch (IndexOutOfRangeException)
            { }

            return new Tuple<bool, string>(false, " ");
        }

        private static void VerificarEPromoverPecas(string pieceSelected)
        {
            bool prototionSound = false;
            sbyte linhaTabuleiroVerificada;
            if (pieceSelected == "0")
                linhaTabuleiroVerificada = 0;
            else
                linhaTabuleiroVerificada = 7;

            //Percorrer o tabuleiro e verificar se não há casas de promoção
            for (byte linha = 0; linha <= 7; linha++)
            {
                if (Table.table[linhaTabuleiroVerificada, linha] == pieceSelected)
                {
                    Table.table[linhaTabuleiroVerificada, linha] = promotedPiece;
                    prototionSound = true;
                }
            }
            if (prototionSound)
            {
                int tempoDelay = 300;
                for (int i = 1; i <= 3; i++)
                {
                    Console.Beep(170 * i, tempoDelay);
                    Thread.Sleep(i * 50);
                    tempoDelay += 75;
                }
            }
        }

    }

}
