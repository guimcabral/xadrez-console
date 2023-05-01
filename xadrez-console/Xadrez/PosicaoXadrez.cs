using TabuleiroNM;

namespace Xadrez
{
    class PosicaoXadrez
    {
        public char Column { get; set; }
        public int Line { get; set; }

        public PosicaoXadrez(char column, int line)
        {
            Column = column;
            Line = line;
        }

        public BoardPosition ToPosition() => new(8-Line, Column - 'a');

        public override string ToString() => $"{Column}{Line}";
    }
}
