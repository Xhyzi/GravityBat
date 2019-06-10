using UnityEngine;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado al boton de informacion de la pantalla de titulo.
/// Implementa la interface IPointerClickHandler
/// </summary>
public class ButtonInfo : MonoBehaviour, IPointerClickHandler
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias del TSM y el animator.
    /// </summary>
    private void Awake()
    {
        TSM = TitleScreenManager.Instance;
        anim = GetComponent<Animator>();
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

    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al pinchar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        TSM.Raise_InfoButtonClick();
    }
    #endregion

    #region EventHandlers
    /// <summary>
    /// Activa el boton cuando han pasado 0.3f desde la animacion del logo
    /// </summary>
    private void HandleTitleAnimationFinished()
    {
        Invoke("EnableButton", Constants.BUTTON_INFO_TIME_SPANW);
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