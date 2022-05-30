using System;
using TabuleiroNM;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8,8);

            Tela.ImprimirTabuleito(tab);
        }
    }
}