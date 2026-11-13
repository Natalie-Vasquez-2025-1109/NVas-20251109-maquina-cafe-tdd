# Práctica TDD: Máquina de café

Implementación en C#/.NET 8 con xUnit. La solución separa biblioteca, aplicación de consola y pruebas. El proyecto se construyó con el ciclo Rojo, Verde y Refactorizar, y ahora cuenta con un pipeline de integración y despliegue continuo en GitHub Actions.

## Estructura

- `src/CafeTdd`: clases Vaso, Cafetera, Azucarero y MaquinaDeCafe.
- `src/CafeTdd.App`: programa de consola para escoger tamaño de vaso y azúcar.
- `tests/CafeTdd.Tests`: pruebas automatizadas con xUnit.
- `evidence`: evidencia del ciclo Rojo y Verde.
- `.github/workflows/ci-pipeline.yml`: workflow de CI/CD.

## Ejecutar

Desde la carpeta raíz del repositorio:

```powershell
dotnet test CafeTdd.sln
```

Para usar la máquina:

```powershell
dotnet run --project src/CafeTdd.App
```

La aplicación inicia con 5 vasos por tamaño, 50 oz de café y 20 cucharadas de azúcar; solicita tamaño y azúcar, sirve si alcanza el inventario e informa lo restante.

## Qué cubre

- Inventario de vasos, café y azúcar: disponibilidad y descuento.
- Selección de los vasos pequeño (3 oz), mediano (5 oz) y grande (7 oz).
- Mensajes “No hay Vasos”, “No hay Cafe”, “No hay Azucar” y “Felicitaciones”.
- No se modifica ningún inventario si falta un insumo; al servir, se descuentan los tres recursos solicitados.
- Casos de límites: inventario justo, insuficiente, cero y selección inválida.

## Pipeline CI/CD

El workflow `CI Pipeline` se ejecuta con cada `push` y cada `pull_request`, y se puede revisar en la pestaña **Actions** del repositorio.

| Job | Cuándo corre | Qué hace |
| --- | --- | --- |
| `build` (Integración continua) | Push y pull request en cualquier rama | Clona el repositorio, configura .NET 8, restaura dependencias, compila la solución y ejecuta todas las pruebas. |
| `deploy` (Despliegue continuo) | Solo en push a `main`, después de que `build` termine bien | Publica la aplicación de consola en modo Release y guarda el resultado como artefacto `maquina-cafe`. |

Si una prueba falla, el pipeline se marca en rojo y el despliegue no se ejecuta.

## Control de versiones

- `main`: rama estable. Solo recibe cambios mediante pull request.
- `develop`: rama de integración del desarrollo.
- `feature/*`: una rama por funcionalidad, creada desde `develop`. Ejemplo: `feature/nueva-funcionalidad`.

Flujo de trabajo: `feature/*` → pull request hacia `develop` → pull request hacia `main`. Cada pull request ejecuta el pipeline antes de fusionarse.
