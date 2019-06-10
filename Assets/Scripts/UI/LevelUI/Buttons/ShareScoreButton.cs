using UnityEngine;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado al boton de compartir puntuacion
/// </summary>
public class ShareScoreButton : MonoBehaviour, IPointerClickHandler
{

    #region Attributes
    private LevelManager LM;
    private Animator anim;
    #endregion

    private void Awake()
    {
        LM = LevelManager.Instance;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Se suscribe a los eventos
    /// </summary>
    private void OnEnable()
    {
        LM.OnCompletedLevelPageAnimFinished += HandleOnPageAnimationFinished;
        LM.OnInfiniteAttemptPageAnimationFinished += HandleOnInfinitePageAnimationFinished;
    }

    /// <summary>
    /// Se desuscribe a los eventos
    /// </summary>
    private void OnDisable()
    {
        LM.OnCompletedLevelPageAnimFinished -= HandleOnPageAnimationFinished;
        LM.OnInfiniteAttemptPageAnimationFinished -= HandleOnInfinitePageAnimationFinished;
    }

    #region Event Handlers
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
        LevelManager.Instance.Raise_ButtonShareClicked();
    }
    #endregion
}
