using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script correspondiente a la pagina de GameOver en la pantalla del nivel
/// </summary>
public class GameOverMenuPage : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaiour.
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
        LM.OnGameOver += HandleGameOver;
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        LM.OnButtonBackClicked -= HandleButtonBackClicked;
        LM.OnGameOver -= HandleGameOver;
    }

    #region Event Handlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes al GameOver.
    /// Muestra la pantalla de GameOver o el autoreplay dependiendo de la configuracion
    /// </summary>
    private void HandleGameOver()   //TODO: Tener en cuenta nivel infinito
    {
        if (GameManager.Instance.Data.AutoRetry && !LM.IsInfinite)    //&& !LM.IsInfinite
            Invoke("AutoReplay", Constants.AUTO_REPLAY_WAIT_TIME);
        else if (!LM.IsInfinite)
            EnablePage();       
    }

    /// <summary>
    /// Lleva a cabo la accion correspondiente al boton back
    /// </summary>
    private void HandleButtonBackClicked()
    {
        if (LM.State == LevelState.GAME_OVER)
            LM.Raise_MenuButtonClick();
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
    /// Deshabilita la pagina
    /// </summary>
    private void DisablePage()
    {
        if (anim.GetBool("Enabled"))
            anim.SetBool("Enabled", false);
    }

    /// <summary>
    /// Notifica a LM que debe elevar el evento de Replay.
    /// </summary>
    private void AutoReplay()
    {
        LM.Raise_ReplayButtonClick();
    }
    #endregion
}
