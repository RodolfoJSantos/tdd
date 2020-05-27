using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
	public class LanceCtor
	{
		[Fact]
		public void LancaAargumentExceptionComLanceNegativo()
		{
			//Arrange
			var valorNegativo = -100;

			//Asset
			Assert.Throws<ArgumentException>(
				//Act
				() => new Lance(null, -100)
			);
		}
	}
}
