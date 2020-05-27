using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoPregao
    {
        LeilaoAntesPregao,
        EmAndamento,
        Finalizado
    }

    public class Leilao
    {
        private Interessada _ultimoCliente { get; set; }

        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public EstadoPregao Estado { get; private set; }

        public Lance Ganhador { get; private set; }

		private readonly IModalidadeAvaliacao _modalidade;

		public Leilao(string peca, IModalidadeAvaliacao modalidade)
        {
            Peca = peca;
			_modalidade = modalidade;
			_lances = new List<Lance>();
            Estado = EstadoPregao.LeilaoAntesPregao;
        }

        private bool NovoLanceAceito(Interessada cliente)
        {
            return Estado == EstadoPregao.EmAndamento
                && cliente != _ultimoCliente;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceAceito(cliente))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
			_lances.Skip(1).FirstOrDefault();
        }

        public void IniciaPregao()
        {
            Estado = EstadoPregao.EmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoPregao.EmAndamento)
            {
                throw new InvalidOperationException("Não é possível terminar o pregão " +
				"sem que ele tenha começado. Para isso utilize o método IniciaPregao.");
            }

			Ganhador = _modalidade.Avaliar(this);
            Estado = EstadoPregao.Finalizado;
        }
    }
}
