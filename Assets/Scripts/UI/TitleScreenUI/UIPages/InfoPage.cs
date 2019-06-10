using UnityEngine;

/// <summary>
/// Script asociado a la pagina de creditos/informacion de la pantalla de titulo.
/// Muestra los creditos del juego
/// </summary>
public class InfoPage : MonoBehaviour
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias del Manager y el animator.
    /// </summary>
    private void Awake()
    {
        TSM = TitleScreenManager.Instance;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Se suscribe a los eventos de TSM.
    /// </summary>
    private void OnEnable()
    {
        TSM.OnButtonInfoClicked += HandleOnButtonInfoClicked;
        TSM.OnButtonBackClicked += HandleOnButtonBackClicked;
    }

    /// <summary>
    /// Se desuscribe a los eventos de TSM.
    /// </summary>
    private void OnDisable()
    {
        TSM.OnButtonInfoClicked -= HandleOnButtonInfoClicked;
        TSM.OnButtonBackClicked -= HandleOnButtonBackClicked;
    }

    #region EventHandlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes al boton de info/creditos
    /// </summary>
    private void HandleOnButtonInfoClicked()
    {
        EnablePage();
    }

    /// <summary>
    /// Lleva a cabo las acciones correspondientes al boton back/back@android
    /// </summary>
    private void HandleOnButtonBackClicked()
    {
        Disablepage();
    }
    #endregion

    #region Methods 
    /// <summary>
    /// Habilita la pagina de creditos y lo notifica al manager
    /// </summary>
    private void EnablePage()
    {
        anim.SetBool("Enabled", true);
        TSM.Raise_PageEnabled();
    }

    /// <summary>
    /// Desabilita la pagina de creditos y lo notifica al manager
    /// </summary>
    private void Disablepage()
    {
        if (anim.GetBool("Enabled"))
        {
            anim.SetBool("Enabled", false);
            TSM.Raise_PageDisabled();
        }
    }
    #endregion
}