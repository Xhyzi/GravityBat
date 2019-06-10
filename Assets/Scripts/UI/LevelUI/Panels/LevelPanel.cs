using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado a los paneles utilizados para recoger el Input del nivel.
/// Las acciones asociadas al panel dependeran de la direccion que se establezca asi
/// como de la configuracion del usuario (si ha intercambiado las posiciones o no.
/// Por defecto el 'Tap' se encontrara a la derecha y el 'swap' en la izquierda.
///     Nota:   'Tap' hace referencia a volar.
///             'Swap' hace referencia al cambio de gravedad.
/// </summary>
public class LevelPanel : MonoBehaviour, IPointerDownHandler
{
    #region Attributes
    private LevelManager LM;
    private Button btn;
    private delegate void PanelTask();
    private PanelTask PanelTaskDelegate;
    private bool enabled;
    #endregion

    #region Serialized Field
    [SerializeField]
    Direction dir;
    #endregion
    
    /// <summary>
    /// Es ejecutado al instanciar el MonoBehaviour.
    /// Recoge instancias del LM y el button.
    /// Establece la funcionalidad del panel y el grado de transparencia al ser pulsado.
    /// </summary>
    private void Awake()
    {
        LM = LevelManager.Instance;
        btn = GetComponent<Button>();

        SetPanelDelegate();
        SetButtonPressedAlpha(GameManager.Instance.Data.ShowLevelPanels);

        enabled = true;
    }

    /// <summary>
    /// Se suscribe a los eventos de LM.
    /// </summary>
    private void OnEnable()
    {
        LM.OnTutorialTriggered += HandleTutorialTriggered;
        LM.OnTutorialEnded += HandleTutorialEnded;
    }

    /// <summary>
    /// Se desuscribe a los eventos de LM.
    /// </summary>
    private void OnDisable()
    {
        LM.OnTutorialTriggered -= HandleTutorialTriggered;
        LM.OnTutorialEnded -= HandleTutorialEnded;
    }

    #region IPointerDownHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al clickar sobre el boton (en el press, no en el release -> disminuye el delay desde que el usuario pulsa hasta que se ejecuta la accion)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (enabled)
            PanelTaskDelegate();
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes con la activacion de un tutorial
    /// </summary>
    /// <param name="tutorial">tutorial activado</param>
    private void HandleTutorialTriggered(TutorialTags tutorial)
    {
        switch (tutorial)
        {
            case TutorialTags.TAP:
                if (dir == Direction.RIGHT)
                    HighlightButton();
                else
                    enabled = false;
                break;

            case TutorialTags.SWAP:
                if (dir == Direction.LEFT)
                    HighlightButton();
                break;
        }
    }

    /// <summary>
    /// Lleva a cabo las acciones correspondientes a la finalizacion de un tutorial
    /// </summary>
    /// <param name="tutorial">tutorial finalizado</param>
    private void HandleTutorialEnded(TutorialTags tutorial)
    {
        Color c = new Color(255, 255, 255, 0);
        ColorBlock cb = btn.colors;
        cb.normalColor = c;
        cb.highlightedColor = c;
        btn.colors = cb;

        enabled = true;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Establece el valor del delegado seteando la funcionalidad
    /// que le correspondera al panel.
    /// </summary>
    private void SetPanelDelegate()
    {
        if (dir == Direction.RIGHT)
        {
            if (!GameManager.Instance.Data.SwapLevelPanels)
                PanelTaskDelegate = new PanelTask(PanelTaskTap);
            else
                PanelTaskDelegate = new PanelTask(PanelTaskGravitySwap);
        }
        else
        {
            if (!GameManager.Instance.Data.SwapLevelPanels)
                PanelTaskDelegate = new PanelTask(PanelTaskGravitySwap);
            else
                PanelTaskDelegate = new PanelTask(PanelTaskTap);
                
        }
    }

    /// <summary>
    /// Establece la transparencia que tendra al boton al ser pulsado
    /// </summary>
    /// <param name="enabled"></param>
    private void SetButtonPressedAlpha(bool enabled)
    {
        if (!enabled)
        {
            Color c = new Color(255, 255, 255, 0);
            ColorBlock cb = btn.colors;
            cb.pressedColor = c;
            btn.colors = cb;
        }
    }

    /// <summary>
    /// Destaca el boton aumentando mucho su opacidad y dandole un color blanco.
    /// Utilizado para los tutoriales.
    /// </summary>
    private void HighlightButton()
    {
        Color c = new Color(255, 255, 255, 0.2f);
        ColorBlock cb = btn.colors;
        cb.normalColor = c;
        cb.highlightedColor = c;
        btn.colors = cb;
    }

    /// <summary>
    /// Notifica el 'Tap' a LM para elevart el evento.
    /// </summary>
    private void PanelTaskTap()
    {
        LM.Raise_TapButtonClick();
    }

    /// <summary>
    /// Notifica el 'Swap' a LM para elevar el evento.
    /// </summary>
    private void PanelTaskGravitySwap()
    {
        LM.Raise_GravityButtonClick();
    }
    #endregion
}
