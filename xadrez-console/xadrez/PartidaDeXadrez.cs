using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
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
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in capturadas)
            {
                if(p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in pecas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p);
                }   
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));

            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca));
            
        }
    }
}
