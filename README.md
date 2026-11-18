# SpriteDemo — Guía de Creación de Personajes Animados con Engine2D

Repositorio oficial del tutorial guiado y código fuente de referencia para el curso de **Programación Orientada a Objetos**. 
 [Ver Tutorial](Guia_Creacion_Sprites.pdf)
Este proyecto demuestra cómo cargar hojas de sprites (*sprite sheets*), controlar animaciones por estado y gestionar el desplazamiento de cámara en **C# (Windows Forms - .NET 8.0)** utilizando la librería **`Engine2D.dll`**.  [Ir a la librería](./Libreria-grafica/Engine2D.dll)

---

## Estructura del Proyecto

* `Escenario.cs` — Formulario principal (`Form`). Configura `GraphicsScene`, vincula el control `GraphicsView` (`visor`), carga las monedas desde recursos y ejecuta el bucle principal mediante `gameTimer`.
* `Explorador.cs` — Clase del personaje principal. Hereda de `AnimatedItem`, gestiona estados de animación (`EstadoExplorardor`), físicas de salto y movimiento horizontal con `deltaTime`.
* `Moneda.cs` — Elemento coleccionable estático. Hereda de `GraphicsItem`.
* `Resources/` — Recursos incrustados en el proyecto (`.resx`):
  * `explorador.png` — Hoja de sprites de 3 filas × 4 columnas (Reposo, Caminar Derecha, Caminar Izquierda).
  * `moneda.png` — Sprite estático del coleccionable.
  * `fondo_bosque.jpg` — Imagen de fondo de la escena (`3200 × 600 px`).
  * `CoordenadasMonedasL1.txt` — Archivo plano con las posiciones `X,Y` de las monedas en el nivel.

---

## Requisitos y Configuración Inicial

1. **Entorno:** Visual Studio 2022 o superior con carga de trabajo de escritorio para **.NET 8.0 (WinForms)**.
2. **Referencia externa:** Vincular `Engine2D.dll` en el proyecto (*Clic derecho en Dependencias → Agregar referencia del proyecto → Examinar*).
3. **Controles en `Escenario` (`800 × 600`):**
   * `GraphicsView` nombrado `visor` (componente de `Engine2D`).
   * `Label` nombrado `lblScore` para visualizar el puntaje.
   * `Timer` nombrado `gameTimer` con `Enabled = True` e `Interval = 20` ms.

---

## Conceptos Clave Implementados en el Tutorial

### 1. Animación por Matriz de Sprites (`AnimatedItem`)
La hoja `explorador.png` está organizada por filas según el estado lógico del personaje:
* **Fila 0:** `Reposo`
* **Fila 1:** `CaminarDerecha`
* **Fila 2:** `CaminarIzquierda`

El cambio de animación se realiza invocando `SeleccionarFila(indice, loop: true)` dentro de `CambiarEstado(...)`, y el avance de fotogramas se actualiza automáticamente al llamar `base.Update(deltaTime)`.

### 2. Bucle del Juego y Colisiones (`gameTimer_Tick`)
En cada ciclo de `20 ms`:
1. Se actualiza la escena y las animaciones con `scene.Update(deltaTime)`.
2. Se recalcula la posición de `scene.CamaraX` mediante `ActualizarCamara()` para mantener centrado al personaje dentro de los límites `[0, scene.Width - visor.Width]`.
3. Se verifican las colisiones gráficas de forma nativa con `explorador.ColisionaCon(moneda)` para remover el ítem de la escena e incrementar el puntaje.

---

## Nota de Arquitectura para el Proyecto del Curso

Este ejemplo muestra el funcionamiento directo de la capa gráfica (`Engine2D`). Para el proyecto final de la asignatura:
* **No repliques interfaces innecesarias** (como `IRecogible` de un solo método) cuando `Engine2D` ya resuelve la detección espacial mediante `ColisionaCon(...)`.
* Mantén las reglas de negocio y el cálculo de coordenadas lógicas desacoplados de los componentes visuales.
