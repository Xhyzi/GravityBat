using GravityBat_Constants;

namespace GravityBat_Data
{
    /// <summary>
    /// Clase para los datos de un intento de un nivel.
    /// </summary>
    public class AttemptData
    {
        #region Constructors
        /// <summary>
        /// Constructor vacio.
        /// </summary>
        public AttemptData() { }

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// <param name="taps">Numero de toques totales</param>
        /// <param name="swaps">Numero de cambios de gravedad totales</param>
        /// <param name="multiplier">Valor del multiplicador</param>
        /// <param name="highestMultiplier">Valor del maximo multiplicador</param>
        /// <param name="diamonds">Diamantes obtenidos</param>
        /// <param name="score">Puntuacion</param>
        /// <param name="gravityPower">Energia de gravedad</param>
        public AttemptData(int taps, int swaps, int multiplier, int highestMultiplier, bool[] diamonds, long score, int gravityPower, float speedModifier)
        {
            this.taps = taps;
            this.swaps = swaps;
            this.multiplier = multiplier;
            this.highestMultiplier = highestMultiplier;
            this.diamonds = diamonds;
            this.score = score;
            this.gravityPower = gravityPower;
            this.speedModifier = speedModifier;
        }
        #endregion

        #region Attributes
        private int taps;
        private int swaps;
        private int multiplier;
        private int highestMultiplier;
        private bool[] diamonds;
        private long score;
        private int gravityPower;
        private float speedModifier;
        #endregion

        #region Properties
        public int Taps
        {
            get { return taps; }
            set { taps = value; }
        }

        public int Swaps
        {
            get { return swaps; }
            set { swaps = value; }
        }

        public int Multiplier
        {
            get { return multiplier; }
            set
            {
                if (value < Constants.MULTIPLIER_MAX_VALUE)
                    multiplier = value;
                else
                    multiplier = Constants.MULTIPLIER_MAX_VALUE;

                if (multiplier > highestMultiplier)
                    highestMultiplier = multiplier;
            }
        }

        public int HighestMultiplier
        {
            get { return highestMultiplier; }
            set { highestMultiplier = value; }
        }

        public bool[] Diamonds
        {
            get { return diamonds; }
            set { diamonds = value; }
        }

        public int DiamondsCount
        {
            get
            {
                int c = 0;
                for (int i = 0; i < diamonds.Length; i++)
                    if (diamonds[i])
                        c++;
                return c;
            }
        }

        public long Score
        {
            get { return DecryptScore(score); }
            set { score = EncryptScore(value); }
        }

        public int GravityPower
        {
            get { return gravityPower; }
            set
            {
                gravityPower = value > Constants.MAX_GRAVITY_POWER ? Constants.MAX_GRAVITY_POWER : value; //Evita sobrepasar el maximo
            }
        }

        public float SpeedModifier
        {
            get { return speedModifier; }
            set { speedModifier = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Aniade valor a la puntuacion
        /// </summary>
        /// <param name="points"></param>
        public void AddScore(long points)
        {
            Score += points * multiplier;
        }

        /// <summary>
        /// Cifra en tiempo de ejecucion el valor de la puntuacion utilizando XOR con el PID.
        /// Esto dificulta la busqueda y edicion del valor utilizando editores de memoria
        /// como Cheat Engine o similares.
        /// Se utilizan dos keys para el cifrado para hacerlo más complejo.
        /// </summary>
        /// <param name="score">Valor de entrada a encriptar</param>
        /// <returns>Devuelve el valor encriptado.</returns>
        private long EncryptScore(long score)
        {
            score = score ^ GameManager.Instance.Data.PID_A;
            score = score ^ GameManager.Instance.Data.PID_B;

            return score;
        }

        /// <summary>
        /// Descifra en tiempo de ejecución el valor de la puntuación utilizando XOR con el PID.
        /// </summary>
        /// <param name="score">Valor de entrada a desencriptar.</param>
        /// <returns>Devuelve el valor desencriptado.</returns>
        private long DecryptScore(long score)
        {
            //Las claves se aplican en orden inverso.
            score = score ^ GameManager.Instance.Data.PID_B;
            score = score ^ GameManager.Instance.Data.PID_A;

            return score;
        }
        #endregion
    }
}
