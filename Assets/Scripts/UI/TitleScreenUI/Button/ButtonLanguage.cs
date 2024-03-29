﻿using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script asociado al boton de idioma de la pantalla de titulo.
/// Implementa la interface IPointerClickHandler
/// </summary>
public class ButtonLanguage : MonoBehaviour, IPointerClickHandler
{
    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al pinchar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        TitleScreenManager.Instance.Raise_LanguageButtonClick();
    }
    #endregion
}