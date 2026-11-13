using CafeTdd;

var maquina = new MaquinaDeCafe();
maquina.SetCafetera(new Cafetera(50));
maquina.SetVasosPequeno(new Vaso(5, 3));
maquina.SetVasosMediano(new Vaso(5, 5));
maquina.SetVasosGrande(new Vaso(5, 7));
maquina.SetAzucarero(new Azucarero(20));

Console.WriteLine("=== Máquina de café ===");
Console.WriteLine("Inventario inicial: 5 vasos de cada tamaño, 50 oz de café y 20 cucharadas de azúcar.");
Console.Write("Tamaño del vaso (pequeno, mediano, grande): ");
var tipo = Console.ReadLine()?.Trim() ?? "";
var vaso = maquina.GetTipoDeVaso(tipo);
if (vaso is null)
{
    Console.WriteLine("Tamaño no válido. Elija pequeno, mediano o grande.");
    return;
}

Console.Write("Cucharadas de azúcar (0 o más): ");
if (!int.TryParse(Console.ReadLine(), out var azucar) || azucar < 0)
{
    Console.WriteLine("Cantidad de azúcar no válida.");
    return;
}

var resultado = maquina.GetVasoDeCafe(vaso, vaso.GetContenido(), azucar);
Console.WriteLine(resultado == "Felicitaciones" ? "Felicitaciones, su café está listo." : resultado);
Console.WriteLine($"Inventario restante: café {maquina.GetCafetera().GetCantidadCafe()} oz, vasos {vaso.GetCantidadVasos()}, azúcar {maquina.GetAzucarero().GetCantidadAzucar()} cucharadas.");
