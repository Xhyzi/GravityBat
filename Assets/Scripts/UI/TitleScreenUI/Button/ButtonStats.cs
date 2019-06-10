using UnityEngine;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado al boton de estadisticas de la pantalla de titulo.
/// Implementa la interface IPointerClickHandler
/// </summary>
public class ButtonStats : MonoBehaviour, IPointerClickHandler
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias a TSM y animator.
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
        TSM.OnTitleAnimationFinished += HandleOnTitleAnimFinished;
    }

    /// <summary>
    /// Desuscribe eventos de TSM.
    /// </summary>
    private void OnDisable()
    {
        TSM.OnTitleAnimationFinished -= HandleOnTitleAnimFinished;
    }

    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al pinchar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        TSM.Raise_StatsButtonClick();
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Activa el boton 0.4s despues de que termine la animacion del title logo
    /// </summary>
    private void HandleOnTitleAnimFinished()
    {
        Invoke("EnableButton", Constants.BUTTON_STATS_TIME_SPAWN);
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