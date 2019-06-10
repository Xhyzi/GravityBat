using GravityBat_Constants;
using UnityEngine;

namespace GravityBat_Data
{
    /// <summary>
    /// Datos del juego que son guardados de forma persistence.
    /// Clase base para heredar por GameManager y SaveData.
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        #region SaveBlock Attributes
        /// <summary>
        /// Array con los niveles del juego
        /// </summary>
        protected Level[,] levelArray;    //[WORLD, LEVEL]

        /// <summary>
        /// ID del jugador generado aleatoriamente. Key para encrypt.
        /// </summary>
        protected long pID_A;
        protected long pID_B;

        //Opciones del usuario
        protected Languages language;
        protected float musicVolume;
        protected float sfxVolume;
        protected float gameButtonOpacity;
        protected bool autoRetry;
        protected bool music;
        protected bool sfx;
        protected bool showLevelPanel;
        protected bool swapLevelPanels;
        protected bool enableTutorials;
        protected bool isTutorialTapDone;
        protected bool isTutorialSwapDone;
        protected bool isTutorialSwapConsecuencesDone;
        protected bool isTutorialScoreDone;
        protected bool isTutorialGemsDone;
        protected bool isWelcomeMessageDone;
        #endregion

        #region Properties
        public Level[,] LevelArray
        {
            get { return levelArray; }
            set { levelArray = value; }
        }

        public long PID_A
        {
            get { return pID_A; }
            set { pID_A = value; }
        }

        public long PID_B
        {
            get { return pID_B; }
            set { pID_B = value; }
        }

        public Languages Language
        {
            get { return language; }
            set { language = value; }
        }

        public float MusicVolume
        {
            get { return musicVolume; }
            set { musicVolume = value; }
        }

        public float SfxVolume
        {
            get { return sfxVolume; }
            set { sfxVolume = value; }
        }

        public float GameButtonOpacity
        {
            get { return gameButtonOpacity; }
            set { gameButtonOpacity = value; }
        }

        public bool AutoRetry
        {
            get { return autoRetry; }
            set { autoRetry = value; }
        }

        public bool Music
        {
            get { return music; }
            set { music = value; }
        }

        public bool Sfx
        {
            get { return sfx; }
            set { sfx = value; }
        }

        public bool ShowLevelPanels
        {
            get { return showLevelPanel; }
            set { showLevelPanel = value; }
        }

        public bool SwapLevelPanels
        {
            get { return swapLevelPanels; }
            set
            {
                if (value)
                    EnableTutorials = false;

                swapLevelPanels = value;
            }
        }

        public bool EnableTutorials
        {
            get { return enableTutorials; }
            set
            {
                enableTutorials = value;

                if (enableTutorials)
                {
                    isTutorialTapDone = false;
                    isTutorialSwapDone = false;
                    isTutorialSwapConsecuencesDone = false;
                    isTutorialScoreDone = false;
                    isTutorialGemsDone = false;
                    swapLevelPanels = false;
                    showLevelPanel = true;
                }
            }
        }

        public bool IsTutorialTapDone
        {
            get { return isTutorialTapDone; }
            set { isTutorialTapDone = value; }
        }

        public bool IsTutorialSwapDone
        {
            get { return isTutorialSwapDone; }
            set { isTutorialSwapDone = value; }
        }

        public bool IsTutorialSwapConsecuencesDone
        {
            get { return isTutorialSwapConsecuencesDone; }
            set { isTutorialSwapConsecuencesDone = value; }
        }

        public bool IsTutorialScoreDone
        {
            get { return isTutorialScoreDone; }
            set { isTutorialScoreDone = value; }
        }

        public bool IsTutorialMultiplierDone
        {
            get { return isTutorialGemsDone; }
            set { isTutorialGemsDone = value; }
        }

        public bool IsWelcomeMessageDone
        {
            get { return isWelcomeMessageDone; }
            set { isWelcomeMessageDone = value; }
        }
        #endregion

        #region Methods
        //Permiten obtener datos calculados sobre los datos de la partida.

        /// <summary>
        /// Devuelve el numero total de toques del jugador.
        /// </summary>
        /// <returns>int</returns>
        public int TotalTaps()
        {
            int taps = 0;

            for (int i = 0; i < levelArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelArray.GetLength(1); j++)
                {
                    taps += levelArray[i, j].TotalTaps;
                }
            }

            return taps;
        }

        /// <summary>
        /// Devuelve el numero total de cambios de gravedad del jugador
        /// </summary>
        /// <returns>int</returns>
        public int TotalSwaps()
        {
            int swaps = 0;

            for (int i = 0; i < levelArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelArray.GetLength(1); j++)
                {
                    swaps += levelArray[i, j].TotalSwaps;
                }
            }
            return swaps;
        }

        /// <summary>
        ///  Devuelve el mayor multiplicador del jugador en cualquier nivel
        /// </summary>
        /// <returns>int</returns>
        public int LongestCombo()
        {
            int combo = 0;

            for (int i = 0; i < levelArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelArray.GetLength(1); j++)
                {
                    if (combo < levelArray[i, j].HighestMultiplier)
                    {
                        combo = levelArray[i, j].HighestMultiplier;
                    }
                }
            }
            return combo;
        }

        /// <summary>
        /// Devuelve el numero total de diamantes obtenidos por el jugador en todos los niveles.
        /// </summary>
        /// <returns>int</returns>
        public int TotalDiamonds()
        {
            int diamonds = 0;

            for (int i = 0; i < levelArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelArray.GetLength(1); j++)
                {
                    diamonds += levelArray[i, j].DiamondsCount();
                }
            }

            return diamonds;
        }

        /// <summary>
        /// Devuelve el numero de niveles completados
        /// </summary>
        /// <returns>int</returns>
        public int LevelsCompleted()
        {
            int completed = 0;

            for (int i = 0; i < levelArray.GetLength(0); i++)
                for (int j = 0; j < levelArray.GetLength(1); j++)
                    if (levelArray[i, j].Completed)
                        completed++;

            return completed;
        }

        /// <summary>
        /// Devuelve el numero de niveles especiales completados
        /// </summary>
        /// <returns>int</returns>
        public int SecretLevelsCompleted()
        {
            int completed = 0;

            for (int i = 0; i < levelArray.GetLength(0); i++)
            {
                if (levelArray[i, 4].Completed)
                    completed++;
            }

            return completed;
        }

        /// <summary>
        /// Devuelve el numero total de intentos realizados entre todos los niveles
        /// </summary>
        /// <returns>int</returns>
        public int TotalAttempts()
        {
            int attempts = 0;

            for (int i = 0; i < levelArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelArray.GetLength(1); j++)
                {
                    attempts += levelArray[i, j].Attempts;
                }
            }
            return attempts;
        }

        /// <summary>
        /// Devuelve la mayor puntuacion obtenida por el jugador en cualquier nivel
        /// </summary>
        /// <returns>long</returns>
        public long RecordScore()
        {
            long record = 0;

            for (int i = 0; i < levelArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelArray.GetLength(1); j++)
                {
                    if (levelArray[i, j].BestScore > record)
                        record = levelArray[i, j].BestScore;
                }
            }

            return record;
        }

        /// <summary>
        /// Comprueba si un mundo ha sido desbloqueado.
        /// El Mundo 0 esta desbloqueado por defecto.
        /// Para el resto de mundos se debe de haber
        /// completado los primeros 3 niveles del mundo anterior.
        /// </summary>
        /// <param name="worldIndex">Indice del mundo</param>
        /// <returns>bool</returns>
        public bool WorldUnlocked(int worldIndex)
        {
            const int MAIN_LEVELS = 3;  //Niveles necesarios para desbloquear mundos

            if (worldIndex != 0)
            {
                for (int i = 0; i < MAIN_LEVELS; i++)
                {
                    if (!levelArray[worldIndex - 1, i].Completed)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }

            return true;
        }

        /// <summary>
        /// Devuelve la cantidad de mundos desbloqueados por el jugador
        /// </summary>
        /// <returns>int</returns>
        public int WorldsUnlockedCount()
        {
            int worlds = 0;

            for (int i = 0; i < levelArray.GetLength(0); i++)
            {
                if (WorldUnlocked(i))
                {
                    worlds++;
                }
            }

            return worlds;
        }

        /// <summary>
        /// Genera un PID de forma aleatoria.
        /// </summary>
        /// <returns>long con el pid</returns>
        public static long GeneratePID()
        {
            return (Random.Range(int.MinValue, int.MaxValue) << 16) + Random.Range(int.MinValue, int.MaxValue);
        }
        #endregion
    }
}