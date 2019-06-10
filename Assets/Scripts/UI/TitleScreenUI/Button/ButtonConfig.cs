using UnityEngine;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado al boton de configuracion de la pantalla de titulo.
/// Implementa la interface IPointerClickHandler
/// </summary>
public class ButtonConfig : MonoBehaviour, IPointerClickHandler
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Se ejecuta al instanciar el MonoBehaviour.
    /// Recoge referencias a la instancia de TSM y anim.
    /// </summary>
    private void Awake()
    {
        TSM = TitleScreenManager.Instance;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Suscribe eventos de TSM.
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
        TSM.Raise_ConfigButtonClick();
    }
    #endregion

    #region EventHandlers
    /// <summary>
    /// Activa el boton 0.25s despues de que termine la animacion del logo
    /// </summary>
    private void HandleTitleAnimationFinished()
    {
        Invoke("EnableButton", Constants.BUTTON_CONFIG_TIME_SPAWN);
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