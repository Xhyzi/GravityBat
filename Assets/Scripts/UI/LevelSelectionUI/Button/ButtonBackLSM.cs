using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script asociado al boton de volver del menu de seleccion de nivel.
/// Implementa el interface IPointerClickHandler
/// </summary>
public class ButtonBackLSM : MonoBehaviour, IPointerClickHandler
{
    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al clikar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelSelectionManager.Instance.Raise_BackButtonClick();
    }
    #endregion
}