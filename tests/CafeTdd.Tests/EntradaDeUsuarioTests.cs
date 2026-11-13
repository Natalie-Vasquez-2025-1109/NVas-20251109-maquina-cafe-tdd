using CafeTdd;
using Xunit;

namespace CafeTdd.Tests;

public class EntradaDeUsuarioTests
{
    private static MaquinaDeCafe CrearMaquina()
    {
        var maquina = new MaquinaDeCafe();
        maquina.SetCafetera(new Cafetera(50));
        maquina.SetVasosPequeno(new Vaso(5, 3));
        maquina.SetVasosMediano(new Vaso(5, 5));
        maquina.SetVasosGrande(new Vaso(5, 7));
        maquina.SetAzucarero(new Azucarero(20));
        return maquina;
    }

    [Theory]
    [InlineData("GRANDE", 7)]
    [InlineData("  Mediano  ", 5)]
    [InlineData("PEQUENO", 3)]
    public void GetTipoDeVaso_IgnoraMayusculasYEspacios(string tipo, int contenido)
    {
        var maquina = CrearMaquina();
        Assert.Equal(contenido, maquina.GetTipoDeVaso(tipo)!.GetContenido());
    }

    [Fact]
    public void GetVasoDeCafe_AzucarNegativaLanzaExcepcion()
    {
        var maquina = CrearMaquina();
        var vaso = maquina.GetTipoDeVaso("pequeno")!;
        Assert.Throws<ArgumentOutOfRangeException>(() => maquina.GetVasoDeCafe(vaso, 3, -1));
    }

    [Fact]
    public void GetVasoDeCafe_VasoGrandeDescuentaSieteOnzas()
    {
        var maquina = CrearMaquina();
        var vaso = maquina.GetTipoDeVaso("grande")!;
        Assert.Equal("Felicitaciones", maquina.GetVasoDeCafe(vaso, vaso.GetContenido(), 2));
        Assert.Equal(43, maquina.GetCafetera().GetCantidadCafe());
        Assert.Equal(4, vaso.GetCantidadVasos());
        Assert.Equal(18, maquina.GetAzucarero().GetCantidadAzucar());
    }

    [Fact]
    public void GetVasoDeCafe_TrasAgotarLosVasosInformaQueNoHayVasos()
    {
        var maquina = CrearMaquina();
        var vaso = maquina.GetTipoDeVaso("pequeno")!;
        for (var i = 0; i < 5; i++)
        {
            Assert.Equal("Felicitaciones", maquina.GetVasoDeCafe(vaso, vaso.GetContenido(), 1));
        }
        Assert.Equal("No hay Vasos", maquina.GetVasoDeCafe(vaso, vaso.GetContenido(), 1));
        Assert.Equal(35, maquina.GetCafetera().GetCantidadCafe());
        Assert.Equal(15, maquina.GetAzucarero().GetCantidadAzucar());
    }
}
