using Engine2D;

namespace SpriteDemo
{
    public enum EstadoExplorardor
    {
        Reposo = 0,
        CaminarDerecha = 1,
        CaminarIzquierda = 2
    }

    internal class Explorador : AnimatedItem
    {
        private int puntaje;
        public float Velocidad { get; set; } = 120f;
        public float DireccionX { get; set; } = 0f;
        //Variables para gestionar el salto del personaje
        public float VelocidadY { get; set; } = 0f;
        public float FuerzaSalto { get; set; } = -450f; // Negativo porque hacia arriba Y disminuye
        public float Gravedad { get; set; } = 1200f;    // Aceleración hacia abajo (píxeles/s²)
        public float NivelSuelo { get; set; }           // Coordenada Y del piso
        public bool EnElSuelo { get; private set; } = true;
        private EstadoExplorardor estadoActual;
        public int Puntaje { get => puntaje; set => puntaje = value; }
        //  3 filas (estados) y 4 columnas (fotogramas) esat asociado a la imagen del spirtes
        public Explorador(float x, float y) : base(x, y, columnas: 4, filas: 3, fps: 5f, "explorador")
        {
            this.Ancho = 50;
            this.Alto =90;
            puntaje = 0;
            NivelSuelo = y;
            //Forzar la fila inicial (0 = Reposo)  para la animacion del personaje
            estadoActual = EstadoExplorardor.Reposo;
            SeleccionarFila(0, loop: true);
        }

        public void Detener()
        {
            DireccionX = 0f;
            CambiarEstado(EstadoExplorardor.Reposo);
        }

        public void CambiarEstado(EstadoExplorardor nuevoEstado)
        {
            if (estadoActual == nuevoEstado) return;

            estadoActual = nuevoEstado;
            switch (nuevoEstado)
            {
                case EstadoExplorardor.Reposo:
                    SeleccionarFila(0, loop: true);
                    break;
                case EstadoExplorardor.CaminarDerecha:
                    SeleccionarFila(1, loop: true);
                    break;
                case EstadoExplorardor.CaminarIzquierda:
                    SeleccionarFila(2, loop: true);
                    break;
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            //  Física del salto (Eje Y) la coordenada en Y se actualizará hasta que llegue al piso.
            if (!EnElSuelo)
            {
                VelocidadY += Gravedad * deltaTime; // La gravedad frena la subida y acelera la caída
                this.Y += VelocidadY * deltaTime;

                // Verificar si ya tocó el suelo nuevamente
                if (this.Y >= NivelSuelo)
                {
                    this.Y = NivelSuelo;
                    VelocidadY = 0f;
                    EnElSuelo = true;
                }
            }

            //Actualizar desplazamiento horizontal con deltaTime
            this.X += DireccionX * Velocidad * deltaTime;
            if (DireccionX > 0)
                CambiarEstado(EstadoExplorardor.CaminarDerecha);
            else if (DireccionX < 0)
                CambiarEstado(EstadoExplorardor.CaminarIzquierda);
            else
                Detener();
        }

        public void MoverIzquierda()
        {
            DireccionX = -1f;
            CambiarEstado(EstadoExplorardor.CaminarIzquierda);
        }

        public void MoverDerecha()
        {
            DireccionX = 1f;
            CambiarEstado(EstadoExplorardor.CaminarDerecha);
        }

        public void Saltar()
        {
            // Solo permite saltar si está apoyado en el suelo
            if (EnElSuelo)
            {
                VelocidadY = FuerzaSalto;
                EnElSuelo = false;
            }
        }
    }
}