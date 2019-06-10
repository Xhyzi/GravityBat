using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GravityBat_Constants;

/// <summary>
/// Script asociado al boton de volver de la pantalla de titulo.
/// Implementa la interface IPointerClickHandler
/// </summary>
public class ButtonBack : MonoBehaviour, IPointerClickHandler
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    private Image img;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias a TSM, animator e Image.
    /// </summary>
    private void Awake()
    {
        TSM = TitleScreenManager.Instance;
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();
    }

    /// <summary>
    /// Se suscribe a eventos de TSM.
    /// </summary>
    private void OnEnable()
    {
        TSM.OnUIPageEnabled += HandlerOnUIPageEnabled;
        TSM.OnUIPageDisabled += HandlerOnUIPageDisabled;
    }

    /// <summary>
    /// Desuscribe los eventos de TSM.
    /// </summary>
    private void OnDisable()
    {
        TSM.OnUIPageEnabled -= HandlerOnUIPageEnabled;
        TSM.OnUIPageDisabled -= HandlerOnUIPageDisabled;
    }

    #region Event Handlers
    /// <summary>
    /// Activa el boton con un delay de 1/5 s desde la activacion de la ventana
    /// </summary>
    private void HandlerOnUIPageEnabled()
    {
        Invoke("EnableButton", Constants.BUTTON_BACK_TIME_SPAWN);
    }

    /// <summary>
    /// Desactiva el boton
    /// </summary>
    private void HandlerOnUIPageDisabled()
    {
        //anim.SetBool("Disabled", true);
        img.enabled = false;    //TODO: La animacion no funciona correctamente en Android (concretamente la parte del Disable)       
    }
    #endregion

    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al pinchar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        TSM.Raise_BackButtonClick();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita el boton
    /// </summary>
    private void EnableButton()
    {
        img.enabled = true; //TODO: Una vez arreglada la animacion en Android hay que quitar esto
        anim.SetTrigger("Enabled");
    }
    #endregion
}