using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script asociado al boton de continuar jugando del menu de pausa del nivel.
/// Implementa la interfaz IPointerClickhandler
/// </summary>
public class ButtonResumeLM : MonoBehaviour, IPointerClickHandler
{
    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al pinchar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.Instance.Raise_ResumeButtonClick();
    }
    #endregion
}
