using GravityBat_Localization;
using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Controlador del Scene de seleccion de nivel
/// </summary>
public class LevelSelectionManager : ScriptableObject
{
    #region Singleton
    //So una unica instancia de la clase estara instanciada al mismo tiempo
    private static LevelSelectionManager instance;
    protected LevelSelectionManager()
    {
        state = LSMState.IDLE;
    }

    public static LevelSelectionManager Instance
    {
        get
        {
            if (LevelSelectionManager.instance == null)
            {
                LevelSelectionManager.instance = ScriptableObject.CreateInstance<LevelSelectionManager>();
            }
            return LevelSelectionManager.instance;
        }
    }
    #endregion

    #region Attributes
    private GameManager GM;     //Referencia a GameManager
    private LSMState state;     //Estado
    #endregion

    #region Properties
    //Acceso publico a algunos de los atributos
    public LSMState State
    {
        get { return state; }
        set { state = value; }
    }

    public int WorldIndex
    {
        get { return GM.SelectedWorldIndex; }
        set { GM.SelectedWorldIndex = value; }
    }

    public int SelectedLevelIndex
    {
        get { return GM.SelectedLevelIndex; }
        set { GM.SelectedLevelIndex = value; }
    }
    #endregion

    #region Events
    public delegate void OnButtonClickHandler();                    //Delegado generico para botones
    public delegate void OnLevelButtonHandler(int levelIndex);      //Delegado para botones de nivel
    public delegate void OnArrowButtonHandler(Direction dir);       //Delegado para flechas de cambio de pagina
    public delegate void OnButtonBackClickHandler(LSMState state);  //Delegado para el boton de retroceso
    public delegate void OnSceneLoadedHandler();

    public event OnButtonClickHandler OnPlayButtonClicked;          //Boton jugar
    public event OnButtonClickHandler OnButtonLevelStatsClicked;    //Boton estadisticas del nivel
    public event OnButtonClickHandler OnButtonRankingClicked;       //Boton de ranking
    public event OnLevelButtonHandler OnLevelBlockedClick;      //Boton de nivel bloqueado
    public event OnLevelButtonHandler OnLevelEnabledClick;      //Boton de nivel desbloqueado
    public event OnArrowButtonHandler OnArrowButtonClicked;     //Flecha de cambio de pagina
    public event OnButtonBackClickHandler OnButtonBackClicked;  //Boton de retroceso
    public event OnSceneLoadedHandler OnSceneLoaded;
    #endregion

    /// <summary>
    /// Se ejecuta de forma inmediata al instanciar el objeto
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
        this.OnLevelBlockedClick += HandleLevelBlockedClicked;
        this.OnLevelEnabledClick += HandleLevelEnabledClicked;
        this.OnButtonBackClicked += HandleButtonBackClicked;
        this.OnPlayButtonClicked += HandlePlayButtonClicked;
        this.OnButtonLevelStatsClicked += HandleLevelStatsButtonClicked;
        this.OnButtonRankingClicked += HandleRankingButtonClicked;

        GM.OnLevelSelectSceneLoaded += HandleLevelSelectionSceneLoaded;
        GM.OnAndroidBackPressed += HandleAndroidBackButton;
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        this.OnLevelBlockedClick -= HandleLevelBlockedClicked;
        this.OnLevelEnabledClick -= HandleLevelEnabledClicked;
        this.OnButtonBackClicked -= HandleButtonBackClicked;
        this.OnPlayButtonClicked -= HandlePlayButtonClicked;
        this.OnButtonLevelStatsClicked -= HandleLevelStatsButtonClicked;
        this.OnButtonRankingClicked -= HandleRankingButtonClicked;

        GM.OnLevelSelectSceneLoaded -= HandleLevelSelectionSceneLoaded;
        GM.OnAndroidBackPressed -= HandleAndroidBackButton;
    }

    #region EventHandlers
    private void HandleLevelBlockedClicked(int levelIndex)
    {
        state = LSMState.ERROR_INFO_MENU;
    }

    private void HandleLevelEnabledClicked(int levelIndex)
    {
        state = LSMState.LEVEL_MENU;
    }

    private void HandleButtonBackClicked(LSMState s)
    {
        if (GM.GameState == GameState.LEVEL_SELECTION_MENU)
            switch (state)
            {
                case LSMState.IDLE:
                    GM.GameState = GameState.TITLE_SCREEN;
                    break;

                case LSMState.LEVEL_MENU:
                    state = LSMState.IDLE;
                    break;

                case LSMState.ERROR_INFO_MENU:
                    state = LSMState.IDLE;
                    break;

                case LSMState.LEVEL_STATS_MENU:
                    state = LSMState.LEVEL_MENU;
                    break;
            }
    }

    private void HandleLevelStatsButtonClicked()
    {
        state = LSMState.LEVEL_STATS_MENU;
        InitLevelStatsStrings(GM.SelectedLevelIndex);
    }

    private void HandleRankingButtonClicked()
    {
        GM.Raise_RequestGPGOpenLadderboard(WorldIndex, SelectedLevelIndex);
    }

    private void HandlePlayButtonClicked()
    {
        GM.GameState = GameState.LEVEL;
    }

    private void HandleLevelSelectionSceneLoaded()
    {
        state = LSMState.IDLE;
        Raise_SceneLoaded();
    }

    private void HandleAndroidBackButton()
    {
        Raise_BackButtonClick();
    }
    #endregion     //Lleva a cabo las acciones deseadas para cada evento        

    #region Raise Events
    public void Raise_LevelButtonClick(int index, bool enabled)
    {
        SelectedLevelIndex = index;
        if (enabled)
        {
            if (OnLevelEnabledClick != null)
                OnLevelEnabledClick(index);
        }
        else
        {
            if (OnLevelBlockedClick != null)
                OnLevelBlockedClick(index);
        }
    }

    public void Raise_BackButtonClick()
    {
        if (OnButtonBackClicked != null)
            OnButtonBackClicked(state);
    }

    public void Raise_ArrowButtonClick(Direction dir)
    {
        if (OnArrowButtonClicked != null)
            OnArrowButtonClicked(dir);
    }

    public void Raise_LevelStatsButtonClick()
    {
        if (OnButtonLevelStatsClicked != null)
            OnButtonLevelStatsClicked();
    }

    public void Raise_PlayButtonClick()
    {
        if (OnPlayButtonClicked != null)
            OnPlayButtonClicked();
    }

    public void Raise_RankingButtonClick()
    {
        if (OnButtonRankingClicked != null)
            OnButtonRankingClicked();
    }

    private void Raise_SceneLoaded()
    {
        if (OnSceneLoaded != null)
            OnSceneLoaded();
    }
    #endregion      

    #region String Methods
    /// <summary>
    /// Devuelve el string del nivel.
    /// </summary>
    /// <returns>string con el nombre del nivel.</returns>
    public string GetLevelString()
    {
        return (Strings.GetString(TEXT_TAG.TXT_LEVEL) + " " + (GM.SelectedWorldIndex + 1) + "-" + (GM.SelectedLevelIndex + 1));
    }

    /// <summary>
    /// Devuelve un string con el nombre del mundo.
    /// </summary>
    /// <returns>string con el nombre del mundo.</returns>
    public string GetWorldString()
    {
        return Strings.GetString(TEXT_TAG.TXT_WORLD) + " " + (GM.SelectedWorldIndex + 1);
    }

    /// <summary>
    /// Devuelve un string con la informacion del nivel.
    /// </summary>
    /// <returns>string con la informacion del nivel</returns>
    public string GetLevelBlockedInfoString()
    {
        TEXT_TAG tag = GM.SelectedLevelIndex != 4 ? TEXT_TAG.TXT_LEVEL_BLOCKED_INFO : TEXT_TAG.TXT_SPECIAL_LEVEL_BLOCKED_INFO;
        return Strings.GetString(tag);
    }

    /// <summary>
    /// Inicializa las estadisticas de un nivel
    /// </summary>
    /// <param name="levelIndex">indice del nivel dentro del mundo</param>
    public void InitLevelStatsStrings(int levelIndex)
    {
        Strings.LoadLevelDataTexts(GM.SelectedWorldIndex, levelIndex);
    }
    #endregion

    
}
