namespace CafeTdd;

public sealed class Azucarero
{
    private int _cantidadAzucar;

    public Azucarero(int cantidadAzucar)
    {
        if (cantidadAzucar < 0) throw new ArgumentOutOfRangeException(nameof(cantidadAzucar));
        _cantidadAzucar = cantidadAzucar;
    }

    public bool HayAzucar(int cantidad) => cantidad >= 0 && _cantidadAzucar >= cantidad;
    public int GetCantidadAzucar() => _cantidadAzucar;

    public void DarAzucar(int cantidad)
    {
        if (!HayAzucar(cantidad)) throw new InvalidOperationException("No hay suficiente azúcar.");
        _cantidadAzucar -= cantidad;
    }
}
