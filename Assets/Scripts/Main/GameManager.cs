using GravityBat_Localization;
using GravityBat_Constants;
using GravityBat_Audio;
using GravityBat_Data;
using GravityBat_Debug;
using GPGServices;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Utiliza el patron singleton.
/// Controla el flujo general de la aplicacion.
/// Contiene eventos a los que se suscriben los controladores especificos de cada escena. Los eventos
/// son lanzados desde este controlador hacia el resto.
/// </summary>
public class GameManager : ScriptableObject
{
    #region Singleton
    //Constructor privado
    protected GameManager() { }

    private static GameManager instance = null; //instancia static de la clase

    //Atributo publico de la instancia con el get, siguiendo el patron Singleton
    public static GameManager Instance
    {
        get
        {
            /*
             * Nota: No puede utilizarse new para instanciar el objeto. Este debe estar asociado a un componente (que no es el caso)
             * o heredar de la clase ScriptableObject para poder ser instanciado utilzando el metodo CreateInstance.
             */
            if (GameManager.instance == null)
            {
                GameManager.instance = ScriptableObject.CreateInstance<GameManager>(); //Crea la instancia -> similar a "new"
                //Evita que la instancia se destruya en nuevas scenes
                DontDestroyOnLoad(GameManager.instance);
            }
            return GameManager.instance;
        }
    }
    #endregion

    #region Attributes
    private int levelIndex;
    private int worldIndex;
    private bool authenticated;
    private GameState gameState;
    public AudioManager AudioM;
    private GameData data;

    public int SelectedLevelIndex
    {
        get { return levelIndex; }
        set { levelIndex = value; }
    }

    public int SelectedWorldIndex
    {
        get { return worldIndex; }
        set { worldIndex = value; }
    }

    public GameState GameState
    {
        get { return gameState; }
        set
        {
            this.gameState = value;
            Raise_GameStateChanged();
        }
    }

    public GameData Data
    {
        get { return data; }
    }

    public bool Authenticated
    {
        get { return authenticated; }
    }
    #endregion

    #region Events
    public delegate void OnGameStateChangedHandler();    
    public delegate void OnFadeCompletedHandler();
    public delegate void OnLanguageEventHandler();
    public delegate void OnSceneLoadedHandler();
    public delegate void OnTextLoadedToMemoryHandler();
    public delegate void OnLevelsStateChangedHandler(int worldIndex, int levelIndex);
    public delegate void OnUserConfigChangedHandler();
    public delegate void OnSaveDataEventHandler();
    public delegate void OnAndroidInputHandler();
    public delegate void OnDebugStateHandler(bool enabled);
    public delegate void OnGPGLoginRequestHandler();
    public delegate void OnGPGPostScoreRequestHandler(long score, int worldIndex, int levelIndex);
    public delegate void OnGPGOpenLadderboardRequestHandler(int worldIndex, int levelIndex);
    public delegate void OnTutorialToggleHandler(bool enabled);

    //Eventos
    public event OnGameStateChangedHandler OnGameStateChanged;
    public event OnFadeCompletedHandler OnFadeCompleted;
    public event OnLanguageEventHandler OnLanguageChanged;
    public event OnLanguageEventHandler OnStringsLanguageUpdated;
    public event OnSceneLoadedHandler OnTitleScreenSceneLoaded;
    public event OnSceneLoadedHandler OnLevelSelectSceneLoaded;
    public event OnSceneLoadedHandler OnLevelSceneLoaded;
    public event OnTextLoadedToMemoryHandler OnTextLoadedToMemory;
    public event OnTextLoadedToMemoryHandler OnDataTextLoadedToMemory;
    public event OnTextLoadedToMemoryHandler OnTutorialTextLoadedToMemory;
    public event OnLevelsStateChangedHandler OnLevelCompleted;
    public event OnLevelsStateChangedHandler OnLevelUnlocked;
    public event OnLevelsStateChangedHandler OnSpecialLevelUnlocked;
    public event OnLevelsStateChangedHandler OnWorldUnlocked;
    public event OnUserConfigChangedHandler OnUserConfigChanged;
    public event OnSaveDataEventHandler OnRequestDataSave;      //UnUsed
    public event OnSaveDataEventHandler OnGameDataSaved;        //UnUsed
    public event OnSaveDataEventHandler OnGameDataLoaded;       //Unused
    public event OnAndroidInputHandler OnAndroidBackPressed;
    public event OnDebugStateHandler OnDebugUIEnabled;
    public event OnGPGLoginRequestHandler OnGPGLoginRequest;
    public event OnGPGPostScoreRequestHandler OnGPGPostScoreRequest;
    public event OnGPGOpenLadderboardRequestHandler OnGPGOpenLadderboardRequest;
    public event OnTutorialToggleHandler OnTutorialToggle;
    public event OnTutorialToggleHandler OnSwapControlsToggle;
    #endregion

    /// <summary>
    /// Inicializa debug, Google Play Services, AudioManager y la partida guardada.
    /// Se suscribe a eventos
    /// </summary>
    private void OnEnable()
    {
        Application.targetFrameRate = 60;
        _Debug.Init(this);                  
        InitGooglePlayGamesServices();      
        AudioM = AudioManager.Instance;
        InitSavedGame();

        worldIndex = 0;
        levelIndex = 0;
        this.OnLanguageChanged += HandleLanguageChanged;
        this.OnFadeCompleted += HandleFadeCompleted;
        SceneManager.sceneLoaded += HandleSceneLoaded;
        this.OnTitleScreenSceneLoaded += HandleTitleScreenSceneLoaded;
        this.OnLevelSelectSceneLoaded += HandleOnLevelSelectionSceneLoaded;
        this.OnLevelSceneLoaded += HandleOnLevelSceneLoaded;
        this.OnLevelCompleted += HandleLevelCompleted;
        this.OnLevelUnlocked += HandleLevelUnlocked;
        this.OnWorldUnlocked += HandleWorldUnlocked;
        this.OnRequestDataSave += HandleRequestDataSave;
        this.OnGPGLoginRequest += HandleRequestGPGLogin;
        this.OnGPGPostScoreRequest += HandleRequestGPGPostScore;
        this.OnGPGOpenLadderboardRequest += HandleRequestGPGOpenLadderboard;
    }

    /// <summary>
    /// Se desuscribe de eventos.
    /// </summary>
    private void OnDisable()
    {
        this.OnLanguageChanged -= HandleLanguageChanged;
        this.OnFadeCompleted -= HandleFadeCompleted;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
        this.OnTitleScreenSceneLoaded -= HandleTitleScreenSceneLoaded;
        this.OnLevelSelectSceneLoaded -= HandleOnLevelSelectionSceneLoaded;
        this.OnLevelSceneLoaded -= HandleOnLevelSceneLoaded;
        this.OnLevelCompleted -= HandleLevelCompleted;
        this.OnLevelUnlocked -= HandleLevelUnlocked;
        this.OnWorldUnlocked -= HandleWorldUnlocked;
        this.OnRequestDataSave -= HandleRequestDataSave;
        this.OnGPGLoginRequest -= HandleRequestGPGLogin;
        this.OnGPGPostScoreRequest -= HandleRequestGPGPostScore;
        this.OnGPGOpenLadderboardRequest -= HandleRequestGPGOpenLadderboard;
    }

    private void OnApplicationQuit()
    {
        Raise_RequestDataSave();
        GameManager.instance = null;
    }

    #region Event Handlers
    /// <summary>
    /// Recarga los textos en memoria
    /// </summary>
    private void HandleLanguageChanged()
    {
        LoadTexts();
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Scene_TitleScreen":
                Raise_TitleScreenSceneLoaded();
                break;

            case "Scene_LevelSelection":
                Raise_LevelSelectionSceneLoaded();
                break;

            case "Scene_Level":
                Raise_LevelSceneLoaded();
                break;
        }
    }

    private void HandleTitleScreenSceneLoaded()
    {
        Strings.LoadTitleScreenTexts();
        Strings.LoadGlobalDataTexts();
        AudioM.Raise_TitleScreenSceneLoaded();
    }

    private void HandleOnLevelSelectionSceneLoaded()
    {
        Strings.LoadLevelSelectionTexts();
        Strings.LoadLevelDataTexts(0, 0);
        AudioM.Raise_LevelSelectionSceneLoaded();
    }

    private void HandleOnLevelSceneLoaded()
    {
        Strings.LoadLevelTexts();
        AudioM.Raise_LevelSceneLoaded();
    }

    private void HandleFadeCompleted()
    {
        LoadScene();
    }

    private void HandleLevelCompleted(int worldIndex, int levelIndex)
    {
        if (levelIndex < 4)
        {
            data.LevelArray[worldIndex, levelIndex].Completed = true;

            if (levelIndex < 2)
                 Raise_LevelUnlocked(worldIndex, levelIndex + 1);
            else
            {
                Raise_WorldUnlocked(worldIndex + 1, 0);
                Raise_LevelUnlocked(worldIndex, levelIndex + 1);
            }

            if (IsSpecialLevelUnlockable(worldIndex))
                Raise_LevelUnlocked(worldIndex, 4);
        }
    }

    private void HandleLevelUnlocked(int worldIndex, int levelIndex)
    {
        data.LevelArray[worldIndex, levelIndex].Unlocked = true;
    }

    private void HandleSpecialLevelUnlocked(int worldIndex, int levelIndex)
    {
        
    }

    private void HandleWorldUnlocked(int worldIndex, int levelIndex)
    {
        data.LevelArray[worldIndex, levelIndex].Unlocked = true;
    }

    private void HandleRequestDataSave()
    {
        SaveSystem.Save();
        Raise_GameDataSaved();
    }

    private void HandleRequestGPGLogin()
    {
        GPGLogin();
    }

    private void HandleRequestGPGPostScore(long score, int worldIndex, int levelIndex)
    {
        GPGPostScore(score, worldIndex, levelIndex);
    }

    private void HandleRequestGPGOpenLadderboard(int worldIndex, int levelIndex)
    {
        GPGOpenLadderboard(worldIndex, levelIndex);
    }
    #endregion

    #region Raise Events
    private void Raise_GameStateChanged()
    {
        if (OnGameStateChanged != null)
            OnGameStateChanged();
    }

    public void Raise_FadeCompleted()
    {
        if (OnFadeCompleted != null)
            OnFadeCompleted();  
    }

    public void Raise_LanguageChanged()
    {
        if (OnLanguageChanged != null)
            OnLanguageChanged();
    }

    public void Raise_StringsLanguageUpdated()
    {
        if (OnStringsLanguageUpdated != null)
            OnStringsLanguageUpdated();
    }

    public void Raise_TitleScreenSceneLoaded()
    {
        if (OnTitleScreenSceneLoaded != null)
            OnTitleScreenSceneLoaded();
    }

    public void Raise_LevelSelectionSceneLoaded()
    {
        if (OnLevelSelectSceneLoaded != null)
            OnLevelSelectSceneLoaded();
    }

    public void Raise_LevelSceneLoaded()
    {
        if (OnLevelSceneLoaded != null)
            OnLevelSceneLoaded();
    }

    public void Raise_TextLoadedToMemory()
    {
        if (OnTextLoadedToMemory != null)
            OnTextLoadedToMemory();
    }

    public void Raise_DataTextLoadedToMemory()
    {
        if (OnDataTextLoadedToMemory != null)
            OnDataTextLoadedToMemory();
    }

    public void Raise_TutorialTextLoadedToMemory()
    {
        if (OnTutorialTextLoadedToMemory != null)
            OnTutorialTextLoadedToMemory();
    }

    public void Raise_LevelCompleted(int worldIndex, int levelIndex)
    {
        if (OnLevelCompleted != null)
            OnLevelCompleted(worldIndex, levelIndex);
    }

    public void Raise_LevelUnlocked(int worldIndex, int levelIndex)
    {
        if (OnLevelUnlocked != null)
            OnLevelUnlocked(worldIndex, levelIndex);
    }

    public void Raise_SpecialLevelUnlocked(int worldIndex, int levelIndex)
    {
        if (OnSpecialLevelUnlocked != null)
            OnSpecialLevelUnlocked(worldIndex, levelIndex);
    }

    public void Raise_WorldUnlocked(int worldIndex, int levelIndex)
    {
        if (OnWorldUnlocked != null)
            OnWorldUnlocked(worldIndex, levelIndex);
    }

    public void Raise_UserConfigChanged()
    {
        if (OnUserConfigChanged != null)
            OnUserConfigChanged();
    }

    public void Raise_GameDataSaved()
    {
        if (OnGameDataSaved != null)
            OnGameDataSaved();
    }

    public void Raise_GameDataLoaded()
    {
        if (OnGameDataLoaded != null)
            OnGameDataLoaded();
    }

    public void Raise_RequestDataSave()
    {
        if (OnRequestDataSave != null)
            OnRequestDataSave();
    }

    public void Raise_AndroidBackPressed()
    {
        if (OnAndroidBackPressed != null)
            OnAndroidBackPressed();
    }

    public void Raise_DebugUIEnabled(bool enabled)
    {
        if (OnDebugUIEnabled != null)
            OnDebugUIEnabled(enabled);
    }

    public void Raise_RequestGPGLogin()
    {
        if (OnGPGLoginRequest != null)
            OnGPGLoginRequest();
    }

    public void Raise_RequestGPGPostScore(long score, int worldIndex, int levelIndex)
    {
        if (OnGPGPostScoreRequest != null)
            OnGPGPostScoreRequest(score, worldIndex, levelIndex);
    }

    public void Raise_RequestGPGOpenLadderboard(int worldIndex, int levelIndex)
    {
        if (OnGPGOpenLadderboardRequest != null)
            OnGPGOpenLadderboardRequest(worldIndex, levelIndex);
    }

    public void Raise_TutorialToggle(bool enabled)
    {
        if (OnTutorialToggle != null)
            OnTutorialToggle(enabled);
    }

    public void Raise_SwapControlsToggle(bool enabled)
    {
        if (OnSwapControlsToggle != null)
            OnSwapControlsToggle(enabled);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Carga una escena
    /// </summary>
    public void LoadScene()
    {
        switch (gameState)
        {
            case GameState.TITLE_SCREEN:
                SceneManager.LoadScene(0);
                break;

            case GameState.LEVEL_SELECTION_MENU:
                SceneManager.LoadScene(1);
                break;

            case GameState.LEVEL:
                SceneManager.LoadScene(2 + worldIndex);
                break;
        }
    }

    /// <summary>
    /// Inicializa el juego con los datos guardados
    /// </summary>
    private void InitSavedGame()
    {
        SaveData data = SaveSystem.Load();
        this.data = new GameData();
        this.data.LevelArray = data.LevelArray;

        this.data.Language = data.Language;
        this.data.MusicVolume = data.MusicVolume;
        this.data.SfxVolume = data.SfxVolume;
        this.data.GameButtonOpacity = data.GameButtonOpacity;
        this.data.AutoRetry = data.AutoRetry;
        this.data.Music = data.Music;
        this.data.Sfx = data.Sfx;
        this.data.ShowLevelPanels = data.ShowLevelPanels;
        this.data.SwapLevelPanels = data.SwapLevelPanels;
        this.data.EnableTutorials = data.EnableTutorials;
        this.data.IsWelcomeMessageDone = data.IsWelcomeMessageDone;

        this.data.PID_A = GameData.GeneratePID();
        this.data.PID_B = GameData.GeneratePID();

        Raise_GameDataLoaded();
    }

    /// <summary>
    /// Autentica al usuario dentro de Google Play Games
    /// </summary>
    private void InitGooglePlayGamesServices()
    {
        authenticated = PGServices.AuthenticateUser();
        authenticated = true;
    }

    /// <summary>
    /// Carga/recarga los textos necesarios en memoria
    /// </summary>
    private void LoadTexts()
    {
        switch (gameState)
        {
            case GameState.TITLE_SCREEN:
                Strings.LoadTitleScreenTexts();
                Strings.LoadGlobalDataTexts();
                break;

            case GameState.LEVEL_SELECTION_MENU:
                Strings.LoadLevelSelectionTexts();
                break;

            case GameState.LEVEL:
                Strings.LoadLevelTexts();
                break;
        }
    }

    /// <summary>
    /// Guarda la partida y eleva el evento de partida guardada.
    /// </summary>
    private void SaveGame()
    {
        SaveSystem.Save();
        Raise_GameDataSaved();
    }

    private void GPGLogin()
    {
        PGServices.AuthenticateUser();
    }

    private void GPGPostScore(long score, int worldIndex, int levelIndex)
    {
        Debug.LogWarning("Score: "+score+" WorldIndex: "+worldIndex+" LevelIndex: "+levelIndex);
        PGServices.PostScoreToLadderboard(score, worldIndex, levelIndex);
    }

    private void GPGOpenLadderboard(int worldIndex, int levelIndex)
    {
        Debug.LogWarning(" WorldIndex: " + worldIndex + " LevelIndex: " + levelIndex);
        PGServices.ShowLadderboard(worldIndex, levelIndex);
    }

    private bool IsSpecialLevelUnlockable(int worldIndex)
    {
        for (int i=0; i<2; i++)
            if (data.LevelArray[worldIndex, i].DiamondsCount() != 3)
                return false;

        return true;
    }
    #endregion

}
