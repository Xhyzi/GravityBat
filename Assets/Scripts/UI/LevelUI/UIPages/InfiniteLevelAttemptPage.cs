using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script asociado a la pagina que muestra los datos del intento
/// en un nivel infinito.
/// </summary>
public class InfiniteLevelAttemptPage : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private Animator anim;
    private bool animFinished;
    #endregion

    private void Awake()
    {
        LM = LevelManager.Instance;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Suscribe los eventos de LevelManager
    /// </summary>
    private void OnEnable()
    {
        LM.OnInfiniteGameOver += HandleAttemptFinished;
        LM.OnButtonBackClicked += HandleButtonBackClicked;

        animFinished = false;
    }

    /// <summary>
    /// Desuscribe los eventos de LevelManager
    /// </summary>
    private void OnDisable()
    {
        LM.OnInfiniteGameOver -= HandleAttemptFinished;
        LM.OnButtonBackClicked -= HandleButtonBackClicked;
    }

    #region Event Handlers
    private void HandleButtonBackClicked()
    {
        if (LM.IsInfinite && animFinished && LM.State == LevelState.GAME_OVER)
            LM.Raise_MenuButtonClick();
    }

    private void HandleAttemptFinished()
    {
        EnablePage();
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

    /// <summary>
    /// Lleva a cabo la accion correspondiente a la finalizacion de la animacion
    /// </summary>
    private void OnAnimationFinished()
    {
        animFinished = true;
        LM.Raise_InfiniteAttemptPageAnimationFinished();
    }
    #endregion

}
