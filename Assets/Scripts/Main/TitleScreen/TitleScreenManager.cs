using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Controlador para el Menu principal de la aplicacion.
/// Contiene una referencia a la instancia del GameManager y se suscribe a sus eventos.
/// Contiene eventos a los que se suscriben los distintos componentes del menu principal.
/// </summary>
public class TitleScreenManager : ScriptableObject
{
    #region Singleton
    private static TitleScreenManager instance = null;

    protected TitleScreenManager()  
    {
        state = TitleScreenState.IDLE;
    }

    public static TitleScreenManager Instance
    {
        get
        {
            if (TitleScreenManager.instance == null)
            {
                TitleScreenManager.instance = ScriptableObject.CreateInstance<TitleScreenManager>();
            }
            return TitleScreenManager.instance;
        }
    }
    #endregion

    #region Attributes
    private TitleScreenState state;
    private GameManager GM;
    #endregion

    #region Events
    //Delegados
    public delegate void OnUIPageStateChangedHandler(); 
    public delegate void OnButtonClickedHandler();
    public delegate void OnAnimationEventHandler();
    public delegate void OnLanguageToggleGroupHandler(Languages language);
    public delegate void OnSceneLoadedHandler();
    public delegate void OnWelcomeMessageHandler();

    //Eventos
    public event OnUIPageStateChangedHandler OnUIPageEnabled;
    public event OnUIPageStateChangedHandler OnUIPageDisabled;
    public event OnButtonClickedHandler OnButtonPlayClicked;
    public event OnButtonClickedHandler OnButtonStatsClicked;
    public event OnButtonClickedHandler OnButtonConfigClicked;
    public event OnButtonClickedHandler OnButtonInfoClicked;
    public event OnButtonClickedHandler OnButtonBackClicked;
    public event OnButtonClickedHandler OnButtonLanguageClicked;
    public event OnButtonClickedHandler OnButtonOkClicked;
    public event OnButtonClickedHandler OnButtonShareClicked;
    public event OnWelcomeMessageHandler OnWelcomeMessage;

    public event OnAnimationEventHandler OnTitleAnimationFinished;
    public event OnLanguageToggleGroupHandler OnLanguageToggleActive;
    public event OnSceneLoadedHandler OnSceneLoaded;
    #endregion

    private void Awake()
    {
        GM = GameManager.Instance;  
    }

    /// <summary>
    /// Suscribe los eventos
    /// </summary>
    private void OnEnable()
    {
        this.OnButtonPlayClicked += HandleOnButtonPlayClicked;
        GM.OnAndroidBackPressed += HandleAndroidBackPressed;
        this.OnWelcomeMessage += HandleWelcomeMessage;
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        this.OnButtonPlayClicked -= HandleOnButtonPlayClicked;
        GM.OnAndroidBackPressed -= HandleAndroidBackPressed;
        this.OnWelcomeMessage -= HandleWelcomeMessage;
    }

    #region Event Handlers  
    private void HandleOnButtonPlayClicked()
    {
        GM.GameState = GameState.LEVEL_SELECTION_MENU;
    }

    private void HandleAndroidBackPressed()
    {
        Raise_BackButtonClick();
    }

    private void HandleWelcomeMessage()
    {
        Instantiate(Resources.Load("prefabs/welcome_message") as GameObject);
    }
    #endregion

    #region Event Raising
    //Disparan los eventos
    public void Raise_PlayButtonClick()
    {
        if (OnButtonPlayClicked != null)
            OnButtonPlayClicked();
    }

    public void Raise_StatsButtonClick()
    {
        if (OnButtonStatsClicked != null)
            OnButtonStatsClicked();
    }

    public void Raise_ConfigButtonClick()
    {
        if (OnButtonConfigClicked != null)
            OnButtonConfigClicked();
    }

    public void Raise_InfoButtonClick()
    {
        if (OnButtonInfoClicked != null)
            OnButtonInfoClicked();
    }

    public void Raise_BackButtonClick()
    {
        if (OnButtonBackClicked != null)
            OnButtonBackClicked();
    }

    public void Raise_LanguageButtonClick()
    {
        if (OnButtonLanguageClicked != null)
            OnButtonLanguageClicked();
    }

    public void Raise_OkButtonClick()
    {
        if (OnButtonOkClicked != null)
            OnButtonOkClicked();
    }

    public void Raise_PageEnabled()
    {
        if (OnUIPageEnabled != null)
        {
            OnUIPageEnabled();
        }
            
    }

    public void Raise_PageDisabled()
    {
        if (OnUIPageDisabled != null)
        {
            OnUIPageDisabled();
        }
            
    }

    public void Raise_TitleAnimationFinished()
    {
        
        if (OnTitleAnimationFinished != null)
            OnTitleAnimationFinished();          
    }

    public void Raise_ToggleGroupToggleActive()
    {
        if (OnLanguageToggleActive != null)
            OnLanguageToggleActive(GM.Data.Language);
    }

    public void Raise_SceneLoaded()
    {
        if (OnSceneLoaded != null)
            OnSceneLoaded();
    }

    public void Raise_ButtonShareClick()
    {
        if (OnButtonShareClicked != null)
            OnButtonShareClicked();
    }

    public void Raise_WelcomeMessage()
    {
        if (OnWelcomeMessage != null)
            OnWelcomeMessage();
    }
    #endregion
}
