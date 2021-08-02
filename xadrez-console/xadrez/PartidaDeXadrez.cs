using System;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            ColocarPecas();
        }


        private void MudarJogador()
        {
            _ = jogadorAtual == Cor.Branca ? jogadorAtual = Cor.Preta : jogadorAtual = Cor.Branca;
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
        }
        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            turno++;
            MudarJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao origem)
        {
            if (Tab.Peca(origem) == null)
            {
                throw new TabuleiroException("Não há peças na posição indicada.");
            }
            if (jogadorAtual != Tab.Peca(origem).Cor)
            {
                throw new TabuleiroException("A peça indicada pertence ao adversário.");
            }
            if (!Tab.Peca(origem).ExisteMovimentoPossivel())
            {
                throw new TabuleiroException("A peça escolhida não possui movimentos possíveis.");
            }
        }
        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida.");
            }
        }
        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('h', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('a', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('a', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('h', 1).ToPosicao());

            Tab.ColocarPeca(new Rei(Tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
        }
    }
}
