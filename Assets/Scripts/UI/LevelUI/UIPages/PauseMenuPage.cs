using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script vinculado con la pantalla de pausa del nivel
/// </summary>
public class PauseMenuPage : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias de LM y animator.
    /// </summary>
    private void Awake()
    {
        LM = LevelManager.Instance;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Suscribe los eventos
    /// </summary>
    private void OnEnable()
    {
        LM.OnButtonBackClicked += HandleButtonBackClicked;
        LM.OnButtonResumeClicked += HandleResumeButton;
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        LM.OnButtonBackClicked -= HandleButtonBackClicked;
        LM.OnButtonResumeClicked -= HandleResumeButton;
    }

    #region Event Handlers
    /// <summary>
    /// Activa la pagina de pausa y lo notifica al LevelManager para pausar la partidfa
    /// </summary>
    private void HandleButtonBackClicked()
    {
        if (LM.State == LevelState.RUNNING || LM.State == LevelState.INIT)
        {
            EnablePage();
            LM.Raise_PausePageEnabled();
        }
        else if (LM.State == LevelState.GAME_PAUSED)
        {
            LM.Raise_MenuButtonClick();
        }
    }
    /// <summary>
    /// Lleva a cabo la accion correspondiente al boton continuar
    /// </summary>
    private void HandleResumeButton()
    {
        DisablePage();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita la pagina
    /// </summary>
    private void EnablePage()
    {
        anim.SetBool("Enabled", true);
    }

    /// <summary>
    /// Desabilita la pagina
    /// </summary>
    private void DisablePage()
    {
        if (anim.GetBool("Enabled"))
            anim.SetBool("Enabled", false);
    }
    #endregion
}
