using Engine2D;
using static System.Net.Mime.MediaTypeNames;
namespace SpriteDemo
{
    public partial class Escenario : Form
    {
        GraphicsScene scene;
        List<Moneda> monedas= new List<Moneda>();
        Explorador explorador;
        public Escenario()
        {
            InitializeComponent();
            scene = new GraphicsScene();
            scene.Width = 3200;
            scene.Height = 600;
            scene.SetBackgroundImage("fondo_bosque");
            visor.Scene = scene;
            CargarMonedas();
            explorador = new Explorador(100, 330);
            scene.AddItem(explorador);


            this.Shown += (s, e) => visor.Start();
        }

        void CargarMonedas()
        {
            try
            {
                string contenido = Properties.Resources.CoordenadasMonedasL1;
                if (string.IsNullOrWhiteSpace(contenido))
                    throw new Exception("El archivo de coordenadas está vacío.");

                // Dividir el texto en líneas (compatible con Windows y Unix)
                string[] lineas = contenido.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] coordenadas;
                foreach (string line in lineas)
                {
                    string lineaLimpia = line.Trim();
                    coordenadas = lineaLimpia.Split(',');
                    float x = float.Parse(coordenadas[0]);
                    float y = float.Parse(coordenadas[1]);
                    Moneda moneda = new Moneda(x, y);
                    monedas.Add(moneda);
                    scene.AddItem(moneda);
                }

            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el formato de las coordenadas: {ex.Message}",
                        "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void visor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                explorador.MoverDerecha();
            }
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                explorador.MoverIzquierda();
            }
            else if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
            {
                explorador.Saltar();
            }
        }

        private void visor_KeyUp(object sender, KeyEventArgs e)
        {
            // Solo detenemos el desplazamiento horizontal si soltó las teclas de movimiento lateral
            // (así, si suelta la tecla de salto en el aire, no se frena en horizontal)
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                explorador.Detener();
            }
        }

        private void ActualizarCamara()
        {

            // Calcular la posición ideal manteniendo al explorador centrado en el visor
            float posicionObjetivoX = (explorador.X + explorador.Ancho / 2f) - (visor.Width / 2f);

            // Definir el límite máximo hasta donde puede desplazarse la cámara a la derecha
            float maxCamaraX = Math.Max(0, scene.Width - visor.Width);

            // Restringir la cámara dentro de los límites del escenario [0, maxCamaraX]
            posicionObjetivoX = Math.Max(0, Math.Min(posicionObjetivoX, maxCamaraX));

            // Asignar la coordenada redondeada a la cámara de la escena
            scene.CamaraX = (float)Math.Round(posicionObjetivoX);
        }
        

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            float deltaTime = gameTimer.Interval / 1000f; // ~60 FPS (o tu intervalo / 1000f)

            scene.Update(deltaTime);
            ActualizarCamara();
            visor.Invalidate();

            foreach (var moneda in monedas)
            {
                if(explorador.ColisionaCon(moneda))
                {
                    scene.RemoveItem(moneda);
                    monedas.Remove(moneda);
                    explorador.Puntaje += 20;
                    lblScore.Text = "Score :" + explorador.Puntaje;
                    break;
                }
            }

        }


    }
}
