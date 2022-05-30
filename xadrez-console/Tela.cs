using TabuleiroNM;

namespace Xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleito(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) != null)
                        Console.Write(tab.Peca(i, j) + " ");
                    else
                        Console.Write("- ");
                }
                Console.WriteLine();
            }
        }
    }
}
