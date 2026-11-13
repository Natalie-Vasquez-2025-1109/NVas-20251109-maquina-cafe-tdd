namespace CafeTdd;

public sealed class Vaso
{
    private int _cantidadVasos;
    public int Contenido { get; }

    public Vaso(int cantidadVasos, int contenido)
    {
        if (cantidadVasos < 0) throw new ArgumentOutOfRangeException(nameof(cantidadVasos));
        if (contenido <= 0) throw new ArgumentOutOfRangeException(nameof(contenido));
        _cantidadVasos = cantidadVasos;
        Contenido = contenido;
    }

    public bool HayVasos(int cantidad) => cantidad >= 0 && _cantidadVasos >= cantidad;
    public int GetCantidadVasos() => _cantidadVasos;
    public int GetContenido() => Contenido;

    public void DarVasos(int cantidad)
    {
        if (!HayVasos(cantidad)) throw new InvalidOperationException("No hay suficientes vasos.");
        _cantidadVasos -= cantidad;
    }
}
