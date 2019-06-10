using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script asociado al boton de volver a jugar del menu de game over del nivel.
/// Implementa la interfaz IPointerClickhandler
/// </summary>
public class ButtonReplayLM : MonoBehaviour, IPointerClickHandler
{
    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al pinchar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.Instance.Raise_ReplayButtonClick();
    }
    #endregion
}