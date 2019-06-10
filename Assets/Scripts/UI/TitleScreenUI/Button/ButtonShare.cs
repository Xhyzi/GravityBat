using UnityEngine;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado al boton de compartir de la pantalla de titulo
/// </summary>
public class ButtonShare : MonoBehaviour, IPointerClickHandler
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    #endregion

    private void Awake()
    {
        TSM = TitleScreenManager.Instance;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Se suscribe a eventos de TSM
    /// </summary>
    private void OnEnable()
    {
        TSM.OnTitleAnimationFinished += HandleTitleAnimationFinished;
    }

    /// <summary>
    /// Se desuscribe a los eventosd e TSM
    /// </summary>
    private void OnDisable()
    {
        TSM.OnTitleAnimationFinished -= HandleTitleAnimationFinished;
    }

    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al pinchar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        TSM.Raise_ButtonShareClick();
    }
    #endregion

    #region EventHandlers
    /// <summary>
    /// Activa el boton cuando termina la animacion del logo
    /// </summary>
    private void HandleTitleAnimationFinished()
    {
        Invoke("EnableButton", Constants.BUTTON_SHARE_TIME_SPAWN);
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
    #endregion
}
