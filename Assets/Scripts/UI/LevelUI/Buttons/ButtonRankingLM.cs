using UnityEngine;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado al boton de ranking del menu de nivel completado.
/// Implementa la interfaz IPointerClickhandler
/// </summary>
public class ButtonRankingLM : MonoBehaviour, IPointerClickHandler
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
    /// Suscribe los eventos de LM.
    /// </summary>
    private void OnEnable()
    {
        LM.OnCompletedLevelPageAnimFinished += HandleOnPageAnimationFinished;
        LM.OnInfiniteAttemptPageAnimationFinished += HandleOnInfinitePageAnimationFinished;
    }

    /// <summary>
    /// Desuscribe los eventos de LM.
    /// </summary>
    private void OnDisable()
    {
        LM.OnCompletedLevelPageAnimFinished -= HandleOnPageAnimationFinished;
        LM.OnInfiniteAttemptPageAnimationFinished -= HandleOnInfinitePageAnimationFinished;
    }

    #region Event Handlers
    /// <summary>
    /// Activa el boton cuando finaliza la animacion del menu de nivel completado.
    /// </summary>
    private void HandleOnPageAnimationFinished()
    {
        if (LM.State == LevelState.COMPLETED)
            anim.SetTrigger("Enabled");
    }

    private void HandleOnInfinitePageAnimationFinished()
    {
        if (LM.State == LevelState.GAME_OVER)
            anim.SetTrigger("Enabled");
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
        LevelManager.Instance.Raise_RankingButtonClick();
    }
    #endregion
}