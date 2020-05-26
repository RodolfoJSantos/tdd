using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(4, new double[] { 600, 850, 900, 1200 })]
        [InlineData(2, new double[] { 600, 850 })]
        public void ClienteNaoPodeRealizarDoisLancesSeguidos(
                    int quantidadeLaces,
                    double[] ofertas)
        {
            //Arrange
            var leilao = new Leilao("Van gogh");
            var maria = new Interessada("Maria", leilao);
            var jose = new Interessada("Jose", leilao);

            leilao.IniciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor  = ofertas[i];
                if ((i % 2) == 0)
                    leilao.RecebeLance(jose, valor);
                else
                    leilao.RecebeLance(maria, valor);
            }
            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(jose, 1500);

            //Assert            
            var valorObtido = leilao.Lances.Count();
            Assert.Equal(quantidadeLaces, valorObtido);
        }

        [Theory]
        [InlineData(2, new double[] { 600, 850 })]
        [InlineData(4, new double[] { 600, 850, 900, 1200 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
                    int quantidadeLaces, 
                    double[] ofertas)
        {
            //Arrange
            var leilao = new Leilao("Van gogh");
            var jose = new Interessada("Jose", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(jose, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }
            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(jose, 1500);

            //Assert
            var valorObtido = leilao.Lances.Count();
            Assert.Equal(quantidadeLaces, valorObtido);
        }
    }
}
