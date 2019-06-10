using UnityEngine;
using GravityBat_Constants;
using GravityBat_Data;

namespace GravityBat_Debug
{

    /// <summary>
    /// Clase utilizada para activar el modo debug.
    /// Se suscribe a los diferentes eventos del juego, notificando los eventos disparados por del Debug.Log
    /// El modo debug se activara automaticamente en las versiones DebugBuil
    /// </summary>
    public class _Debug : ScriptableObject
    {

        /// <summary>
        /// Activa y desactiva el modo Debug.
        /// </summary>
        public static bool DEBUG_MODE = Debug.isDebugBuild;   //Activa/desactiva el modo debug
        public static bool enabledDebugUI = true;
        private static bool titlescreenSubscribed = false;
        private static bool levelSelectionSusbscribed = false;
        private static bool levelSuscribed = false;

        private static GameManager GM;
        private static TitleScreenManager TSM;
        private static LevelSelectionManager LSM;
        private static LevelManager LM;

        /// <summary>
        /// Inicializa el modo debug.
        /// Suscribe los eventos de GameManager
        /// </summary>
        /// <param name="GM"></param>
        public static void Init(GameManager GM)
        {
            if (_Debug.DEBUG_MODE)
            {
                Debug.Log("Inicializando modo Debug...");
                _Debug.GM = GM;

                Debug.Log("Suscribiendose a eventos del GameManager...");
                GM.OnGameStateChanged += NotifyGameStateChanged;
                GM.OnFadeCompleted += NotifyFadeCompleted;
                GM.OnLanguageChanged += NotifyLanguageChanged;
                GM.OnStringsLanguageUpdated += NotifyLanguageTextsUpdated;
                GM.OnTitleScreenSceneLoaded += HandleOnTitleScreenSceneLoaded;
                GM.OnLevelSelectSceneLoaded += HandleOnLevelSelectSceneLoaded;
                GM.OnLevelSceneLoaded += HandleOnLevelSceneLoaded;
                GM.OnTextLoadedToMemory += NotifyTextLoadedToMemory;
                GM.OnDataTextLoadedToMemory += NotifyDataTextLoadedToMemory;
                GM.OnTutorialTextLoadedToMemory += NotifyTutorialTextLoadedToMemory;
                GM.OnLevelCompleted += NotifyLevelCompleted;
                GM.OnLevelUnlocked += NotifyLevelUnlocked;
                GM.OnUserConfigChanged += NotifyUserConfigChanged;
                GM.OnGameDataSaved += NotifyGameDataSaved;
                GM.OnGameDataLoaded += NotifyGameDataLoaded;

                Debug.Log("Suscripcion completada...");
                Debug.Log("Modo Debug iniciado correctamente.");


                GM.OnGameStateChanged += NotifyGameStateChanged;

            }
        }

        #region EventHandlers
        #region GameManager
        private static void HandleOnTitleScreenSceneLoaded()
        {
            NotifyTitleSceneLoaded();
            SubscribeTitleScreenScene();
        }

        private static void HandleOnLevelSelectSceneLoaded()
        {
            NotifyLevelSelectSceneLoaded();
            SubscribeLevelSelectScene();
        }

        private static void HandleOnLevelSceneLoaded()
        {
            NotifyLevelSceneLoaded();
            SubscribeLevelScene();
        }

        private static void NotifyGameStateChanged()
        {
            Debug.Log("Ha cambiado el GameState: " + GameManager.Instance.GameState);
        }

        private static void NotifyFadeCompleted()
        {
            Debug.Log("Se ha completado el fade in.");
        }

        private static void NotifyLanguageChanged()
        {
            Debug.Log("Se ha cambiado el idioma: " + GameManager.Instance.Data.Language);
        }

        private static void NotifyLanguageTextsUpdated()
        {
            Debug.Log("Se han actualizado los textos según el idioma: " + GameManager.Instance.Data.Language);
        }

        private static void NotifyTextLoadedToMemory()
        {
            Debug.Log("Textos cargados en memoria.");
        }

        private static void NotifyDataTextLoadedToMemory()
        {
            Debug.Log("Textos de datos cargados en memoria.");
        }

        private static void NotifyTutorialTextLoadedToMemory()
        {
            Debug.Log("Textos de los tutoriales cargados en memoria");
        }

        private static void NotifyLevelCompleted(int worldIndex, int levelIndex)
        {
            Debug.Log("Nivel completado: " +
                worldIndex + "-" + levelIndex);
        }

        private static void NotifyLevelUnlocked(int worldIndex, int levelIndex)
        {
            Debug.Log("Nivel desbloqueado: " +
                worldIndex + "-" + levelIndex);
        }

        private static void NotifyUserConfigChanged()
        {
            Debug.Log("Se ha cambiado la configuración del usuario.");
        }

        private static void NotifyTitleSceneLoaded()
        {
            Debug.Log("Se ha cargado la escena principal.");
        }

        private static void NotifyLevelSelectSceneLoaded()
        {
            Debug.Log("Se ha cargado la escena de seleccion de niveles");
        }

        private static void NotifyLevelSceneLoaded()
        {
            Debug.Log("Se ha cargado la escena del nivel");
        }

        private static void NotifyGameDataSaved()
        {
            Debug.Log("Se ha guardado la partida en " + SaveSystem.PATH);
        }

        private static void NotifyGameDataLoaded()
        {
            Debug.Log("Se han cargado los datos de la partida en " + SaveSystem.PATH);
        }
        #endregion

        #region TitleScreen
        private static void NotifyPageEnabled()
        {
            Debug.Log("Se ha activado una página del canvas.");
        }

        private static void NotifyPageDisabled()
        {
            Debug.Log("Se ha desactivado una página del canvas.");
        }

        private static void NotifyButtonPlayClicked()
        {
            Debug.Log("Se ha pulsado el botón 'Jugar'.");
        }

        private static void NotifyButtonStatsClicked()
        {
            Debug.Log("Se ha pulsado el botón 'Estadisticas'.");
        }

        private static void NotifyButtonConfigClicked()
        {
            Debug.Log("Se ha pulsado el botón 'Configuracion'.");
        }

        private static void NotifyButtonInfoClicked()
        {
            Debug.Log("Se ha pulsado el botón 'Informacion'.");
        }

        private static void NotifyButtonBackClicked()
        {
            Debug.Log("Se ha pulsando el botón 'Volver'.");
        }

        private static void NotifyButtonLanguageClicked()
        {
            Debug.Log("Se ha pulsado el botón 'Idioma'.");
        }

        private static void NotifyButtonOkClicked()
        {
            Debug.Log("Se ha pulsado el botón 'Ok'.");
        }

        private static void NotifyTitleAnimationFinished()
        {
            Debug.Log("Ha finalizado la animación del logo.");
        }
        #endregion

        #region LevelSelection
        private static void NotifyButtonLevelStatsClicked()
        {
            Debug.Log("Se ha pulsado el botón de estadísticas del nivel " +
                GameManager.Instance.SelectedWorldIndex + "-" +
                GameManager.Instance.SelectedLevelIndex);
        }

        private static void NotifyButtonRankingClicked()
        {
            Debug.Log("Se ha pulsado el botón del Ranking para el nivel " +
                GameManager.Instance.SelectedWorldIndex + "-" +
                GameManager.Instance.SelectedLevelIndex);
        }

        private static void NotifyLevelBlockedClicked(int levelIndex)
        {
            Debug.Log("Se ha pulsado un nivel bloqueado: Nivel " +
                GameManager.Instance.SelectedWorldIndex + "-" +
                levelIndex);
        }

        private static void NotifyLevelEnabledClicked(int levelIndex)
        {
            Debug.Log("Se ha pulsado un nivel habilitado: Nivel " +
                GameManager.Instance.SelectedWorldIndex + "-" +
                levelIndex);
        }

        private static void NotifyArrowButtonClicked(Direction dir)
        {
            Debug.Log("Pusado el botón arrow " + dir);
        }

        private static void NotifyButtonBackClicked(LSMState state)
        {
            Debug.Log("Se ha pulsado el botón 'volver'.");
        }
        #endregion

        #region Level
        private static void NotifyTapButton()
        {
            Debug.Log("Se ha pulsado el botón 'tap'.");
        }

        private static void NofityGraivityButton(bool enabled)
        {
            Debug.Log("Se ha pulsado el botón 'cambiar gravedad'.");
        }

        private static void NotifyButtonMenuClicked()
        {
            Debug.Log("Se ha pulsado el botón 'menu'.");
        }

        private static void NotifyButtonReplayClicked()
        {
            Debug.Log("Se ha pulsado el boton 'replay'.");
        }

        private static void NotifyButtonResumeClicked()
        {
            Debug.Log("Se ha pulsado el botón 'continuar'.");
        }

        private static void NotifyButtonOK()
        {
            Debug.Log("Se ha pulsado el botón 'Ok'.");
        }

        private static void NotifyMultiplierUpdate()
        {
            Debug.Log("Se ha actualizado el valor del multiplicador -> x" + LM.Data.Multiplier);
        }

        private static void NotifyScoreChanged()
        {
            Debug.Log("Se ha actualizado el valor del score -> " + LM.Data.Score);
        }

        private static void NotifyGamePaused()
        {
            Debug.Log("Se ha pausado la partida.");
        }

        private static void NotifyDeath()
        {
            Debug.Log("El jugador ha muerto");
        }

        private static void NotifyGameOver()
        {
            Debug.Log("GameOver completado.");
        }

        private static void NotifyInfiniteGameOver()
        {
            Debug.Log("Se ha terminado la partida en un nivel infinito");
        }

        private static void NotifyLevelStart()
        {
            Debug.Log("Ha comenzado el nivel " + GM.SelectedWorldIndex + "-" +
                GM.SelectedLevelIndex);
        }

        private static void NotifyLevelCompleted()
        {
            Debug.Log("Nivel completado...");
        }

        private static void NotifyLevelFragmentTrigger()
        {
            Debug.Log("Disparador de LevelFragment");
        }

        private static void NotifySecondParsed()
        {
            //Debug.Log("Ha pasado un segundo -> " + Time.timeSinceLevelLoad);
        }

        private static void NotifyCompletedLevelPageAnimFinished()
        {
            Debug.Log("Ha finalizado la animación de la página de nivel completado.");
        }

        private static void NotifyIntiniteAttemptPageAnimFinished()
        {
            Debug.Log("Ha finalizado la animación de la página de intento de nivel infinito.");
        }

        private static void NotifySecretDiamondObtained(int id)
        {
            Debug.Log("Se ha obtenido el diamante " + id);
        }

        private static void NotifyTutorialTriggered(TutorialTags tutorial)
        {
            Debug.Log("Se ha activado el tutorial " + tutorial);
        }

        private static void NotifyTutorialEnded(TutorialTags tutorial)
        {
            Debug.Log("Ha finalizado el tutorial " + tutorial);
        }

        private static void NotifyPlayerCollision(string msg)
        {
            Debug.Log(msg);
        }
        #endregion
        #endregion

        /// <summary>
        /// Se suscribe a los eventos de TitleScreenManager
        /// </summary>
        private static void SubscribeTitleScreenScene()
        {
            TSM = TitleScreenManager.Instance;

            if (enabledDebugUI)
                InstantiateDebugGO();

            if (!titlescreenSubscribed)
            {
                Debug.Log("Suscribiendose a eventos de TitleScreen...");
                TSM.OnUIPageEnabled += NotifyPageEnabled;
                TSM.OnUIPageDisabled += NotifyPageDisabled;
                TSM.OnButtonPlayClicked += NotifyButtonPlayClicked;
                TSM.OnButtonStatsClicked += NotifyButtonStatsClicked;
                TSM.OnButtonConfigClicked += NotifyButtonConfigClicked;
                TSM.OnButtonInfoClicked += NotifyButtonInfoClicked;
                TSM.OnButtonBackClicked += NotifyButtonBackClicked;
                TSM.OnButtonLanguageClicked += NotifyButtonLanguageClicked;
                TSM.OnButtonOkClicked += NotifyButtonOkClicked;
                TSM.OnTitleAnimationFinished += NotifyTitleAnimationFinished;
                Debug.Log("Eventos de TitleScreen suscritos.");
                titlescreenSubscribed = true;
            }
        }

        /// <summary>
        /// Se suscribe a los eventos de LevelSelectionManager
        /// </summary>
        private static void SubscribeLevelSelectScene()
        {
            LSM = LevelSelectionManager.Instance;

            if (enabledDebugUI)
                InstantiateDebugGO();

            if (!levelSelectionSusbscribed)
            {
                Debug.Log("Suscribiendose a eventos de LevelSelection...");
                LSM.OnPlayButtonClicked += NotifyButtonPlayClicked;
                LSM.OnButtonLevelStatsClicked += NotifyButtonLevelStatsClicked;
                LSM.OnButtonRankingClicked += NotifyButtonRankingClicked;
                LSM.OnLevelBlockedClick += NotifyLevelBlockedClicked;
                LSM.OnLevelEnabledClick += NotifyLevelEnabledClicked;
                LSM.OnArrowButtonClicked += NotifyArrowButtonClicked;
                LSM.OnButtonBackClicked += NotifyButtonBackClicked;
                Debug.Log("Eventos suscritos.");

                levelSelectionSusbscribed = true;
            }
        }

        /// <summary>
        /// Se suscribe a los eventos de LevelManager
        /// </summary>
        private static void SubscribeLevelScene()
        {
            LM = LevelManager.Instance;

            if (enabledDebugUI)
                InstantiateDebugGO();

            if (!levelSuscribed)
            {
                Debug.Log("Suscribiendose a eventos de Level...");
                LM.OnTapButton += NotifyTapButton;
                LM.OnGravityButton += NofityGraivityButton;
                LM.OnButtonRankingClicked += NotifyButtonRankingClicked;
                LM.OnButtonMenuClicked += NotifyButtonMenuClicked;
                LM.OnButtonReplayClicked += NotifyButtonReplayClicked;
                LM.OnButtonResumeClicked += NotifyButtonResumeClicked;
                LM.OnButtonOK += NotifyButtonOkClicked;
                LM.OnMultiplierUpdate += NotifyMultiplierUpdate;
                LM.OnScoreChanged += NotifyScoreChanged;
                LM.OnGamePaused += NotifyGamePaused;
                LM.OnDeath += NotifyDeath;
                LM.OnGameOver += NotifyGameOver;
                LM.OnInfiniteGameOver += NotifyInfiniteGameOver;
                LM.OnLevelStart += NotifyLevelStart;
                LM.OnLevelCompleted += NotifyLevelCompleted;
                LM.OnLevelFragmentTrigger += NotifyLevelFragmentTrigger;
                LM.OnSecondParsed += NotifySecondParsed;
                LM.OnCompletedLevelPageAnimFinished += NotifyCompletedLevelPageAnimFinished;
                LM.OnInfiniteAttemptPageAnimationFinished += NotifyIntiniteAttemptPageAnimFinished;
                LM.OnTutorialTriggered += NotifyTutorialTriggered;
                LM.OnTutorialEnded += NotifyTutorialEnded;
                LM.OnPlayerCollision += NotifyPlayerCollision;
                Debug.Log("Eventos suscritos.");
                levelSuscribed = true;
            }

        }

        /// <summary>
        /// Instancia el GameObject con la consola y el display con los fps y la memoria ram en uso.
        /// </summary>
        private static void InstantiateDebugGO()
        {
            Instantiate(Resources.Load("prefabs/debug/debug_go") as GameObject).GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
}