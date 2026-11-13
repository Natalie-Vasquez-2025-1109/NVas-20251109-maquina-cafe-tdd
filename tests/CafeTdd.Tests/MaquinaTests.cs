using CafeTdd;
using Xunit;

namespace CafeTdd.Tests;

public class VasoTests
{
    [Fact]
    public void HayVasos_DaVerdaderoCuandoHayCantidadSolicitada()
    {
        var vasos = new Vaso(2, 3);
        Assert.True(vasos.HayVasos(1));
        Assert.True(vasos.HayVasos(2));
    }

    [Fact]
    public void HayVasos_DaFalsoCuandoNoAlcanza()
    {
        Assert.False(new Vaso(1, 3).HayVasos(2));
    }

    [Fact]
    public void DarVasos_RestaLaCantidadEntregada()
    {
        var vasos = new Vaso(5, 3);
        vasos.DarVasos(1);
        Assert.Equal(4, vasos.GetCantidadVasos());
    }
}

public class CafeteraTests
{
    [Fact]
    public void HayCafe_ValidaCantidadSuficiente()
    {
        var cafetera = new Cafetera(10);
        Assert.True(cafetera.HayCafe(5));
        Assert.True(cafetera.HayCafe(10));
        Assert.False(cafetera.HayCafe(11));
    }

    [Fact]
    public void DarCafe_RestaLaCantidadServida()
    {
        var cafetera = new Cafetera(10);
        cafetera.DarCafe(7);
        Assert.Equal(3, cafetera.GetCantidadCafe());
    }
}

public class AzucareroTests
{
    [Fact]
    public void HayAzucar_ValidaCantidadSuficiente()
    {
        var azucarero = new Azucarero(10);
        Assert.True(azucarero.HayAzucar(5));
        Assert.True(azucarero.HayAzucar(10));
        Assert.False(azucarero.HayAzucar(15));
    }

    [Fact]
    public void DarAzucar_RestaLaCantidadServida()
    {
        var azucarero = new Azucarero(10);
        azucarero.DarAzucar(5);
        azucarero.DarAzucar(2);
        Assert.Equal(3, azucarero.GetCantidadAzucar());
    }
}

public class MaquinaDeCafeTests
{
    private static MaquinaDeCafe CrearMaquina(int vasos = 5, int cafe = 50, int azucar = 20)
    {
        var maquina = new MaquinaDeCafe();
        maquina.SetCafetera(new Cafetera(cafe));
        maquina.SetVasosPequeno(new Vaso(vasos, 3));
        maquina.SetVasosMediano(new Vaso(vasos, 5));
        maquina.SetVasosGrande(new Vaso(vasos, 7));
        maquina.SetAzucarero(new Azucarero(azucar));
        return maquina;
    }

    [Theory]
    [InlineData("pequeno", 3)]
    [InlineData("mediano", 5)]
    [InlineData("grande", 7)]
    public void GetTipoDeVaso_DevuelveElVasoConfigurado(string tipo, int contenido)
    {
        var maquina = CrearMaquina();
        Assert.Equal(contenido, maquina.GetTipoDeVaso(tipo)!.GetContenido());
    }

    [Fact]
    public void GetTipoDeVaso_TipoDesconocidoDevuelveNulo() =>
        Assert.Null(CrearMaquina().GetTipoDeVaso("gigante"));

    [Fact]
    public void GetVasoDeCafe_SinVasosDevuelveMensajeYNoConsumeInsumos()
    {
        var maquina = CrearMaquina(vasos: 0);
        Assert.Equal("No hay Vasos", maquina.GetVasoDeCafe(maquina.GetTipoDeVaso("pequeno")!, 10, 2));
        Assert.Equal(50, maquina.GetCafetera().GetCantidadCafe());
        Assert.Equal(20, maquina.GetAzucarero().GetCantidadAzucar());
    }

    [Fact]
    public void GetVasoDeCafe_SinCafeDevuelveMensajeYNoConsumeVasoNiAzucar()
    {
        var maquina = CrearMaquina(cafe: 0);
        var vaso = maquina.GetTipoDeVaso("pequeno")!;
        Assert.Equal("No hay Cafe", maquina.GetVasoDeCafe(vaso, 1, 2));
        Assert.Equal(0, maquina.GetCafetera().GetCantidadCafe());
        Assert.Equal(5, vaso.GetCantidadVasos());
        Assert.Equal(20, maquina.GetAzucarero().GetCantidadAzucar());
    }

    [Fact]
    public void GetVasoDeCafe_SinAzucarDevuelveMensajeYNoConsumeNada()
    {
        var maquina = CrearMaquina(azucar: 1);
        var vaso = maquina.GetTipoDeVaso("pequeno")!;
        Assert.Equal("No hay Azucar", maquina.GetVasoDeCafe(vaso, 1, 3));
        Assert.Equal(5, vaso.GetCantidadVasos());
        Assert.Equal(50, maquina.GetCafetera().GetCantidadCafe());
        Assert.Equal(1, maquina.GetAzucarero().GetCantidadAzucar());
    }

    [Fact]
    public void GetVasoDeCafe_ConInsumosSuficientesSirveYDescuentaTodo()
    {
        var maquina = CrearMaquina();
        var vaso = maquina.GetTipoDeVaso("pequeno")!;
        Assert.Equal("Felicitaciones", maquina.GetVasoDeCafe(vaso, 10, 3));
        Assert.Equal(40, maquina.GetCafetera().GetCantidadCafe());
        Assert.Equal(4, vaso.GetCantidadVasos());
        Assert.Equal(17, maquina.GetAzucarero().GetCantidadAzucar());
    }

    [Fact]
    public void GetVasoDeCafe_SinAzucarSolicitadaNoDescuentaAzucar()
    {
        var maquina = CrearMaquina();
        var vaso = maquina.GetTipoDeVaso("pequeno")!;
        Assert.Equal("Felicitaciones", maquina.GetVasoDeCafe(vaso, 10, 0));
        Assert.Equal(20, maquina.GetAzucarero().GetCantidadAzucar());
    }
}

