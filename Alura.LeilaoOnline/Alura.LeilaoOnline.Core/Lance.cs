using System;

namespace Alura.LeilaoOnline.Core
{
	public class Lance
    {
        public Interessada Cliente { get; }
        public double Valor { get; }

        public Lance(Interessada cliente, double valor)
        {
			if (valor < 0)
			{
				throw new ArgumentException("O valor do lance não pode ser negativo.",
					nameof(valor));
			}
            Cliente = cliente;
            Valor = valor;
        }
    }
}
