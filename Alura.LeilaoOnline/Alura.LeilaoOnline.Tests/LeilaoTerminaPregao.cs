﻿using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
		[Theory]
		[InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
		public void RenornaValorSuperiorMaisProximoDadoEsteCenario(
					double valorDestino,
					double valorEsperado,
					double[] ofertas)
		{
			//Arrange
			IModalidadeAvaliacao modalidade =
						new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van gogh", modalidade);
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

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

		}

        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(
                    double valorEsperado, 
                    double[] ofertas)
        {
			//Arrange
			IModalidadeAvaliacao modalidade =
						new MaiorValor();
            var leilao = new Leilao("Van gogh", modalidade);
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

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    
        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
			//Arrange
			IModalidadeAvaliacao modalidade
						= new MaiorValor();
            var leilao = new Leilao("Van gogh", modalidade);
			leilao.IniciaPregao();

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }


        [Fact]
        public void LancaInvalidOperationExceptionCasoTerminoPregaoNaoIniciado()
        {
			//Arrange
			IModalidadeAvaliacao modalidade
						= new MaiorValor();
			var leilao = new Leilao("Van gogh", modalidade);

			//Asset
			var msgObtida = Assert.Throws<InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao());

			var msgEsperada = "Não é possível terminar o pregão " +
				"sem que ele tenha começado. Para isso utilize o método IniciaPregao.";
			Assert.Equal(msgEsperada, msgObtida.Message);
        }
    }
}
