using UnityEngine;
using GravityBat_Data;
using GravityBat_Constants;
using GravityBat_Localization;

/// <summary>
/// Controlador encargado de llevar a cabo la gestion de un nivel.
/// Contiene una referencia a la instancia del GameManager y se suscribe a sus eventos.
/// Contiene eventos a los que se suscriben a su vez los distintos componentes del nivel.
/// Sigue el patron Singleton.
/// Extiende de ScriptableObject.
/// </summary>
public class LevelManager : ScriptableObject
{
    #region Singleton
    //Solo habra una instancia de Levelmanager al mismo tiempo
    private static LevelManager instance = null;

    //Constructor privado
    private LevelManager() { }
    
    public static LevelManager Instance
    {
        get
        {
            if (LevelManager.instance == null)
            {
                LevelManager.instance = ScriptableObject.CreateInstance<LevelManager>();
            }
            return instance;
        }
    }
    #endregion

    #region Attributes
    private GameManager GM;
    private AttemptData data;

    private float lastTimeScale;
    private bool isInitPause;
    private bool isInfinite;
    private bool isGravityEnabled;
    private LevelState state;
    #endregion

    #region Properties
    public AttemptData Data
    {
        get { return data; }
    }

    public LevelState State
    {
        get { return state; }
        set { state = value; }
    }

    public bool IsInfinite
    {
        get { return isInfinite; }
    }

    public bool GravityEnabled
    {
        get { return isGravityEnabled; }
        set { isGravityEnabled = value; }
    }

    public int WorldIndex
    {
        get { return GM.SelectedWorldIndex; }
        set { GM.SelectedWorldIndex = value; }
    }

    public int LevelIndex
    {
        get { return GM.SelectedLevelIndex; }
        set { GM.SelectedLevelIndex = value; }
    }
    #endregion

    #region Constants
    /// <summary>
    /// Vector de gravedad ascendente.
    /// </summary>
    private Vector3 GRAVITY_UP = new Vector3(0, - Constants.GRAVITY_FORCE_Y, 0);   

    /// <summary>
    /// Vector de gravedad descendente.
    /// </summary>
    private Vector3 GRAVITY_DOWN = new Vector3(0, Constants.GRAVITY_FORCE_Y, 0);
    #endregion

    #region Events
    public delegate void OnTapPanelClickedHandler();
    public delegate void OnGravityPanelClickedHandler(bool gravityEnabled);
    public delegate void OnButtonClickedHandler();
    public delegate void OnUIUpdateHandler();
    public delegate void OnPauseHandler();
    public delegate void OnDeathHandler();
    public delegate void OnGameOverHandler();
    public delegate void OnLevelTriggerHandler();
    public delegate void OnParallaxChangeTriggerHandler(Biome biome);
    public delegate void OnParseSecondHandler();
    public delegate void OnCompletedPageAnimFinishedHandler();
    public delegate void OnSecreDiamondObtainedHandler(int id);
    public delegate void OnTutorialTriggerHandler(TutorialTags tag);
    public delegate void OnPlayerCollisionHandler(string msg);
    public delegate void OnTutorialTextLoadHandler();
    public delegate void OnItemObtainedHandler(int baseScore);
    public delegate void OnItemDiamondObtainedHandler(int baseScore, int id);
    public delegate void OnItemAbilityFruitObtainedHandler(int baseScore, Abilities ability);
    public delegate void OnItemAnimationFinishedHandler(int realScore, Vector3 position);
    public delegate void OnTextPopUpTriggerHandler();
    public delegate void OnAbilityAcquiredHandler(Abilities ability);

    public event OnTapPanelClickedHandler OnTapButton;              //Boton para hacer volar al murcielago
    public event OnGravityPanelClickedHandler OnGravityButton;      //Boton para cambiar la gravedad
    public event OnButtonClickedHandler OnBlockChangerClick;            //Boton para realizar acciones especiales
    public event OnButtonClickedHandler OnButtonRankingClicked;
    public event OnButtonClickedHandler OnButtonMenuClicked;
    public event OnButtonClickedHandler OnButtonReplayClicked;
    public event OnButtonClickedHandler OnButtonResumeClicked;
    public event OnButtonClickedHandler OnButtonOK;
    public event OnButtonClickedHandler OnButtonBackClicked;
    public event OnButtonClickedHandler OnButtonShareClicked;
    public event OnUIUpdateHandler OnMultiplierUpdate;              //Se ha actualizado el valor del mutiplier
    public event OnUIUpdateHandler OnMultiplierTimeFinished;
    public event OnUIUpdateHandler OnScoreChanged;                  //Se actualiza el valor del score
    public event OnPauseHandler OnGamePaused;                            //Se ha pausado la partida
    public event OnDeathHandler OnDeath;                            //El jugador ha muerto
    public event OnGameOverHandler OnGameOver;                      //Se produce el GameOver
    public event OnGameOverHandler OnInfiniteGameOver;              //Game Over en el nivel infinito
    public event OnLevelTriggerHandler OnLevelStart;                //Comienza el nivel
    public event OnLevelTriggerHandler OnLevelCompleted;            //Termina el nivel
    public event OnLevelTriggerHandler OnLevelFragmentTrigger;      //Se debe cargar un nuevo fragmento de nivel
    public event OnParallaxChangeTriggerHandler OnParallaxChangeTrigger;
    public event OnParseSecondHandler OnSecondParsed;               //Ha pasado un segundo
    public event OnCompletedPageAnimFinishedHandler OnCompletedLevelPageAnimFinished;
    public event OnCompletedPageAnimFinishedHandler OnInfiniteAttemptPageAnimationFinished;
    public event OnTutorialTriggerHandler OnTutorialTriggered;
    public event OnTutorialTriggerHandler OnTutorialEnded;
    public event OnPlayerCollisionHandler OnPlayerCollision;
    public event OnTutorialTextLoadHandler OnTutorialTextLoad;
    public event OnItemObtainedHandler OnCherryObtained;
    public event OnItemObtainedHandler OnGravityGemObtained;
    public event OnItemDiamondObtainedHandler OnDiamondObtained;
    public event OnItemAbilityFruitObtainedHandler OnAbilityFruitObtained;
    public event OnItemAnimationFinishedHandler OnItemAnimationFinished;
    public event OnTextPopUpTriggerHandler OnReadyTextTrigger;
    public event OnTextPopUpTriggerHandler OnStartTextTrigger;
    public event OnAbilityAcquiredHandler OnAbilityAcquired;
    #endregion

    /// <summary>
    /// Llamado al instanciar el ScriptableObject
    /// </summary>
    private void Awake()
    {
        GM = GameManager.Instance;  
    }

    /// <summary>
    /// Suscribe los eventos
    /// </summary>
    private void OnEnable()
    {
        GM.OnAndroidBackPressed += HandleAndroidBackPressed;
        this.OnTapButton += HandleTapButton;
        this.OnGravityButton += HandleOnGravityButton;
        this.OnButtonRankingClicked += HandleRankingButtonClick;
        this.OnButtonMenuClicked += HandleMenuButtonClick;
        this.OnButtonReplayClicked += HandleReplayButtonClick;
        this.OnButtonResumeClicked += HandleResumeButtonClick;
        this.OnGamePaused += HandleGamePaused;
        this.OnDeath += HandleOnDeath;
        this.OnGameOver += HandleOnGameOver;
        this.OnInfiniteGameOver += HandleOnInfiniteGameOver;
        this.OnLevelStart += HandleOnLevelStart;
        this.OnLevelCompleted += HandleOnLevelCompleted;
        this.OnMultiplierTimeFinished += HandleMultiplierTimeFinished;
        this.OnSecondParsed += HandleOnParseSecond;
        this.OnTutorialTriggered += HandleTutorialTriggered;
        this.OnTutorialEnded += HandleTutorialEnded;
        this.OnCherryObtained += HandleCherryObtained;
        this.OnGravityGemObtained += HandleGravityGemObtained;
        this.OnDiamondObtained += HandleDiamondObtained;
        this.OnAbilityFruitObtained += HandleAbilityFruitObtained;
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        GM.OnAndroidBackPressed -= HandleAndroidBackPressed;
        this.OnTapButton -= HandleTapButton;
        this.OnGravityButton -= HandleOnGravityButton;
        this.OnButtonRankingClicked -= HandleRankingButtonClick;
        this.OnButtonMenuClicked -= HandleMenuButtonClick;
        this.OnButtonReplayClicked -= HandleReplayButtonClick;
        this.OnButtonResumeClicked -= HandleResumeButtonClick;
        this.OnGamePaused -= HandleGamePaused;
        this.OnDeath -= HandleOnDeath;
        this.OnGameOver -= HandleOnGameOver;
        this.OnInfiniteGameOver -= HandleOnInfiniteGameOver;
        this.OnLevelStart -= HandleOnLevelStart;
        this.OnLevelCompleted -= HandleOnLevelCompleted;
        this.OnMultiplierTimeFinished -= HandleMultiplierTimeFinished;
        this.OnSecondParsed -= HandleOnParseSecond;
        this.OnTutorialTriggered -= HandleTutorialTriggered;
        this.OnTutorialEnded -= HandleTutorialEnded;
        this.OnCherryObtained -= HandleCherryObtained;
        this.OnGravityGemObtained -= HandleGravityGemObtained;
        this.OnDiamondObtained -= HandleDiamondObtained;
        this.OnAbilityFruitObtained -= HandleAbilityFruitObtained;
    }

    #region Event Handlers
    private void HandleTapButton()
    {
        data.Taps++;
        GM.AudioM.Raise_PlayerFlap();
    }

    private void HandleOnGravityButton(bool gravityEnabled)
    {
        SwapGravity(gravityEnabled);
        GM.AudioM.Raise_PlayerGravitySwap();
    }

    private void HandleRankingButtonClick()
    {
        GM.Raise_RequestGPGOpenLadderboard(WorldIndex, LevelIndex);
    }

    private void HandleMenuButtonClick()
    {
        if (state == LevelState.GAME_PAUSED)
            ResumeGame();
        GM.GameState = GameState.LEVEL_SELECTION_MENU;
    }

    private void HandleReplayButtonClick()
    {
        ReloadLevel();
    }

    private void HandleResumeButtonClick()
    {
        ResumeGame();
        GM.AudioM.Raise_ResumedLevel();
    }

    private void HandleGamePaused()
    {
        PauseGame();
        GM.AudioM.Raise_PausedLevel();
    }

    private void HandleOnDeath()
    {
        state = LevelState.DEAD;
        GM.AudioM.Raise_PlayerDeath();
    }

    private void HandleOnGameOver()
    {
        state = LevelState.GAME_OVER;
        UpdateAndSaveLevelData(false);
    }

    private void HandleOnInfiniteGameOver()
    {
        state = LevelState.GAME_OVER;
        UpdateAndSaveLevelData(true);
    }

    private void HandleOnLevelStart()
    {
        state = LevelState.RUNNING;

        Raise_StartTextTrigger();
    }

    private void HandleOnLevelCompleted()
    {
        if (!GravityEnabled)
        {
            data.Multiplier -= 1;
            Raise_GravityButtonClick();
        }

        state = LevelState.COMPLETED;
        GM.Raise_LevelCompleted(GM.SelectedWorldIndex, GM.SelectedLevelIndex);
        UpdateAndSaveLevelData(true);
        GM.Raise_RequestGPGPostScore(data.Score, WorldIndex, LevelIndex);
    }

    private void HandleMultiplierTimeFinished()
    {
        ResetMultiplier();
    }

    private void HandleOnParseSecond()
    {
        AddGravityPower();
    }

    private void HandleTutorialTriggered(TutorialTags tag)
    {
        TutorialPauseFreeze();
    }

    private void HandleTutorialEnded(TutorialTags tag)
    {
        ResumeGame();
        UpdateTutorialFlags(tag);

        if (tag == TutorialTags.SWAP_CONSECUENCES)
        {
            Raise_GravityButtonClick();
        }
    }

    private void HandleAndroidBackPressed()
    {
        Raise_ButtonBackClicled();
    }

    private void HandleCherryObtained(int baseScore)
    {
        AddScoreWithMultiplier(baseScore);
    }

    private void HandleGravityGemObtained(int baseScore)
    {
        data.GravityPower += Constants.GRAVITY_CHUNK;
        AddScoreWithMultiplier(baseScore);
    }

    private void HandleDiamondObtained(int baseScore, int id)
    {
        AddScoreWithMultiplier(baseScore);
        data.Diamonds[id] = true;
    }

    private void HandleAbilityFruitObtained(int baseScore, Abilities ability)
    {
        AddScoreWithMultiplier(baseScore);
        Raise_AbilityAcquired(ability);
    }
    #endregion

    #region Raise Events
    public void Raise_TapButtonClick()
    {
        if (OnTapButton != null && (state == LevelState.RUNNING || state == LevelState.TUTORIAL))
            OnTapButton();
    }

    public void Raise_GravityButtonClick()
    {
        if (OnGravityButton != null && (state == LevelState.RUNNING || state == LevelState.TUTORIAL))
        {
            if (data.GravityPower >= Constants.GRAVITY_CHUNK)
            {
                data.GravityPower -= Constants.GRAVITY_CHUNK;
                OnGravityButton(isGravityEnabled);
            }
            else if (state == LevelState.TUTORIAL)
            {
                OnGravityButton(isGravityEnabled);
            }
        }
    }

    public void Raise_SpecialButtonClick(Abilities ability)
    {
        switch (ability)
        {
            case Abilities.BLOCK_CHANGER:
                if (OnBlockChangerClick != null && state == LevelState.RUNNING)
                    OnBlockChangerClick();
                break;
        }
    }

    public void Raise_RankingButtonClick()
    {
        if (OnButtonRankingClicked != null && (state == LevelState.COMPLETED || state == LevelState.GAME_OVER))
            OnButtonRankingClicked();
    }

    public void Raise_MenuButtonClick()
    {
        if (OnButtonMenuClicked != null && state != LevelState.RUNNING)
            OnButtonMenuClicked();
    }

    public void Raise_ReplayButtonClick()
    {
        if (OnButtonReplayClicked != null && state == LevelState.GAME_OVER)
            OnButtonReplayClicked();
    }

    public void Raise_ResumeButtonClick()
    {
        if (OnButtonResumeClicked != null && state == LevelState.GAME_PAUSED)
            OnButtonResumeClicked();
    }

    public void Raise_ButtonOKClick()
    {
        if (OnButtonOK != null)
            OnButtonOK();
    }

    public void Raise_ButtonShareClicked()
    {
        if (OnButtonShareClicked != null)
            OnButtonShareClicked();
    }

    public void Raise_MultiplierUpdate()
    {
        if (OnMultiplierUpdate != null && (state == LevelState.RUNNING || state == LevelState.TUTORIAL))
            OnMultiplierUpdate();
    }

    public void Raise_MultiplierTimeFinished()
    {
        if (OnMultiplierTimeFinished != null)
            OnMultiplierTimeFinished();
    }

    public void Raise_ScoreChanged()
    {
        if (OnScoreChanged != null && state == LevelState.RUNNING)
            OnScoreChanged();
    }

    public void Raise_PausePageEnabled()
    {
        if (OnGamePaused != null)
            OnGamePaused();
    }

    public void Raise_PlayerDeath()
    {
        if (OnDeath != null && state == LevelState.RUNNING)
            OnDeath();
    }

    public void Raise_GameOver()
    {
        if (OnGameOver != null && state == LevelState.DEAD)
            OnGameOver();
    }

    public void Raise_InfiniteGameOver()
    {
        if (OnInfiniteGameOver != null && state == LevelState.DEAD)
            OnInfiniteGameOver();
    }

    public void Raise_StartLevelTrigger()
    {
        if (OnLevelStart != null && state == LevelState.INIT)
            OnLevelStart();
    }

    public void Raise_LevelCompletedTrigger()
    {
        if (OnLevelCompleted != null && state == LevelState.RUNNING)
            OnLevelCompleted();
    }

    public void Raise_LevelFragmentTrigger()
    {
        if (OnLevelFragmentTrigger != null)
            OnLevelFragmentTrigger();
    }

    public void Raise_ParallaxChangeTrigger(Biome biome)
    {
        if (OnParallaxChangeTrigger != null)
            OnParallaxChangeTrigger(biome);
            
    }

    public void Raise_SecondParsed()
    {
        if (OnSecondParsed != null && state == LevelState.RUNNING)
            OnSecondParsed();
    }

    public void Raise_CompletedLevelPageAnimationFinished()
    {
        if (OnCompletedLevelPageAnimFinished != null && state == LevelState.COMPLETED)
            OnCompletedLevelPageAnimFinished();
    }    

    public void Raise_InfiniteAttemptPageAnimationFinished()
    {
        if (OnInfiniteAttemptPageAnimationFinished != null && state == LevelState.GAME_OVER)
            OnInfiniteAttemptPageAnimationFinished();
    }

    public void Raise_TutorialTriggered(TutorialTags tag)
    {
        if (OnTutorialTriggered != null)
            OnTutorialTriggered(tag);
    }

    public void Raise_TutorialEnded(TutorialTags tag)
    {
        if (OnTutorialEnded != null)
            OnTutorialEnded(tag);
    }

    public void Raise_PlayerCollision(string msg)
    {
        if (OnPlayerCollision != null)
            OnPlayerCollision(msg);
    }

    public void Raise_ButtonBackClicled()
    {
        if (OnButtonBackClicked != null)
            OnButtonBackClicked();
    }

    public void Raise_TutorialTextLoad()
    {
        if (OnTutorialTextLoad != null)
            OnTutorialTextLoad();
    }

    public void Raise_CherryObtained(int baseScore)
    {
        if (OnCherryObtained != null)
            OnCherryObtained(baseScore);
    }

    public void Raise_GravityGemObtained(int baseScore)
    {
        if (OnGravityGemObtained != null)
            OnGravityGemObtained(baseScore);
    }

    public void Raise_DiamondObtained(int baseScore, int id)
    {
        if (OnDiamondObtained != null)
            OnDiamondObtained(baseScore, id);
    }

    public void Raise_AbilityFruitObtained(int baseScore, Abilities ability)
    {
        if (OnAbilityFruitObtained != null)
            OnAbilityFruitObtained(baseScore, ability);
    }

    public void Raise_ItemAnimationFinished(int realScore, Vector3 position)
    {
        if (OnItemAnimationFinished != null)
            OnItemAnimationFinished(realScore, position);
    }

    public void Raise_ReadyTextTrigger()
    {
        if (OnReadyTextTrigger != null)
            OnReadyTextTrigger();
    }

    public void Raise_StartTextTrigger()
    {
        if (OnStartTextTrigger != null)
            OnStartTextTrigger();
    }

    public void Raise_AbilityAcquired(Abilities ability)
    {
        if (OnAbilityAcquired != null)
            OnAbilityAcquired(ability);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Inicializa el nivel
    /// </summary>
    public void InitLevel()
    {
        Time.timeScale = Constants.DEFAULT_TIME_SCALE;
        GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].Attempts++;
        data = new AttemptData();
        ResetLevelStats();
        InitTutorial();

        isInfinite = LevelIndex == 3 ? true : false;

        state = LevelState.INIT;
    }

    /// <summary>
    /// Resetea las estadisticas del intento
    /// </summary>
    private void ResetLevelStats()
    {
        Physics2D.gravity = new Vector3(0, - Constants.GRAVITY_FORCE_Y, 0);
        isGravityEnabled = true;
        data.Taps = 0;
        data.Swaps = 0;
        data.Multiplier = Constants.DEFAULT_MULTIPLIER;
        data.HighestMultiplier = Constants.DEFAULT_MULTIPLIER;
        data.Diamonds = new bool[3] { false, false, false };
        data.Score = 0;
        data.GravityPower = Constants.BASE_GRAVITY_POWER;
        data.SpeedModifier = 1;
    }

    /// <summary>
    /// Inicializa el tutorial
    /// </summary>
    private void InitTutorial()
    {
        Instantiate(Resources.Load(Constants.TUTORIAL_RESOURCES_PATH + Constants.TUTORIAL_CANVAS) as GameObject);
        Strings.LoadTutorialText();
        Raise_TutorialTextLoad();
    }

    /// <summary>
    /// Pausa la partida
    /// </summary>
    private void PauseGame()
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = Constants.FREEZE_TIME_SCALE;     //Para el tiempo
        if (state == LevelState.INIT)
            isInitPause = true;
        state = LevelState.GAME_PAUSED;  
    }

    /// <summary>
    /// Continua la partida
    /// </summary>
    private void ResumeGame()
    {
        Time.timeScale = lastTimeScale; //Constants.DEFAULT_TIME_SCALE;
        state = isInitPause ? LevelState.INIT : LevelState.RUNNING;
        isInitPause = false;
    }

    /// <summary>
    /// Pausa la partida para el tutorial
    /// </summary>
    private void TutorialPauseFreeze()
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = Constants.FREEZE_TIME_SCALE;
        state = LevelState.TUTORIAL;
    }

    /// <summary>
    /// Cambia la gravedad del nivel
    /// </summary>
    private void SwapGravity(bool gravityEnabled)
    {
        if (this.isGravityEnabled)
        {
            Physics2D.gravity = GRAVITY_DOWN;
            this.isGravityEnabled = false;
        }
        else
        {
            Physics2D.gravity = GRAVITY_UP;
            this.isGravityEnabled = true;
        }

        IncreaseMultiplier(1);
        data.Swaps++;
    }

    /// <summary>
    /// Add energia
    /// </summary>
    private void AddGravityPower()
    {
        data.GravityPower++;
    }

    /// <summary>
    /// Actualiza los datos del nivel jugado y los guarda.
    /// </summary>
    /// <param name="cleared"> Indica si se ha completado el nivel o no</param>
    private void UpdateAndSaveLevelData(bool cleared)
    {
        if (cleared)    //Comprueba si se ha completado o no el nivel
        {
            GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].Completed = true; //Setea el nivel como completado

            //Activa los diamantes que se han recogido en algun momento
            for (int i = 0; i < data.Diamonds.Length; i++)
            {
                GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].SecretDiamonds[i] = GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].SecretDiamonds[i] || data.Diamonds[i] ? true : false;
            }

            //Actualiza el bestScore
            if (GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].BestScore < data.Score)
            {
                GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].BestScore = data.Score;
            }
        }

        //Actualiza el HighestMultiplier//Combo
        if (GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].HighestMultiplier < data.HighestMultiplier)
        {
            GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].HighestMultiplier = data.HighestMultiplier;
        }

        int world = GM.Data.LevelArray.GetLength(0);
        int levels = GM.Data.LevelArray.GetLength(1);
        GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].TotalTaps += data.Taps;    //Actualiza los taps
        GM.Data.LevelArray[GM.SelectedWorldIndex, GM.SelectedLevelIndex].TotalSwaps += data.Swaps;  //Actualiza los swaps

        GM.Raise_RequestDataSave();  //Notifica a GM, que lanza un evento para guardar la partida.

        Time.timeScale = Constants.DEFAULT_TIME_SCALE;
    }

    /// <summary>
    /// Recarga el nivel
    /// </summary>
    private void ReloadLevel()
    {
        GM.GameState = GameState.LEVEL;
    }

    /// <summary>
    /// Devuelve un string con las estadisticas del intento.
    /// </summary>
    /// <param name="tag">tag identificador de la estadistica a devolver</param>
    /// <returns></returns>
    public string GetAttemptStatString(AttemptStats tag)
    {
        string stat = "";

        switch (tag)
        {
            case AttemptStats.TAPS:
                stat = data.Taps.ToString();
                break;

            case AttemptStats.SWAPS:
                stat = data.Swaps.ToString();
                break;

            case AttemptStats.MUTIPLIER:
                stat = data.Multiplier.ToString();
                break;

            case AttemptStats.DIAMONDS:
                stat = data.DiamondsCount.ToString();
                break;

            case AttemptStats.SPEED:
                stat = (data.SpeedModifier * 10).ToString();
                break;

            case AttemptStats.SCORE:
                stat = data.Score.ToString();
                break;
        }

        return stat;
    }

    /// <summary>
    /// Utilizado para aniadir puntuacion teniendo en cuenta el multiplicador
    /// </summary>
    /// <param name="Score"></param>
    public void AddScoreWithMultiplier(int score) //TODO: BOrrar este metodo
    {
        data.Score += score * data.Multiplier;
        Raise_ScoreChanged();
    }

    /// <summary>
    /// Obtiene el nombre del nivel
    /// </summary>
    /// <returns></returns>
    public string GetLevelString()
    {
        return (Strings.GetString(TEXT_TAG.TXT_LEVEL) + " " + (GM.SelectedWorldIndex + 1) + "-" + (GM.SelectedLevelIndex + 1));
    }

    private void UpdateTutorialFlags(TutorialTags tag)
    {
        switch (tag)
        {
            case TutorialTags.TAP:
                GM.Data.IsTutorialTapDone = true;
                break;

            case TutorialTags.SWAP:
                break;

            case TutorialTags.SWAP_CONSECUENCES:
                break;

            case TutorialTags.MULTIPLIER:
                GM.Data.IsTutorialSwapDone = true;
                GM.Data.IsTutorialSwapConsecuencesDone = true;
                GM.Data.IsTutorialMultiplierDone = true;
                GM.Data.EnableTutorials = false;
                break;

            case TutorialTags.POINTS:
                GM.Data.IsTutorialScoreDone = true;
                break;
        }
    }

    /// <summary>
    /// Aumenta el valor del Multiplicador de puntos.
    /// </summary>
    /// <param name="ammount">Cantidad a aumentar el multiplicador de puntos.</param>
    private void IncreaseMultiplier(int ammount)
    {
        data.Multiplier += ammount;
        Raise_MultiplierUpdate();
    }

    /// <summary>
    /// Resetea el valor del Multiplicador de puntos
    /// </summary>
    private void ResetMultiplier()
    {
        data.Multiplier = Constants.DEFAULT_MULTIPLIER;
    }
    #endregion
}
