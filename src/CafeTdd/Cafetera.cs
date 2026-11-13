namespace CafeTdd;

public sealed class Cafetera
{
    private int _cantidadCafe;

    public Cafetera(int cantidadCafe)
    {
        if (cantidadCafe < 0) throw new ArgumentOutOfRangeException(nameof(cantidadCafe));
        _cantidadCafe = cantidadCafe;
    }

    public bool HayCafe(int cantidad) => cantidad >= 0 && _cantidadCafe >= cantidad;
    public int GetCantidadCafe() => _cantidadCafe;

    public void DarCafe(int cantidad)
    {
        if (!HayCafe(cantidad)) throw new InvalidOperationException("No hay suficiente café.");
        _cantidadCafe -= cantidad;
    }
}
