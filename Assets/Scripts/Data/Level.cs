using UnityEngine;

namespace GravityBat_Data
{
    /// <summary>
    /// Representa un nivel. 
    /// Utilizado en GameData, que contiene un array con los niveles del juego.
    /// </summary>
    [System.Serializable]
    public class Level
    {
        #region Constructors
        /// <summary>
        /// Constructor con parametros de Level.
        /// </summary>
        /// <param name="unlocked">Si esta desbloqueado o no</param>
        /// <param name="completed">Si ha sido completado</param>
        /// <param name="secretDiamond">Indica que diamantes han sido obtenidos y cuales no</param>
        /// <param name="attempts">Numero de intentos</param>
        /// <param name="bestScore">Mejor puntuacion</param>
        /// <param name="totalTaps">Numero total de toques</param>
        /// <param name="totalSwaps">Numero total de cambios de gravedad</param>
        /// <param name="highestMultiplier">Multiplicador mas alto alcanzado</param>
        public Level(bool unlocked, bool completed,
                bool[] secretDiamond, int attempts, int bestScore,
                int totalTaps, int totalSwaps, int highestMultiplier)
        {
            this.unlocked = unlocked;
            this.completed = completed;
            this.secretDiamond = secretDiamond;
            this.attempts = attempts;
            this.bestScore = bestScore;
            this.totalTaps = totalTaps;
            this.totalSwaps = totalSwaps;
            this.highestMultiplier = highestMultiplier;
        }

        /// <summary>
        /// Constructor vacío sin parametros.
        /// </summary>
        public Level() { }
        #endregion

        #region Attributes
        private bool unlocked;                      //Si el nivel esta desbloqueado
        private bool completed;                     //Si se ha completado el nivel
        private bool[] secretDiamond = new bool[3]; //Si se han obtenido los diamantes ocultos
        private int attempts;                       //Numero de intentos
        private long bestScore;                      //Mejor puntuacion
        private int totalTaps;                      //Total de pulsaciones en el boton derecho
        private int totalSwaps;                     //Total de cambios de gravedad
        private int highestMultiplier;              //Mayor multiplicador
        #endregion

        #region Properties
        public bool Unlocked
        {
            get { return unlocked; }
            set { unlocked = value; }
        }

        public bool Completed
        {
            get { return completed; }
            set { completed = value; }
        }

        public bool[] SecretDiamonds
        {
            get { return secretDiamond; }
            set { secretDiamond = value; }
        }

        public int Attempts
        {
            get { return attempts; }
            set { attempts = value; }
        }

        public long BestScore
        {
            get { return bestScore; }
            set { bestScore = value; }
        }

        public int TotalTaps
        {
            get { return totalTaps; }
            set { totalTaps = value; }
        }

        public int TotalSwaps
        {
            get { return totalSwaps; }
            set { totalSwaps = value; }
        }

        public int HighestMultiplier
        {
            get { return highestMultiplier; }
            set { highestMultiplier = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Devuelve el numero de diamantes obtenidos en el nivel
        /// </summary>
        /// <returns>int, numero de diamantes obtenidos del nivel</returns>
        public int DiamondsCount()
        {
            int count = 0;

            for (int i = 0; i < secretDiamond.Length; i++)
            {
                if (secretDiamond[i])
                    count++;
            }

            return count;
        }
        #endregion
    }
}