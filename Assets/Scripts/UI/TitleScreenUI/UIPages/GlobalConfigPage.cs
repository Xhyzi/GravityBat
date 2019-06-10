using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script correspondiente a la pagina de configuracion de la pantalla de titulo.
/// Permite personalizar varias opciones.
/// </summary>
public class GlobalConfigPage : MonoBehaviour
{
    #region Attributes
    private TitleScreenManager TSM;
    private Animator anim;
    private ConfigPageState state;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias del manager y el animator e inicializa el state.
    /// </summary>
    private void Awake()
    {
        TSM = TitleScreenManager.Instance;
        anim = GetComponent<Animator>();
        state = ConfigPageState.MAIN;
    }

    /// <summary>
    /// Se suscribe a los eventos de TSM.
    /// </summary>
    private void OnEnable()
    {
        TSM.OnButtonConfigClicked += HandlerOnButtonConfigClicked;
        TSM.OnButtonBackClicked += HandlerOnButtonBackClicked;
        TSM.OnButtonOkClicked += HandlerOnButtonBackClicked;
        TSM.OnButtonLanguageClicked += HandlerOnButtonLanguageClicked;
    }

    /// <summary>
    /// Se desuscribe a los eventos de TSM.
    /// </summary>
    private void OnDisable()
    {
        TSM.OnButtonConfigClicked -= HandlerOnButtonConfigClicked;
        TSM.OnButtonBackClicked -= HandlerOnButtonBackClicked;
        TSM.OnButtonOkClicked -= HandlerOnButtonBackClicked;
        TSM.OnButtonLanguageClicked -= HandlerOnButtonLanguageClicked;
    }

    #region EventHandlers
    /// <summary>
    /// Lleva a cabo las acciones asociadas con el boton de configuracion
    /// </summary>
    private void HandlerOnButtonConfigClicked()
    {
        EnablePage();
    }

    /// <summary>
    /// Lleva a cabo las acciones asociadas con el boton de back/back@android
    /// </summary>
    private void HandlerOnButtonBackClicked()
    {
        if (anim.GetBool("Enabled"))
        {
            switch (state)
            {
                case ConfigPageState.MAIN:
                    DisablePage();
                    break;

                case ConfigPageState.LANGUAGE:
                    state = ConfigPageState.MAIN;
                    break;
            }
        }
    }

    /// <summary>
    /// Lleva a cabo las acciones asociadas con el boton de idioma
    /// </summary>
    private void HandlerOnButtonLanguageClicked()
    {
        state = ConfigPageState.LANGUAGE;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita la pagina de configuracion de la pantalla de titulo
    /// </summary>
    private void EnablePage()
    {
        anim.SetBool("Enabled", true);
        TSM.Raise_PageEnabled();
    }

    /// <summary>
    /// Desabilita la pagina de configuracion de la pantalla de titulo
    /// </summary>
    private void DisablePage()
    {
        if (anim.GetBool("Enabled"))
        {
            anim.SetBool("Enabled", false);
            TSM.Raise_PageDisabled();
            GameManager.Instance.Raise_RequestDataSave();
        }
    }
    #endregion

}