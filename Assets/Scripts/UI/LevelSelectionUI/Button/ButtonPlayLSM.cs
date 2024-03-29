﻿using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script asociado al boton de jugar de la pantalla de seleccion de nivel.
/// Implementa el interface IPointerClickHandler
/// </summary>
public class ButtonPlayLSM : MonoBehaviour, IPointerClickHandler
{
    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al clickar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelSelectionManager.Instance.Raise_PlayButtonClick();
    }
    #endregion
}
