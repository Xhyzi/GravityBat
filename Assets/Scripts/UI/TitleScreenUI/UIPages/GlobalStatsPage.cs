using UnityEngine;

/// <summary>
/// Script asociado a la pagina de estadisticas globales, dentro de la pantalla de titulo.
/// Muestra estadisticas generales del jugador.
/// </summary>
public class GlobalStatsPage : MonoBehaviour
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el Monobehaviour.
    /// Recoge instancias del Manager y el animator
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
        TSM.OnButtonStatsClicked += HandlerOnButtonStatsClicked;
        TSM.OnButtonBackClicked += HandlerOnButtonBackClicked;
    }
    
    /// <summary>
    /// Se desuscribe a los eventos de TSM.
    /// </summary>
    private void OnDisable()
    {
        TSM.OnButtonStatsClicked -= HandlerOnButtonStatsClicked;
        TSM.OnButtonBackClicked -= HandlerOnButtonBackClicked;
    }

    #region EventHandlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes al boton de estadisticas
    /// </summary>
    private void HandlerOnButtonStatsClicked()
    {
        EnablePage();
    }

    /// <summary>
    /// Lleva a cabo las acciones correspondientes al boton back/back@android
    /// </summary>
    private void HandlerOnButtonBackClicked()
    {
        DisablePage();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita la pagina de estadisticas globales y lo notifica al manager.
    /// </summary>
    private void EnablePage()
    {
        anim.SetBool("Enabled", true);
        TSM.Raise_PageEnabled();
    }

    /// <summary>
    /// Desabilita la pagina de estadisticas globales y lo notifica al manager.
    /// </summary>
    private void DisablePage()
    {
        if (anim.GetBool("Enabled"))
        {
            anim.SetBool("Enabled", false);
            TSM.Raise_PageDisabled();
        }
    }
    #endregion
}
