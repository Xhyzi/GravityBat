using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script asociado al slider de la opacitdad de los paneles del nivel.
/// </summary>
public class ButtonOpacitySlider : _BaseSlider
{
    /// <summary>
    /// Inicializa el valor del slider
    /// </summary>
    protected override void InitSliderValue()
    {
        slider.value = GameManager.Instance.Data.GameButtonOpacity;
    }

    /// <summary>
    /// Carga el valor del slider en los datos del GM y se lo notifica para elevar el evento.
    /// </summary>
    protected override void ValueChangedCheck()
    {
        GameManager.Instance.Data.GameButtonOpacity = slider.value;
        GameManager.Instance.Raise_UserConfigChanged();
    }
}