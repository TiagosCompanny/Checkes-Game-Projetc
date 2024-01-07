using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dama
{
    public class Screens
    {
        public bool gameRunning { get; set; }
        public void IrParaMenu()
        {
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("  ___________________________________________________________________________________________________________");
            Console.WriteLine(" |                                                                                                          |");
            Console.WriteLine(" | ░█████╗░██╗░░██╗███████╗░█████╗░██╗░░██╗███████╗██████╗░░██████╗    ░██████╗░░█████╗░███╗░░░███╗███████╗ |");
            Console.WriteLine(" | ██╔══██╗██║░░██║██╔════╝██╔══██╗██║░██╔╝██╔════╝██╔══██╗██╔════╝    ██╔════╝░██╔══██╗████╗░████║██╔════╝ |");
            Console.WriteLine(" | ██║░░╚═╝███████║█████╗░░██║░░╚═╝█████═╝░█████╗░░██████╔╝╚█████╗░    ██║░░██╗░███████║██╔████╔██║█████╗░░ |");
            Console.WriteLine(" | ██║░░██╗██╔══██║██╔══╝░░██║░░██╗██╔═██╗░██╔══╝░░██╔══██╗░╚═══██╗    ██║░░╚██╗██╔══██║██║╚██╔╝██║██╔══╝░░ |");
            Console.WriteLine(" | ╚█████╔╝██║░░██║███████╗╚█████╔╝██║░╚██╗███████╗██║░░██║██████╔╝    ╚██████╔╝██║░░██║██║░╚═╝░██║███████╗ |");
            Console.WriteLine(" | ░╚════╝░╚═╝░░╚═╝╚══════╝░╚════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚═╝╚═════╝░    ░╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝ |");
            Console.WriteLine(" |                                                                                                          |");
            Console.WriteLine("  ___________________________________________________________________________________________________________");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Precione uma das teclas correspondentes as opções abaixo:\n\n");
            Console.WriteLine("|1|  {  JOGAR  }\n");
            Console.WriteLine("|2|  {  REGRAS }\n");
            Console.WriteLine("|3|  {  SOBRE  } \n");
            Console.WriteLine("|4|  {  SAIR   } \n");

            ConsoleKey keyboardKey = Console.ReadKey(true).Key;

            switch (keyboardKey)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1: ProcessarJogo(); break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2: irParaRegras(); break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3: irParaSobre(); break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    Sair(); break;
                default:
                    IrParaMenu(); break;
            }
        }

        private void irParaRegras()
        {
            Console.Clear();
            Console.Clear();
            DesenharCabecalhoXadrez(10, ConsoleColor.Cyan, ConsoleColor.Red);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" REGRAS DO JOGO ");
            Console.ResetColor();
            DesenharCabecalhoXadrez(10, ConsoleColor.Cyan, ConsoleColor.Red);
            Console.ResetColor();

            Console.WriteLine("\n\n1. Tabuleiro:\r\nO tabuleiro é composto por 64 casas, com as peças posicionadas nas três linhas mais próximas a cada jogador.\r" +
                "\nCada jogador começa com 12 peças (geralmente discos) de uma cor.\r\n\n2. Movimento das Peças:\r\nAs peças movem-se diagonalmente para frente.\r\n" +
                "Peças comuns movem-se uma casa por vez.\r\nQuando uma peça atinge a última linha do oponente, ela é promovida a \"dama\".\r\n\n3. Damas:\r\nDamas movem-se" +
                " diagonalmente para frente ou para trás quantas casas desejarem.\r\nDamas podem capturar pulando sobre a peça adversária, e múltiplas capturas podem ser realizadas " +
                "em uma única jogada.\r\n\n4. Captura:\r\nA captura é obrigatória sempre que possível. Se um jogador tem a oportunidade de capturar, deve fazê-lo.\r\nSe um jogador realizar " +
                "uma captura e tiver a oportunidade de fazer outra captura subsequente, deve continuar a capturar.\r\n\n5. Fim do Jogo:\r\nO jogo termina quando um jogador captura todas as peças " +
                "do oponente.\r\nSe o oponente fica impossibilitado de fazer qualquer movimento, o jogador ativo vence.\r\n\n6. Empate:\r\nSe uma mesma posição repetir-se três vezes com os mesmos" +
                " jogadores movendo as mesmas peças, o jogo pode ser declarado como empate.");

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n--Precione qualquer tecla para voltar ao Menu --\n");

            DesenharCabecalhoXadrez(26, ConsoleColor.Cyan, ConsoleColor.Red);

            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();

            IrParaMenu();
        }

        private void irParaSobre()
        {
            Console.Clear();
            DesenharCabecalhoXadrez(10, ConsoleColor.White, ConsoleColor.DarkGray);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" SOBRE O PROJETO ");
            Console.ResetColor();
            DesenharCabecalhoXadrez(10, ConsoleColor.White, ConsoleColor.DarkGray);
            Console.ResetColor();

            Console.WriteLine("\n\nOlá, meu nome é Tiago Henrique, e estou empolgado para compartilhar com vocês o projeto no qual tenho trabalhado recentemente: " +
                "o \"Jogo de Damas\". Este projeto foi uma jornada de alguns meses para aprimorar minhas habilidades de programação e explorar novos horizontes na " +
                "lógica de desenvolvimento de jogos.\rMeu objetivo era criar uma versão do clássico Jogo de Damas no console do Scharp. A ideia por trás do " +
                "projeto era não apenas replicar o jogo, mas também adicionar um capricho pessoal que o tornasse único. \rDurante o desenvolvimento, enfrentei " +
                "diversos desafios, desde a implementação da lógica do jogo até a criação de uma interface amigável para o usuário. A cada desafio superado, pude " +
                "perceber meu crescimento como programador. A otimização do código, a aplicação de boas práticas e a criação de uma experiência de usuário agradável " +
                "foram aspectos fundamentais do processo.\rAgora, apresento com orgulho esta versão avançada do Jogo de Damas!\rEstou entusiasmado para " +
                "compartilhar essa jornada de aprendizado e evolução como programador por meio do \"Jogo de Damas\". Espero que aproveitem o jogo tanto quanto eu " +
                "aproveitei o processo de desenvolvimento.\rMuito obrigado por compartilhar essa experiência comigo!");

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\n--Precione qualquer tecla para voltar ao Menu --\n");
            DesenharCabecalhoXadrez(26, ConsoleColor.White, ConsoleColor.DarkGray);
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            IrParaMenu();
        }
        private void Sair()
        {
            Console.Clear();
            DesenharCabecalhoXadrez(25, ConsoleColor.White, ConsoleColor.DarkYellow);
            Console.ResetColor();
            int tempoDelay = 300;
            Thread.Sleep(tempoDelay);
            Console.WriteLine("\n\n\n                    OBRIGADO POR JOGAR!\n\n                      ATÉ MAIS!\n\n");
            Console.ResetColor();
            Console.WriteLine("\n\n\n\n\n\n");
            DesenharCabecalhoXadrez(25, ConsoleColor.White, ConsoleColor.DarkYellow);
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(1000);
            for (int i = 4; i > 0; i--)
            {
                Console.Beep(250 * i, tempoDelay);
                Thread.Sleep(i * 50);
                tempoDelay += 75;

                for (int j = 0; j <= 3; j++)
                {
                    Thread.Sleep(50);
                    if (j % 2 != 0)
                        DesenharCabecalhoXadrez(25, ConsoleColor.DarkRed, ConsoleColor.DarkMagenta);
                    else
                        DesenharCabecalhoXadrez(25, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed);

                    Console.WriteLine();
                }
            }
            System.Environment.Exit(0);
        }
        private void drawVictory(string winner)
        {
            //Logica para alterar o winer e as cores

            ConsoleColor WinnerCollor;
            if (winner == "Blue")
                WinnerCollor = ConsoleColor.DarkCyan;
            else
                WinnerCollor = ConsoleColor.Red;

            Console.Clear();
            DesenharCabecalhoXadrez(10, ConsoleColor.White, WinnerCollor);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" Jogar "+ winner + " Venceu ");
            Console.ResetColor();
            DesenharCabecalhoXadrez(10, ConsoleColor.White, WinnerCollor);
            Console.ResetColor();

            Console.WriteLine("");

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\n--Precione qualquer tecla para voltar ao Menu --\n");
            DesenharCabecalhoXadrez(26, ConsoleColor.White, WinnerCollor);
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            IrParaMenu();
        }
  
        private void DesenharCabecalhoXadrez(int size, ConsoleColor colorA, ConsoleColor colorB)
        {
            for (int i = 0; i < size; i++)
            {
                if (i % 2 == 0)
                {
                    Console.BackgroundColor = colorA;//ConsoleColor.DarkGray;
                    Console.Write("   ");
                }
                else
                {
                    Console.BackgroundColor = colorB;// ConsoleColor.White;
                    Console.Write("   ");
                }
            }
        }
        private void ProcessarJogo()
        {

            gameRunning = true;
            var tabuleiro = new Table();
            tabuleiro.CreateTable();
            Table.drawTable();

            while (gameRunning)
            {
                Game.Move();
            }
            string winner;

            if (Table.Move % 2 == 0)
                winner = "Blue";
            else
                winner = "Red";
            
            drawVictory(winner);
        }

    }
}
