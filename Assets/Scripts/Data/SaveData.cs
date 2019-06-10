using GravityBat_Localization;
using GravityBat_Constants;
using GravityBat_Debug;
using UnityEngine;

namespace GravityBat_Data { 
    /// <summary>
    /// Clase utilizada para almacenar los datos guardados en un fichero de objetos.
    /// Es serializable.
    /// </summary>
    [System.Serializable]
    public class SaveData : GameData
    {
        #region Constructors
        /// <summary>
        /// Constructor de SaveData utilizado para generar unos datos
        /// de guardado a partir de la informacion contenida en el GameManager.
        /// </summary>
        /// <param name="GM">Instancia del GameManager desde el que se leen los datos a guardar.</param>
        public SaveData(GameManager GM)
        {
            levelArray = GM.Data.LevelArray;

            language = GM.Data.Language;
            musicVolume = GM.Data.MusicVolume;
            sfxVolume = GM.Data.SfxVolume;
            gameButtonOpacity = GM.Data.GameButtonOpacity;
            autoRetry = GM.Data.AutoRetry;
            music = GM.Data.Music;
            sfx = GM.Data.Sfx;
            showLevelPanel = GM.Data.ShowLevelPanels;
            swapLevelPanels = GM.Data.SwapLevelPanels;
            enableTutorials = GM.Data.EnableTutorials;
            isWelcomeMessageDone = GM.Data.IsWelcomeMessageDone;
        }

        /// <summary>
        /// Constructor de vacio SaveData, utilizado para generar unos datos de guardado
        /// en caso de no haber datos guardados previamente.
        /// </summary>
        public SaveData()
        {
            levelArray = new Level[Constants.WORLD_AMMOUNT, Constants.LEVELS_PER_WORLD_AMMOUNT];

            for (int i = 0; i < levelArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelArray.GetLength(1); j++)
                {
                    levelArray[i, j] = new Level(
                        (i == Constants.BASE_UNLOCKED_LEVEL_WORLD_INDEX && j == Constants.BASE_UNLOCKED_LEVEL_INDEX) ? Constants.LEVEL_UNLOCKED : Constants.LEVEL_LOCKED,
                        Constants.LEVEL_INCOMPLETE, 
                        new bool[] { Constants.DIAMOND_NOT_OBTAINED, Constants.DIAMOND_NOT_OBTAINED, Constants.DIAMOND_NOT_OBTAINED }, 
                        0, 0, 0, 0, 0);
                }
            }

            language = Strings.GetSystemLanguages();

            musicVolume = Constants.DEFAULT_MUSIC_VOLUME;
            sfxVolume = Constants.DEFAULT_SFX_VOLUME; ;
            gameButtonOpacity = Constants.DEFAULT_BUTTON_OPACITY;
            autoRetry = Constants.DEFAULT_AUTO_RETRY;
            music = Constants.DEFAULT_MUSIC_ENABLED;
            sfx = Constants.DEFAULT_SFX_ENABLED;
            showLevelPanel = Constants.DEFAULT_SHOW_LEVEL_PANEL;
            swapLevelPanels = Constants.DEFAULT_SWAP_LEVEL_PANEL;
            enableTutorials = Constants.DEFAULT_ENABLE_TUTORIALS;
            isWelcomeMessageDone = false;

            if (_Debug.DEBUG_MODE)
            {
                bool[] level_1 = { false, false, true };
                bool[] level_2 = { false, false, false };
                bool[] level_3 = { true, true, true };

                levelArray[0, 0].Completed = true;
                levelArray[0, 0].TotalTaps = 3689;
                levelArray[0, 0].TotalSwaps = 523;
                levelArray[0, 0].HighestMultiplier = 27;
                levelArray[0, 0].SecretDiamonds = level_1;
                levelArray[0, 0].Attempts = 71;
                levelArray[0, 0].BestScore = 38700;

                levelArray[0, 1].Unlocked = true;
                levelArray[0, 1].Completed = true;
                levelArray[0, 1].TotalTaps = 2315;
                levelArray[0, 1].TotalSwaps = 298;
                levelArray[0, 1].HighestMultiplier = 16;
                levelArray[0, 1].SecretDiamonds = level_2;
                levelArray[0, 1].Attempts = 48;
                levelArray[0, 1].BestScore = 28600;

                levelArray[0, 2].Unlocked = true;
                levelArray[0, 2].Completed = true;
                levelArray[0, 2].TotalTaps = 1528;
                levelArray[0, 2].TotalSwaps = 216;
                levelArray[0, 2].HighestMultiplier = 13;
                levelArray[0, 2].SecretDiamonds = level_3;
                levelArray[0, 2].Attempts = 37;
                levelArray[0, 2].BestScore = 21000;

                levelArray[0, 3].Unlocked = true;
                levelArray[0, 3].TotalTaps = 617;
                levelArray[0, 3].TotalSwaps = 76;
                levelArray[0, 3].HighestMultiplier = 15;
                levelArray[0, 3].Attempts = 19;
                levelArray[0, 3].BestScore = 0;
            }
        }
        #endregion
    }
}
