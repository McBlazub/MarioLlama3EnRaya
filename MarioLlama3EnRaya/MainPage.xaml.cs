namespace MarioLlama3EnRaya
{
    public partial class MainPage : ContentPage
    {
        private TicTacToe juego;
        private Button[,] botones;
        private int victoriasX = 0;
        private int victoriasO = 0;

        /// <summary>
        /// Inicializa el proyecto, la página principal con el juego
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            juego = new TicTacToe();
            botones = new Button[,]
            {
                { btn0, btn1, btn2 },
                { btn3, btn4, btn5 },
                { btn6, btn7, btn8 }
            };
        }

        /// <summary>
        /// Función al presionar botón
        /// Verifica si hay ganador y maneja los turnos
        /// </summary>
        /// <param name="sender">Hace referencia al boton presionado.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void Boton_Clicked(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            int fila = Grid.GetRow(boton);
            int columna = Grid.GetColumn(boton);

            int turno = juego.jugada(columna, fila);
            if (turno == -1) return;

            int jugador = turno % 2 == 0 ? 2 : 1;
            boton.ImageSource = jugador == 1 ? "img_x.png" : "img_o.png";
            boton.IsEnabled = false;

            int ganador = juego.Ganador();
            if (ganador != 0)
            {
                if (ganador == 1)
                {
                    victoriasO++;
                }
                else
                {
                    victoriasX++;
                }

                ActualizarMarcador();
                ReiniciarTablero();
                return;
            }

            if (juego.turno >= 9)
            {
                ReiniciarTablero();
                return;
            }

            lblEstado.Text = $"Turno: {(turno % 2 == 0 ? "X" : "O")}";
        }

        /// <summary>
        /// Pone el marcador al día.
        /// </summary>
        private void ActualizarMarcador()
        {
            lblVictoriasX.Text = victoriasX.ToString();
            lblVictoriasO.Text = victoriasO.ToString();
        }

        /// <summary>
        /// Reinicia el tablero y el estado del juego para una nueva partida.
        /// </summary>
        private void ReiniciarTablero()
        {
            juego.Reiniciar();
            lblEstado.Text = "Turno: X";

            foreach (var boton in botones)
            {
                boton.ImageSource = null;
                boton.IsEnabled = true;
            }
        }
    }

}

