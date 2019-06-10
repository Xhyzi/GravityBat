using UnityEngine;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado a las flechas de cambio de mundo en la pantalla
/// de seleccion de nivel.
/// Implementa la interface IPointerClickHandler
/// </summary>
public class ButtonArrowLSM : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Direction dir;

    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al clikar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelSelectionManager.Instance.Raise_ArrowButtonClick(dir);
    }
    #endregion
}
