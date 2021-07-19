using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    class TabuleiroException : Exception
    {
        public TabuleiroException(string message) : base(message)
        {
        }
    }
}
