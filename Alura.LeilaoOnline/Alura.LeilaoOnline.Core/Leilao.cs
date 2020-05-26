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

        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoPregao.LeilaoAntesPregao;
        }

        private bool NovoLanceAceito(Interessada cliente, double valor)
        {
            return Estado == EstadoPregao.EmAndamento
                && cliente != _ultimoCliente;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoPregao.EmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoPregao.EmAndamento)
            {
                throw new InvalidOperationException();
            }

            Ganhador = _lances
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderBy(l => l.Valor)
                .LastOrDefault();

            Estado = EstadoPregao.Finalizado;
        }
    }
}
