using UnityEngine;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado al boton jugar de la pantalla de titulo.
/// Implementa la interface IPointerClickHandler
/// </summary>
public class ButtonPlay : MonoBehaviour, IPointerClickHandler
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Ejecutadl al instanciar el 
    /// </summary>
    private void Awake()
    {
        TSM = TitleScreenManager.Instance;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!GameManager.Instance.Data.IsWelcomeMessageDone)
            Invoke("RaiseWelcomeMessage", 3);
    }

    /// <summary>
    /// Se suscribe a eventos de TSM.
    /// </summary>
    private void OnEnable()
    {
        TSM.OnTitleAnimationFinished += HandleTitleAnimationFinished;
    }

    /// <summary>
    /// Desuscribe eventos de TSM.
    /// </summary>
    private void OnDisable()
    {
        TSM.OnTitleAnimationFinished -= HandleTitleAnimationFinished;
    }

    #region IpointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al pinchar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        TSM.Raise_PlayButtonClick();
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Activa el boton cuando termina la animacion del logo
    /// </summary>
    private void HandleTitleAnimationFinished()
    {
        Invoke("EnableButton", Constants.BUTTON_PLAY_TIME_SPAWN);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita el boton
    /// </summary>
    private void EnableButton()
    {
        anim.SetTrigger("Enabled");
    }

    private void RaiseWelcomeMessage()
    {
        TSM.Raise_WelcomeMessage();
    }
    #endregion
}