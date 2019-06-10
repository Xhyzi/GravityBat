using UnityEngine;

/// <summary>
/// Script asociado al panel de seleccion de idiomas, dentro de la configuracion del juego.
/// Permite seleccionar el idioma en el que se muestran los textos del juego.
/// </summary>
public class LanguagePanel : MonoBehaviour
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Llamado al instanciar el MonoBehaviour.
    /// Recoge las referencias del Manager (TSM) y del animator
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
        TSM.OnButtonLanguageClicked += HandlerOnButtonLanguageClicked;
        TSM.OnButtonBackClicked += HandlerOnButtonBackClicked;
        TSM.OnButtonOkClicked += HandlerOnButtonBackClicked;
    }

    /// <summary>
    /// Se desuscribe a los eventos de TSM.
    /// </summary>
    private void OnDisable()
    {
        TSM.OnButtonLanguageClicked -= HandlerOnButtonLanguageClicked;
        TSM.OnButtonBackClicked -= HandlerOnButtonBackClicked;
        TSM.OnButtonOkClicked -= HandlerOnButtonBackClicked;
    }

    #region EventHandlers
    /// <summary>
    /// Lleva a cabo la acción correspondiente al botón de idioma
    /// </summary>
    private void HandlerOnButtonLanguageClicked()
    {
        EnablePanel();
    }

    /// <summary>
    /// Lleva a cabo la acción correspondiente al botón de volver/ok
    /// </summary>
    private void HandlerOnButtonBackClicked()
    {
        DisablePanel();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita el panel de seleccion de idiomas
    /// </summary>
    private void EnablePanel()
    {
        anim.SetBool("Enabled", true);
    }

    /// <summary>
    /// Desabilita el panel de seleccion de idiomas
    /// </summary>
    private void DisablePanel()
    {
        anim.SetBool("Enabled", false);
    }
    #endregion
}