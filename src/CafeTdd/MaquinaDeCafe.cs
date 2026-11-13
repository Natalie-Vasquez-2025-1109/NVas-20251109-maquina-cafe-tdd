namespace CafeTdd;

public sealed class MaquinaDeCafe
{
    private Cafetera _cafetera = new(0);
    private Vaso _vasosPequeno = new(0, 3);
    private Vaso _vasosMediano = new(0, 5);
    private Vaso _vasosGrande = new(0, 7);
    private Azucarero _azucarero = new(0);

    public void SetCafetera(Cafetera cafetera) => _cafetera = cafetera ?? throw new ArgumentNullException(nameof(cafetera));
    public void SetVasosPequeno(Vaso vasos) => _vasosPequeno = vasos ?? throw new ArgumentNullException(nameof(vasos));
    public void SetVasosMediano(Vaso vasos) => _vasosMediano = vasos ?? throw new ArgumentNullException(nameof(vasos));
    public void SetVasosGrande(Vaso vasos) => _vasosGrande = vasos ?? throw new ArgumentNullException(nameof(vasos));
    public void SetAzucarero(Azucarero azucarero) => _azucarero = azucarero ?? throw new ArgumentNullException(nameof(azucarero));

    public Cafetera GetCafetera() => _cafetera;
    public Azucarero GetAzucarero() => _azucarero;
    public Vaso GetVasosPequeno() => _vasosPequeno;
    public Vaso GetVasosMediano() => _vasosMediano;
    public Vaso GetVasosGrande() => _vasosGrande;

    public Vaso? GetTipoDeVaso(string tipo) => tipo.Trim().ToLowerInvariant() switch
    {
        "pequeno" or "pequeño" => _vasosPequeno,
        "mediano" => _vasosMediano,
        "grande" => _vasosGrande,
        _ => null
    };

    public string GetVasoDeCafe(Vaso vaso, int cantidadCafe, int cantidadAzucar)
    {
        ArgumentNullException.ThrowIfNull(vaso);
        if (cantidadCafe < 0 || cantidadAzucar < 0)
            throw new ArgumentOutOfRangeException("Las cantidades solicitadas no pueden ser negativas.");
        if (!vaso.HayVasos(1)) return "No hay Vasos";
        if (!_cafetera.HayCafe(cantidadCafe)) return "No hay Cafe";
        if (!_azucarero.HayAzucar(cantidadAzucar)) return "No hay Azucar";

        vaso.DarVasos(1);
        _cafetera.DarCafe(cantidadCafe);
        _azucarero.DarAzucar(cantidadAzucar);
        return "Felicitaciones";
    }
}
